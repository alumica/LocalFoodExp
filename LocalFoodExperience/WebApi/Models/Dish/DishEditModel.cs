using Core.Entities;

namespace WebApi.Models.Dish
{
    public class DishEditModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string DietaryRestrictions { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }

        public string Photos { get; set; }

        public IFormFile PhotosFile { get; set; }

        public int HostId { get; set; }

        public int CategoryId { get; set; }

        public static async ValueTask<DishEditModel> BindAsync(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();
            return new DishEditModel()
            {
                Id = int.Parse(form["Id"]),
                Description = form["Description"],
                DietaryRestrictions = form["DietaryRestrictions"],
                Price = double.Parse(form["Price"]),
                Currency = form["Currency"],
                PhotosFile = form.Files["PhotosFile"],
                //HostId = int.Parse(form["HostId"]),
                //CategoryId = int.Parse(form["CategoryId"]),
            };
        }
    }
}
