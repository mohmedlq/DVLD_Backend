using DVLD.Application.DTOs;
using DVLD.Application.DTOs.LocalDrivingLicenseApplications;
using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using ApplicationEntity=DVLD.Domain.Entities.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DVLD.Domain.Common;
using DVLD.Domain.Enums;

namespace DVLD.Application.Services
{
    public class LocalDrivingLicenseApplicationsService : ILocalDrivingLicenseApplicationsService
    {
        private readonly ILocalDrivingLicenseApplicationsRepository _repository;
        private readonly IApplicationsService _applicationsService;
        private readonly IApplicationTypesService _applicationTypesService;
        public LocalDrivingLicenseApplicationsService(ILocalDrivingLicenseApplicationsRepository repository, IApplicationsService applicationsService,IApplicationTypesService AppType)
        {
            _repository = repository;
            _applicationsService=applicationsService;
            _applicationTypesService=AppType;

        }

        #region ICrud Implementation

        public async Task<bool> CreateAsync(CreateLocalDrivingLicenseApplicationRequest request)
        {
            if (request.ApplicantPersonID <= 0)
                throw new InvalidOperationException("Invalid Applicant Person ID.");
            var application = new ApplicationEntity
            {
                ApplicantPersonID = request.ApplicantPersonID,
                ApplicationTypeID = EnumValidator.GetValidatedIntId<enApplicationType>(enApplicationType.NewLocalDrivingLicenseService),
                PaidFees =await _applicationTypesService.GetApplicationFeesAsync(enApplicationType.NewLocalDrivingLicenseService)?? throw new InvalidOperationException("Application fees not found."),
                CreatedByUserID = request.CreatedByUserID
            };

            int applicationId = await _applicationsService.CreateAsync(application);

            var localApplication = new LocalDrivingLicenseApplication
            {
                ApplicationID = applicationId,
                LicenseClassID = request.LicenseClassID,
            };
            var result = await _repository.AddAsync(localApplication);
            if (!result)
            {
                throw new InvalidOperationException(
                           "Failed to create local driving license application.");
            }
            return applicationId>0;
        }

        public async Task<bool> UpdateAsync(LocalDrivingLicenseApplication entity)
        {
            var existing = await _repository.GetByIdAsync(entity.LocalDrivingLicenseApplicationID);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Local Driving License Application with ID {entity.LocalDrivingLicenseApplicationID} was not found.");
            }

            if (existing.ApplicationID != entity.ApplicationID)
            {
                if (await _repository.ExistsByApplicationIdAsync(entity.ApplicationID))
                {
                    throw new InvalidOperationException("A local driving license application already exists for this application.");
                }
            }

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Local Driving License Application with ID {id} was not found.");
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<LocalDrivingLicenseApplication>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<LocalDrivingLicenseApplication?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        #endregion

        #region ILocalDrivingLicenseApplicationsService Implementation

        public async Task<LocalDrivingLicenseApplication?> GetByApplicationIdAsync(int applicationId)
        {
            return await _repository.GetByApplicationIdAsync(applicationId);
        }

        public async Task<bool> ExistsByApplicationIdAsync(int applicationId)
        {
            return await _repository.ExistsByApplicationIdAsync(applicationId);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        #endregion
    }
}

