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

namespace Services.Hosts
{
    public class HostRepository : IHostRepository
    {
        private readonly LFEDbContext _context;
        private readonly IMemoryCache _memoryCache;


        public HostRepository(LFEDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<int> GetTotalHostAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Host>().CountAsync(cancellationToken);
        }

        public async Task<IPagedList<Host>> GetPagedHostAsync(
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Host>()
                .ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<IPagedList<Host>> GetPagedHostAsync(
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Host>()
                .ToPagedListAsync(
                    pageNumber,
                    pageSize,
                    nameof(Host.Name),
                    "DESC", cancellationToken);
        }

        public async Task<Host> GetHostByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Set<Host>();
            return await query
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Host> GetCachedHostByIdAsync(
            int hostId,
            CancellationToken cancellationToken = default)
        {
            return await _memoryCache.GetOrCreateAsync(
                $"host.by-id.{hostId}",
                async (entry) =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                    return await GetHostByIdAsync(hostId, cancellationToken);
                });
        }

        public async Task<bool> AddOrUpdateHostAsync(
            Host host,
            CancellationToken cancellationToken = default)
        {
            if (host.Id > 0)
            {
                _context.Hosts.Update(host);
                _memoryCache.Remove($"host.by-id.{host.Id}");
            }
            else
            {
                _context.Hosts.Add(host);
            }

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteHostByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var host = await _context.Set<Host>().FindAsync(id);

            if (host is null) return false;

            _context.Set<Host>().Remove(host);
            var rowsCount = await _context.SaveChangesAsync(cancellationToken);

            return rowsCount > 0;
        }

        public Task<bool> SetImageUrlAsync(int id, string imageUrl, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> SetImageUrlAsync(
        //    int id, string imageUrl,
        //    CancellationToken cancellationToken = default)
        //{
        //    return await _context.Hosts
        //        .Where(x => x.Id == id)
        //        .ExecuteUpdateAsync(x =>
        //            x.SetProperty(a => a.ProfilePicture, a => imageUrl),
        //            cancellationToken) > 0;
        //}
    }
}
