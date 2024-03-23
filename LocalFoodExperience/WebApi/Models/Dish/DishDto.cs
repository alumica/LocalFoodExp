namespace WebApi.Models.Dish
{
    public class DishDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string ImageUrl { get; set; }

        public bool TypeUser { get; set; }
    }
}
