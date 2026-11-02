using AppEntity = DVLD.Domain.Entities.Application;
using DVLD.Application.Interfaces;
using DVLD.Domain.Enums;
using DVLD.Domain.Common;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Services
{
    public class ApplicationsService : IApplicationsService
    {
        private readonly IApplicationsRepository _repository;

        public ApplicationsService(IApplicationsRepository repository)
        {
            _repository = repository;
        }

        #region CRUD Implementation

        public async Task<int> CreateAsync(AppEntity entity)
        {
            if (entity.ApplicantPersonID <= 0)
            {
                throw new InvalidOperationException(
                    "Invalid Applicant Person ID.");
            }

            EnumValidator.GetValidatedIntId<enApplicationType>(
                entity.ApplicationTypeID
            );

            if (entity.CreatedByUserID <= 0)
            {
                throw new InvalidOperationException(
                    "Invalid Created By User ID.");
            }

            if (entity.ApplicationDate == default)
            {
                entity.ApplicationDate = DateTime.Now;
            }

            if (entity.ApplicationStatus == 0)
            {
                entity.ApplicationStatus =
                    EnumValidator.GetValidatedByteId(
                        enApplicationStatus.New
                    );
            }
            else
            {
                EnumValidator.GetValidatedByteId<enApplicationStatus>(
                    entity.ApplicationStatus
                );
            }

            entity.LastStatusDate = DateTime.Now;

            var result = await _repository.AddAsync(entity);

            if (!result)
            {
                throw new InvalidOperationException(
                    "Failed to create application.");
            }

            return entity.ApplicationID;
        }

        public async Task<bool> UpdateAsync(AppEntity entity)
        {
            var existing = await _repository.GetByIdAsync(
                entity.ApplicationID
            );

            if (existing is null)
            {
                throw new KeyNotFoundException(
                    $"Application with ID {entity.ApplicationID} was not found.");
            }

            EnumValidator.GetValidatedIntId<enApplicationType>(
                entity.ApplicationTypeID
            );

            EnumValidator.GetValidatedByteId<enApplicationStatus>(
                entity.ApplicationStatus
            );

            entity.LastStatusDate = DateTime.Now;

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing is null)
            {
                throw new KeyNotFoundException(
                    $"Application with ID {id} was not found.");
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AppEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<AppEntity?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        #endregion

        #region Application Specific Implementation

        public async Task<AppEntity?> GetByApplicantPersonIdAsync(
            int personId)
        {
            return await _repository.GetByApplicantPersonIdAsync(personId);
        }

        public async Task<AppEntity?> GetByApplicationTypeIdAsync(
            enApplicationType applicationTypeId)
        {
            int id = EnumValidator.GetValidatedIntId(applicationTypeId);

            return await _repository.GetByApplicationTypeIdAsync(id);
        }

        public async Task<IEnumerable<AppEntity>> AllApplicationsForUserId(
            int userId)
        {
            return await _repository.AllApplicationsForUserId(userId);
        }

        public async Task<IEnumerable<AppEntity>> GetByStatusAsync(
            enApplicationStatus applicationStatus)
        {
            byte status = EnumValidator.GetValidatedByteId(applicationStatus);

            return await _repository.GetByStatusAsync(status);
        }

        public async Task<bool> ExistsAsync(
            int applicantPersonId,
            enApplicationType applicationTypeId)
        {
            int typeId = EnumValidator.GetValidatedIntId(applicationTypeId);

            return await _repository.ExistsAsync(
                applicantPersonId,
                typeId
            );
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public async Task<bool> CancelAsync(int applicationId)
        {
            var existing = await _repository.GetByIdAsync(applicationId);

            if (existing is null)
            {
                throw new KeyNotFoundException(
                    $"Application with ID {applicationId} was not found.");
            }

            if (existing.ApplicationStatus ==
                (byte)enApplicationStatus.Cancelled)
            {
                throw new InvalidOperationException(
                    "Application is already cancelled.");
            }

            if (existing.ApplicationStatus ==
                (byte)enApplicationStatus.Completed)
            {
                throw new InvalidOperationException(
                    "Cannot cancel a completed application.");
            }

            existing.ApplicationStatus =
                EnumValidator.GetValidatedByteId(
                    enApplicationStatus.Cancelled
                );

            existing.LastStatusDate = DateTime.Now;

            return await _repository.UpdateAsync(existing);
        }

        public async Task<bool> SetCompleteAsync(int applicationId)
        {
            var existing = await _repository.GetByIdAsync(applicationId);

            if (existing is null)
            {
                throw new KeyNotFoundException(
                    $"Application with ID {applicationId} was not found.");
            }

            if (existing.ApplicationStatus ==
                (byte)enApplicationStatus.Cancelled)
            {
                throw new InvalidOperationException(
                    "Cannot complete a cancelled application.");
            }

            if (existing.ApplicationStatus ==
                (byte)enApplicationStatus.Completed)
            {
                throw new InvalidOperationException(
                    "Application is already completed.");
            }

            existing.ApplicationStatus =
                EnumValidator.GetValidatedByteId(
                    enApplicationStatus.Completed
                );

            existing.LastStatusDate = DateTime.Now;

            return await _repository.UpdateAsync(existing);
        }

        #endregion
    }
}