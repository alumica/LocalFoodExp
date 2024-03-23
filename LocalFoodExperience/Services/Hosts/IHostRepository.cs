using Core.Contracts;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Hosts
{
    public interface IHostRepository
    {
        Task<int> GetTotalHostAsync(
            CancellationToken cancellationToken = default);

        Task<IPagedList<Host>> GetPagedHostAsync(
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default);

        Task<IPagedList<Host>> GetPagedHostAsync(
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default);

        Task<Host> GetHostByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<Host> GetCachedHostByIdAsync(
            int hostId,
            CancellationToken cancellationToken = default);

        Task<bool> AddOrUpdateHostAsync(
            Host host,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteHostByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<bool> SetImageUrlAsync(
            int id, string imageUrl,
            CancellationToken cancellationToken = default);
    }
}
