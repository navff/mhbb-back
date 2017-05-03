using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camps.Tools;
using Models.Entities;
using System.Data.Entity;

namespace Models.Operations
{
    public class ActivityOperations
    {
        private HobbyContext _context;

        public ActivityOperations(HobbyContext context)
        {
            _context = context;
        }

        public async Task<Activity> GetAsync(int id)
        {
            try
            {
                return await _context.Activities.Include(a => a.Organizer).FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GET ACTIVITY", ex);
                throw;
            }
        }

        public async Task<IEnumerable<Activity>> SearchAsync(String word=null, 
                                                             int? age = null, 
                                                             int? interestId = null, 
                                                             int? cityId = null, 
                                                             bool? sobriety = null, 
                                                             bool? free = null, 
                                                             int? page = null)
        {
            try
            {
                // TODO: сделать нормальный поиск
                return _context.Activities
                                .Include(a => a.Organizer)
                                .Include(a => a.Interest)
                                .ToList();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT FIND ACTIVITIES", ex);
                throw;
            }
        }

        public async Task<Activity> AddAsync(Activity activity)
        {
            try
            {
                _context.Activities.Add(activity);
                await _context.SaveChangesAsync();
                return activity;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT ADD ACTIVITY", ex);
                throw;
            }
        }

        public async Task<Activity> UpdateAsync(Activity activity)
        {
            try
            {
                var activityInDb = _context.Activities.Find(activity.Id);
                if (activityInDb == null)
                {
                    throw new ArgumentException("There is no activity with this Id");
                }
                activityInDb.Name = activity.Name;
                activityInDb.Address = activity.Address;
                activityInDb.InterestId = activity.InterestId;
                activityInDb.AgeFrom = activity.AgeFrom;
                activityInDb.AgeTo = activity.AgeTo;
                activityInDb.Description = activity.Description;
                activityInDb.IsChecked = activity.IsChecked;
                activityInDb.Mentor = activity.Mentor;
                activityInDb.OrganizerId = activity.OrganizerId;
                activityInDb.Phones = activity.Phones;
                activityInDb.Prices = activity.Prices;

                await _context.SaveChangesAsync();
                return activityInDb;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT UPDATE ACTIVITY", ex);
                throw;
            }
        }

        public async Task DeleteAsync(int activityId)
        {
            try
            {
                var activity = _context.Activities.Find(activityId);
                if (activity == null)
                {
                    throw new ArgumentException("There is no activity with this Id");
                }
                _context.Activities.Remove(activity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT DELETE ACTIVITY", ex);
                throw;
            }
        }


    }
}
