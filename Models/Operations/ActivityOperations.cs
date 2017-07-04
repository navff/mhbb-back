using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camps.Tools;
using Models.Entities;
using System.Data.Entity;
using DelegateDecompiler;
using DelegateDecompiler.EntityFramework;
using Models.Tools;

namespace Models.Operations
{
    public class ActivityOperations
    {
        private HobbyContext _context;

        public ActivityOperations(HobbyContext context)
        {
            _context = context;
        }

        public Activity Get(int id)
        {
            try
            {
                return _context.Activities.Include(a => a.Organizer)
                    .Include(a => a.ActivityUserVoices)
                    .Decompile()
                    .FirstOrDefault(a => a.Id == id);
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
                                                             bool isChecked = true,
                                                             int page = 1)
        {
            try
            {
                IQueryable<Activity> result;

                if (!String.IsNullOrEmpty(word))
                {
                    result = _context.Activities
                        .Include(a => a.Organizer)
                        .Include(a => a.Interest)
                        .Include(a => a.ActivityUserVoices)
                        .Decompile()
                        .Where(a => (a.Name.Contains(word))
                                    || (a.Description.Contains(word))
                                    || (a.Organizer.Name.Contains(word)));
                    
                }
                else
                {
                    result = _context.Activities
                        .Include(a => a.Organizer)
                        .Include(a => a.Interest)
                        .Include(a => a.ActivityUserVoices)
                        .Decompile();
                }

                // убираем неактивированные
                result = result.Where(a => a.IsChecked == isChecked);

                // проходим по фильтрам
                if (age != null)
                {
                    result = result.Intersect(result.Where(a => (a.AgeFrom <= age.Value) && (a.AgeTo >= age.Value)));
                }

                if (interestId != null)
                {
                    result = result.Intersect(result.Where(a => a.InterestId == interestId.Value));
                }

                if (cityId != null)
                {
                    result = result.Intersect(result.Where(a => a.Organizer.CityId == cityId.Value));
                }

                if (sobriety != null)
                {
                    result = result.Intersect(result.Where(a => a.Organizer.Sobriety == sobriety.Value));
                }

                if (free != null)
                {
                    result = result.Intersect(result.Where(a => a.Free == free.Value));
                }

                return result.OrderBy(a => a.Voices < 0 ? 1 : 0)
                             .ThenByDescending(a => a.Voices)
                             .ThenBy(a => a.Name)
                             .Skip((page - 1) * ModelsSettings.PAGE_SIZE)
                             .Take(ModelsSettings.PAGE_SIZE).ToList();
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

        public async Task<Activity> SetChecked(int activityId, bool isChecked)
        {
            var activity = Get(activityId);
            activity.IsChecked = isChecked;
            await UpdateAsync(activity);
            return Get(activityId);
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
