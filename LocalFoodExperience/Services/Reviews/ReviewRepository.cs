using Core.Contracts;
using Core.Entities;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Reviews
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly LFEDbContext _context;
        private readonly IMemoryCache _memoryCache;


        public ReviewRepository(LFEDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<int> GetTotalReviewAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Review>().CountAsync(cancellationToken);
        }

        public async Task<IPagedList<Review>> GetPagedReviewAsync(
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Review>()
                .ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<IPagedList<Review>> GetPagedReviewAsync(
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Review>()
                .ToPagedListAsync(
                    pageNumber,
                    pageSize,
                    nameof(Review.DateCreated),
                    "DESC", cancellationToken);
        }


        public async Task<Review> GetReviewByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Set<Review>();
            return await query
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Review> GetCachedReviewByIdAsync(
            int reviewId,
            CancellationToken cancellationToken = default)
        {
            return await _memoryCache.GetOrCreateAsync(
                $"review.by-id.{reviewId}",
                async (entry) =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                    return await GetReviewByIdAsync(reviewId, cancellationToken);
                });
        }

        public async Task<bool> AddOrUpdateReviewAsync(
            Review review,
            CancellationToken cancellationToken = default)
        {
            if (review.Id > 0)
            {
                _context.Reviews.Update(review);
                _memoryCache.Remove($"review.by-id.{review.Id}");
            }
            else
            {
                _context.Reviews.Add(review);
            }

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteReviewByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var Review = await _context.Set<Review>().FindAsync(id);

            if (Review is null) return false;

            _context.Set<Review>().Remove(Review);
            var rowsCount = await _context.SaveChangesAsync(cancellationToken);

            return rowsCount > 0;
        }
    }
}
