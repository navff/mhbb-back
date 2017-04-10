using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;

namespace Models.Operations
{
    public class ReviewOperations
    {
        private AppContext _context;

        public ReviewOperations(AppContext context)
        {
            _context = context;
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
            return await _context.Reviews.Where(r => r.User.Email == userEmail).ToListAsync();
        }
    }
}
