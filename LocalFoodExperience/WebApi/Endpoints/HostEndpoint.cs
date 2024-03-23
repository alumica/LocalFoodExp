using Core.Collections;
using Core.Entities;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Services.Media;
using Services.Hosts;
using System.Net;
using WebApi.Models.Host;
using WebApi.Models;

namespace WebApi.Endpoints
{
    public static class HostEndpoints
    {
        public static WebApplication MapHostEndpoints(
            this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/hosts");

            routeGroupBuilder.MapGet("/", GetHosts)
                .WithName("GetHosts")
                .Produces<ApiResponse<PaginationResult<Core.Entities.Host>>>();

            routeGroupBuilder.MapGet("/{id:int}", GetHostDetails)
                .WithName("GetHostById")
                .Produces<ApiResponse<HostDto>>();

            //routeGroupBuilder.MapGet(
            //       "/{slug:regex(^[a-z0-9_-]*$)}/foods",
            //       GetFoodsByMenuSlug)
            //   .WithName("GetFoodsByMenuSlug")
            //   .Produces<ApiResponse<PaginationResult<FoodDto>>>();

            routeGroupBuilder.MapPost("/", AddHost)
                .WithName("AddNewHost")
                .Accepts<HostEditModel>("multipart/form-data")
                .Produces(401)
                .Produces<ApiResponse<Core.Entities.Host>>();

            //routeGroupBuilder.MapPut("/{id:int}", UpdateMenu)
            //  .WithName("UpdateAnMenu")
            //  .Produces(401)
            //  .Produces<ApiResponse<string>>();

            routeGroupBuilder.MapDelete("/{id:int}", DeleteHost)
                .WithName("DeleteAnHost")
                .Produces(401)
                .Produces<ApiResponse<string>>();

            return app;
        }

        private static async Task<IResult> GetHosts(
            [AsParameters] HostFilterModel model,
            IHostRepository hostRepository)
        {
            var hostList = await hostRepository.GetPagedHostAsync();
            var paginationResult = new PaginationResult<Core.Entities.Host>(hostList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> GetHostDetails(
            int id,
            IHostRepository hostRepository,
            IMapper mapper)
        {
            var host = await hostRepository.GetCachedHostByIdAsync(id);

            return host == null
                ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy người dùng có mã số {id}"))
                : Results.Ok(ApiResponse.Success(mapper.Map<HostDto>(host)));
        }

        private static async Task<IResult> AddHost(
            HttpContext context,
            IHostRepository hostRepository,
            IMediaManager mediaManager,
            IMapper mapper)
        {
            var model = await HostEditModel.BindAsync(context);

            var host = model.Id > 0 ? await hostRepository.GetHostByIdAsync(model.Id) : null;

            if (host == null)
            {
                host = new Core.Entities.Host();
            }

            host.Name = model.Name;
            host.Description = model.Description;


            //if (model.ProfilePictureFile?.Length > 0)
            //{
            //    string hostname =
            //   $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}/",
            //    uploadedPath = await
            //   mediaManager.SaveFileAsync(model.ProfilePictureFile.OpenReadStream(), model.ProfilePictureFile.FileName,
            //    model.ProfilePictureFile.ContentType);
            //    if (!string.IsNullOrWhiteSpace(uploadedPath))
            //    {
            //        host.ProfilePicture = hostname + uploadedPath;
            //    }
            //}

            await hostRepository.AddOrUpdateHostAsync(host);

            return Results.Ok(ApiResponse.Success(
                mapper.Map<Core.Entities.Host>(host), HttpStatusCode.Created));
        }

        private static async Task<IResult> DeleteHost(
            int id,
            IHostRepository hostRepository)
        {
            return await hostRepository.DeleteHostByIdAsync(id)
                ? Results.Ok(ApiResponse.Success("Người dùng đã được xóa", HttpStatusCode.NoContent))
                : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, "Không thể tìm thấy người dùng"));
        }
    }
}
