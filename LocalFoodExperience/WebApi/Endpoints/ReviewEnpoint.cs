using Core.Collections;
using Core.Entities;
using MapsterMapper;
using Services.Media;
using System.Net;
using WebApi.Models;
using WebApi.Models.Review;
using Services.Reviews;

namespace WebApi.Endpoints
{
    public static class ReviewEnpoint
    {
        public static WebApplication MapReviewEndpoints(
            this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/reviews");

            routeGroupBuilder.MapGet("/", GetReviews)
                .WithName("GetReviews")
                .Produces<ApiResponse<PaginationResult<Review>>>();

            routeGroupBuilder.MapGet("/{id:int}", GetReviewDetails)
                .WithName("GetReviewById")
                .Produces<ApiResponse<ReviewDto>>();

            routeGroupBuilder.MapPost("/", AddReview)
                .WithName("AddNewReview")
                .Accepts<ReviewEditModel>("multipart/form-data")
                .Produces(401)
                .Produces<ApiResponse<Review>>();

            routeGroupBuilder.MapDelete("/{id:int}", DeleteReview)
                .WithName("DeleteAnReview")
                .Produces(401)
                .Produces<ApiResponse<string>>();

            return app;
        }

        private static async Task<IResult> GetReviews(
            [AsParameters] ReviewEditModel model,
            IReviewRepository reviewRepository)
        {
            var reviewList = await reviewRepository.GetPagedReviewAsync();
            var paginationResult = new PaginationResult<Review>(reviewList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> GetReviewDetails(
            int id,
            IReviewRepository reviewRepository,
            IMapper mapper)
        {
            var review = await reviewRepository.GetCachedReviewByIdAsync(id);

            return review == null
                ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy bài đánh giá có mã số {id}"))
                : Results.Ok(ApiResponse.Success(mapper.Map<ReviewDto>(review)));
        }

        private static async Task<IResult> AddReview(
            HttpContext context,
            IReviewRepository reviewRepository,
            IMediaManager mediaManager,
            IMapper mapper)
        {
            var model = await ReviewEditModel.BindAsync(context);

            var review = model.Id > 0 ? await reviewRepository.GetReviewByIdAsync(model.Id) : null;

            if (review == null)
            {
                review = new Review();
            }

            review.Rating = model.Rating;
            review.ReviewText = model.ReviewText;

            //if (model.ProfilePictureFile?.Length > 0)
            //{
            //    string hostname =
            //   $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}/",
            //    uploadedPath = await
            //   mediaManager.SaveFileAsync(model.ProfilePictureFile.OpenReadStream(), model.ProfilePictureFile.FileName,
            //    model.ProfilePictureFile.ContentType);
            //    if (!string.IsNullOrWhiteSpace(uploadedPath))
            //    {
            //        user.ProfilePicture = hostname + uploadedPath;
            //    }
            //}

            await reviewRepository.AddOrUpdateReviewAsync(review);

            return Results.Ok(ApiResponse.Success(
                mapper.Map<Review>(review), HttpStatusCode.Created));
        }

        private static async Task<IResult> DeleteReview(
            int id,
            IReviewRepository reviewRepository)
        {
            return await reviewRepository.DeleteReviewByIdAsync(id)
                ? Results.Ok(ApiResponse.Success("Bài đánh giá đã được xóa", HttpStatusCode.NoContent))
                : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, "Không thể tìm thấy bài đánh giá"));
        }
    }
}
