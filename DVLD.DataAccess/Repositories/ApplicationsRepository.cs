using DVLD.DataAccess.Context;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.DataAccess.Repositories
{
    public class ApplicationsRepository : IApplicationsRepository
    {
        private readonly DvldDbContext _context;

        public async Task<bool> AddAsync(Application entity)
        {
            if (entity is null)
            {
                return false;
            }
            var Application = new Application
            {
                ApplicantPersonID = entity.ApplicantPersonID,
                ApplicationDate = entity.ApplicationDate,
                ApplicationTypeID = entity.ApplicationTypeID,
                ApplicationStatus = entity.ApplicationStatus,
                LastStatusDate = entity.LastStatusDate,
                PaidFees = entity.PaidFees,
                CreatedByUserID = entity.CreatedByUserID
            };
            
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Applications.Select(x=>x.ApplicationID).CountAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var application = await _context.Applications.FirstOrDefaultAsync(x=>x.ApplicationID == id);
            if (application is null){return false;}
            _context.Applications.Remove(application);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(int applicantPersonId, int applicationTypeId)
        {
            var application = await  _context.Applications.AnyAsync(x=>x.ApplicantPersonID == applicantPersonId && x.ApplicationTypeID==applicationTypeId);
            return application !=null;
            
        }

        public async Task<IEnumerable<Application>> GetAllAsync()
        {
            return await _context?.Applications?.ToListAsync();
        }

        public async Task<Application> GetByApplicantPersonIdAsync(int personId)
        {
           var Person=await _context.Applications.FirstOrDefaultAsync(x=>x.ApplicantPersonID == personId);
            if (Person is null)
            {return null;}

            return Person;
        }

        public async Task<Application> GetByApplicationTypeIdAsync(int applicationTypeId)
        {
            var Person = await _context.Applications.FirstOrDefaultAsync(x => x.ApplicationTypeID ==applicationTypeId);
            if (Person is null)
            { return null; }

            return  Person;
        }

        public async Task<IEnumerable<Application>> AllApplicationsForUserId(int userId)
        {
            var AllApplications=await _context.Applications.Where(X=>X.CreatedByUserID==userId).ToListAsync();
            return AllApplications;
        }

        public async Task<Application?> GetByIdAsync(int id)
        {
            var Person = await _context.Applications.FirstOrDefaultAsync(x => x.ApplicationID == id);
            if (Person is null)
            { return null; }

            return Person;
        }

        public Task<List<Application>> GetByStatusAsync(byte applicationStatus)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Application entity)
        {
            throw new NotImplementedException();
        }

        Task<Application> IApplicationsRepository.GetByStatusAsync(byte applicationStatus)
        {
            throw new NotImplementedException();
        }
    }
}
