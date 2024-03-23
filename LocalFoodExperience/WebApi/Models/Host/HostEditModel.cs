

namespace WebApi.Models.Host
{
    public class HostEditModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Photos { get; set; }

        public static async ValueTask<HostEditModel> BindAsync(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();
            return new HostEditModel()
            {
                Id = int.Parse(form["Id"]),
                Name = form["Name"],
                Description = form["Description"],
            };
        }
    }
}
