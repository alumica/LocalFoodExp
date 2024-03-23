
namespace WebApi.Models.Review
{
    public class ReviewEditModel
    {
        public int Id { get; set; }

        public int TravelerId { get; set; }

        public int DishId { get; set; }

        //public User Traveler { get; set; }

        //public Dish Dish { get; set; }

        public int Rating { get; set; }

        public string ReviewText { get; set; }

        public DateTime DateCreated { get; set; }

        public static async ValueTask<ReviewEditModel> BindAsync(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();
            return new ReviewEditModel()
            {
                Id = int.Parse(form["Id"]),
                Rating = int.Parse(form["Rating"]),
                ReviewText = form["ReviewText"],
                //DateCreated = form["ReviewText"],
            };
        }
    }
}
