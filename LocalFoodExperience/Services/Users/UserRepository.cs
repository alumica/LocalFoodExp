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

namespace Services.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly LFEDbContext _context;
        private readonly IMemoryCache _memoryCache;


        public UserRepository(LFEDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<int> GetTotalUserAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<User>().CountAsync(cancellationToken);
        }

        public async Task<IPagedList<User>> GetPagedUserAsync(
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<User>()
                .ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<IPagedList<User>> GetPagedUserAsync(
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<User>()
                .ToPagedListAsync(
                    pageNumber,
                    pageSize,
                    nameof(User.Name),
                    "DESC", cancellationToken);
        }

        //public async Task<IPagedList<User>> GetPagedUserAsync(
        //    int menuId,
        //    int pageNumber = 1,
        //    int pageSize = 10,
        //    CancellationToken cancellationToken = default)
        //{
        //    return await _context.Set<User>()
        //        .Where(f => f.MenuId == menuId)
        //        .ToPagedListAsync(
        //            pageNumber,
        //            pageSize,
        //            nameof(User.Name),
        //            "DESC", cancellationToken);
        //}

        //public async Task<IPagedList<User>> GetPagedUserAsync(
        //    string slug,
        //    int pageNumber = 1,
        //    int pageSize = 10,
        //    CancellationToken cancellationToken = default)
        //{
        //    IQueryable<User> query = _context.Set<User>()
        //        .Include(f => f.Menu);

        //    return await query
        //        .Where(f => f.Menu.UrlSlug == slug)
        //        .ToPagedListAsync(
        //            pageNumber,
        //            pageSize,
        //            nameof(User.Name),
        //            "DESC", cancellationToken);
        //}

        //public async Task<IPagedList<T>> GetPagedUserAsync<T>(
        //    string slug,
        //    IPagingParams pagingParams,
        //    Func<IQueryable<User>, IQueryable<T>> mapper,
        //    CancellationToken cancellationToken = default)
        //{
        //    var UserList = _context.Set<User>().Include(f => f.Menu).Where(f => f.Menu.UrlSlug == slug);
        //    var mapperList = mapper(UserList);
        //    return await mapperList
        //        .ToPagedListAsync(pagingParams, cancellationToken);
        //}

        public async Task<User> GetUserByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Set<User>();
            return await query
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User> GetCachedUserByIdAsync(
            int userId,
            CancellationToken cancellationToken = default)
        {
            return await _memoryCache.GetOrCreateAsync(
                $"user.by-id.{userId}",
                async (entry) =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                    return await GetUserByIdAsync(userId, cancellationToken);
                });
        }

        public async Task<bool> AddOrUpdateUserAsync(
            User user,
            CancellationToken cancellationToken = default)
        {
            if (user.Id > 0)
            {
                _context.Users.Update(user);
                _memoryCache.Remove($"user.by-id.{user.Id}");
            }
            else
            {
                _context.Users.Add(user);
            }

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteUserByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var user = await _context.Set<User>().FindAsync(id);

            if (user is null) return false;

            _context.Set<User>().Remove(user);
            var rowsCount = await _context.SaveChangesAsync(cancellationToken);

            return rowsCount > 0;
        }

        public async Task<bool> SetImageUrlAsync(
            int id, string imageUrl,
            CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(a => a.ProfilePicture, a => imageUrl),
                    cancellationToken) > 0;
        }
    }
}
