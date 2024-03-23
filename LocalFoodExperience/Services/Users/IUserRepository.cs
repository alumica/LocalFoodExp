using Core.Contracts;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
    public interface IUserRepository
    {
        Task<int> GetTotalUserAsync(
            CancellationToken cancellationToken = default);

        Task<IPagedList<User>> GetPagedUserAsync(
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default);

        Task<IPagedList<User>> GetPagedUserAsync(
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default);

        //Task<IPagedList<User>> GetPagedUserAsync(
        //    int menuId,
        //    int pageNumber = 1,
        //    int pageSize = 10,
        //    CancellationToken cancellationToken = default);

        //Task<IPagedList<User>> GetPagedUserAsync(
        //    string slug,
        //    int pageNumber = 1,
        //    int pageSize = 10,
        //    CancellationToken cancellationToken = default);

        //Task<IPagedList<T>> GetPagedUserAsync<T>(
        //    string slug,
        //    IPagingParams pagingParams,
        //    Func<IQueryable<User>, IQueryable<T>> mapper,
        //    CancellationToken cancellationToken = default);


        Task<User> GetUserByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<User> GetCachedUserByIdAsync(
            int userId,
            CancellationToken cancellationToken = default);

        Task<bool> AddOrUpdateUserAsync(
            User user,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteUserByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<bool> SetImageUrlAsync(
            int id, string imageUrl,
            CancellationToken cancellationToken = default);
    }
}
