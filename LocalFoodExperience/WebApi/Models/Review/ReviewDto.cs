namespace WebApi.Models.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public int TravelerId { get; set; }

        public int DishId { get; set; }

        //public User Traveler { get; set; }

        //public Dish Dish { get; set; }

        public int Rating { get; set; }

        public string ReviewText { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
