using Core.Contracts;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dishes
{
    public interface IDishRepository
    {
        Task<int> GetTotalDishAsync(
            CancellationToken cancellationToken = default);

        Task<IPagedList<Dish>> GetPagedDishAsync(
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default);

        Task<IPagedList<Dish>> GetPagedDishAsync(
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default);

        //Task<IPagedList<Dish>> GetPagedDishAsync(
        //    int menuId,
        //    int pageNumber = 1,
        //    int pageSize = 10,
        //    CancellationToken cancellationToken = default);

        //Task<IPagedList<Dish>> GetPagedDishAsync(
        //    string slug,
        //    int pageNumber = 1,
        //    int pageSize = 10,
        //    CancellationToken cancellationToken = default);

        //Task<IPagedList<T>> GetPagedDishAsync<T>(
        //    string slug,
        //    IPagingParams pagingParams,
        //    Func<IQueryable<Dish>, IQueryable<T>> mapper,
        //    CancellationToken cancellationToken = default);


        Task<Dish> GetDishByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<Dish> GetCachedDishByIdAsync(
            int dishId,
            CancellationToken cancellationToken = default);

        Task<bool> AddOrUpdateDishAsync(
            Dish dish,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteDishByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<bool> SetImageUrlAsync(
            int id, string imageUrl,
            CancellationToken cancellationToken = default);
    }
}
