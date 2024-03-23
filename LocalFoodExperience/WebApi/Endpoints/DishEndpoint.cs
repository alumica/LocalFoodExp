using Core.Collections;
using Core.Entities;
using MapsterMapper;
using Services.Media;
using Services.Dishes;
using System.Net;
using WebApi.Models;
using WebApi.Models.Dish;

namespace WebApi.Endpoints
{
    public static class DishEndpoints
    {
        public static WebApplication MapDishEndpoints(
            this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/dishes");

            routeGroupBuilder.MapGet("/", GetDishes)
                .WithName("GetDishes")
                .Produces<ApiResponse<PaginationResult<Dish>>>();

            routeGroupBuilder.MapGet("/{id:int}", GetDishDetails)
                .WithName("GetDishById")
                .Produces<ApiResponse<DishDto>>();

            //routeGroupBuilder.MapGet(
            //       "/{slug:regex(^[a-z0-9_-]*$)}/foods",
            //       GetFoodsByMenuSlug)
            //   .WithName("GetFoodsByMenuSlug")
            //   .Produces<ApiResponse<PaginationResult<FoodDto>>>();

            routeGroupBuilder.MapPost("/", AddDish)
                .WithName("AddNewDish")
                .Accepts<DishEditModel>("multipart/form-data")
                .Produces(401)
                .Produces<ApiResponse<Dish>>();

            //routeGroupBuilder.MapPut("/{id:int}", UpdateMenu)
            //  .WithName("UpdateAnMenu")
            //  .Produces(401)
            //  .Produces<ApiResponse<string>>();

            routeGroupBuilder.MapDelete("/{id:int}", DeleteDish)
                .WithName("DeleteAnDish")
                .Produces(401)
                .Produces<ApiResponse<string>>();

            return app;
        }

        private static async Task<IResult> GetDishes(
            [AsParameters] DishFilterModel model,
            IDishRepository dishRepository)
        {
            var foodList = await dishRepository.GetPagedDishAsync();
            var paginationResult = new PaginationResult<Dish>(foodList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> GetDishDetails(
            int id,
            IDishRepository dishRepository,
            IMapper mapper)
        {
            var dish = await dishRepository.GetCachedDishByIdAsync(id);

            return dish == null
                ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy món ăn có mã số {id}"))
                : Results.Ok(ApiResponse.Success(mapper.Map<DishDto>(dish)));
        }

        private static async Task<IResult> AddDish(
            HttpContext context,
            IDishRepository dishRepository,
            IMediaManager mediaManager,
            IMapper mapper)
        {
            var model = await DishEditModel.BindAsync(context);

            var dish = model.Id > 0 ? await dishRepository.GetDishByIdAsync(model.Id) : null;

            if (dish == null)
            {
                dish = new Dish();
            }

            dish.Name = model.Name;
            dish.Description = model.Description;
            dish.DietaryRestrictions = model.DietaryRestrictions;
            dish.Price = model.Price;
            dish.Currency = model.Currency;
            dish.Photos = model.Photos;
            dish.HostId = model.HostId;
            dish.CategoryId = model.CategoryId;

            if (model.PhotosFile?.Length > 0)
            {
                string hostname =
               $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}/",
                uploadedPath = await
               mediaManager.SaveFileAsync(model.PhotosFile.OpenReadStream(), model.PhotosFile.FileName,
                model.PhotosFile.ContentType);
                if (!string.IsNullOrWhiteSpace(uploadedPath))
                {
                    dish.Photos = hostname + uploadedPath;
                }
            }

            await dishRepository.AddOrUpdateDishAsync(dish);

            return Results.Ok(ApiResponse.Success(
                mapper.Map<Dish>(dish), HttpStatusCode.Created));
        }

        //private static async Task<IResult> UpdateMenu(
        //    int id,
        //    MenuEditModel model,
        //    IValidator<MenuEditModel> validator,
        //    IMenuRepository menuRepository,
        //    IMapper mapper)
        //{
        //    var validationResult = await validator.ValidateAsync(model);
        //    if (!validationResult.IsValid)
        //    {
        //        return Results.Ok(ApiResponse.Fail(
        //        HttpStatusCode.BadRequest, validationResult));
        //    }

        //    if (await menuRepository.IsMenuSlugExistedAsync(id, model.UrlSlug))
        //    {
        //        return Results.Ok(ApiResponse.Fail(
        //            HttpStatusCode.Conflict,
        //            $"Slug '{model.UrlSlug}' đã được sử dụng"));
        //    }

        //    var menu = mapper.Map<Menu>(model);
        //    menu.Id = id;

        //    return await menuRepository.AddOrUpdateMenuAsync(menu)
        //        ? Results.Ok(ApiResponse.Success("Thực đơn đã được cập nhật", HttpStatusCode.NoContent))
        //        : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, "Không thể tìm thấy thực đơn"));
        //}

        private static async Task<IResult> DeleteDish(
            int id,
            IDishRepository dishRepository)
        {
            return await dishRepository.DeleteDishByIdAsync(id)
                ? Results.Ok(ApiResponse.Success("Món ăn đã được xóa", HttpStatusCode.NoContent))
                : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, "Không thể tìm thấy món ăn"));
        }
    }
}
