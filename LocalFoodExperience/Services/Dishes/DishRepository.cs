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

namespace Services.Dishes
{
    public class DishRepository : IDishRepository
    {
        private readonly LFEDbContext _context;
        private readonly IMemoryCache _memoryCache;


        public DishRepository(LFEDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<int> GetTotalDishAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Dish>().CountAsync(cancellationToken);
        }

        public async Task<IPagedList<Dish>> GetPagedDishAsync(
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Dish>()
                .ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<IPagedList<Dish>> GetPagedDishAsync(
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Dish>()
                .ToPagedListAsync(
                    pageNumber,
                    pageSize,
                    nameof(Dish.Name),
                    "DESC", cancellationToken);
        }

        //public async Task<IPagedList<Dish>> GetPagedDishAsync(
        //    int menuId,
        //    int pageNumber = 1,
        //    int pageSize = 10,
        //    CancellationToken cancellationToken = default)
        //{
        //    return await _context.Set<Dish>()
        //        .Where(f => f.MenuId == menuId)
        //        .ToPagedListAsync(
        //            pageNumber,
        //            pageSize,
        //            nameof(Dish.Name),
        //            "DESC", cancellationToken);
        //}

        //public async Task<IPagedList<Dish>> GetPagedDishAsync(
        //    string slug,
        //    int pageNumber = 1,
        //    int pageSize = 10,
        //    CancellationToken cancellationToken = default)
        //{
        //    IQueryable<Dish> query = _context.Set<Dish>()
        //        .Include(f => f.Menu);

        //    return await query
        //        .Where(f => f.Menu.UrlSlug == slug)
        //        .ToPagedListAsync(
        //            pageNumber,
        //            pageSize,
        //            nameof(Dish.Name),
        //            "DESC", cancellationToken);
        //}

        //public async Task<IPagedList<T>> GetPagedDishAsync<T>(
        //    string slug,
        //    IPagingParams pagingParams,
        //    Func<IQueryable<Dish>, IQueryable<T>> mapper,
        //    CancellationToken cancellationToken = default)
        //{
        //    var dishList = _context.Set<Dish>().Include(f => f.Menu).Where(f => f.Menu.UrlSlug == slug);
        //    var mapperList = mapper(dishList);
        //    return await mapperList
        //        .ToPagedListAsync(pagingParams, cancellationToken);
        //}

        public async Task<Dish> GetDishByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Set<Dish>();
            return await query
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Dish> GetCachedDishByIdAsync(
            int disId,
            CancellationToken cancellationToken = default)
        {
            return await _memoryCache.GetOrCreateAsync(
                $"dish.by-id.{disId}",
                async (entry) =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                    return await GetDishByIdAsync(disId, cancellationToken);
                });
        }

        public async Task<bool> AddOrUpdateDishAsync(
            Dish dish,
            CancellationToken cancellationToken = default)
        {
            if (dish.Id > 0)
            {
                _context.Dishes.Update(dish);
                _memoryCache.Remove($"dish.by-id.{dish.Id}");
            }
            else
            {
                _context.Dishes.Add(dish);
            }

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteDishByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var dish = await _context.Set<Dish>().FindAsync(id);

            if (dish is null) return false;

            _context.Set<Dish>().Remove(dish);
            var rowsCount = await _context.SaveChangesAsync(cancellationToken);

            return rowsCount > 0;
        }

        public async Task<bool> SetImageUrlAsync(
            int id, string imageUrl,
            CancellationToken cancellationToken = default)
        {
            return await _context.Dishes
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(a => a.Photos, a => imageUrl),
                    cancellationToken) > 0;
        }
    }
}
