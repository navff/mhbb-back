using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camps.Tools;
using Models.Entities;

namespace Models.Operations
{
    public class ReviewOperations
    {
        private HobbyContext _context;

        public ReviewOperations(HobbyContext context)
        {
            _context = context;
        }

        public async Task<Review> GetAsync(int id)
        {
            try
            {
                return await _context.Reviews.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GET REVIEW", ex);
                throw;
            }
        }

        public async Task<Review> UpdateAsync(Review review)
        {
            try
            {
                var reviewInDb = await GetAsync(review.Id);
                reviewInDb.ActivityId = review.ActivityId;
                reviewInDb.IsChecked = review.IsChecked;
                reviewInDb.Text = review.Text;
                reviewInDb.ReplyToReviewId = review.ReplyToReviewId;
                await _context.SaveChangesAsync();
                return reviewInDb;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT UPDATE REVIEW", ex);
                throw;
            }
        }

        public async Task<Review> AddAsync(Review review)
        {
            try
            {
                review.DateCreated = DateTime.Now;
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
                return review;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT ADD REVIEW", ex);
                throw;
            }
        }


        public async Task DeleteAsync(int id)
        {
            var review = await _context.Reviews.FirstAsync(r => r.Id == id);
            var parentReviews = await _context.Reviews.Where(r => r.ReplyToReviewId == id).ToListAsync();

            foreach (var parentReview in parentReviews)
            {
                _context.Reviews.Remove(parentReview);
            }
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();


        }

        public async Task<IEnumerable<Review>> GetByUserEmailAsync(string userEmail)
        {
            return await _context.Reviews.Include(r => r.User.Picture).Where(r => r.User.Email == userEmail).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetByActivityAsync(int activityId)
        {
            try
            {
                return await _context.Reviews.Include(r => r.User.Picture).Where(r => r.ActivityId == activityId).ToListAsync();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GET REVIEWS", ex);
                throw;
            }
        }
    }
}
