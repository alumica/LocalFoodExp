using Core.Contracts;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Reviews
{
    public interface IReviewRepository
    {
        Task<int> GetTotalReviewAsync(
            CancellationToken cancellationToken = default);

        Task<IPagedList<Review>> GetPagedReviewAsync(
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default);

        Task<IPagedList<Review>> GetPagedReviewAsync(
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default);


        Task<Review> GetReviewByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<Review> GetCachedReviewByIdAsync(
            int reviewId,
            CancellationToken cancellationToken = default);

        Task<bool> AddOrUpdateReviewAsync(
            Review review,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteReviewByIdAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}
