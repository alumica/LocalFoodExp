using Core.Entities;

namespace WebApi.Models.User
{
    public class UserEditModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int Type { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePicture { get; set; }

        public IFormFile ProfilePictureFile { get; set; }

        public string Bio { get; set; }

        public string Location { get; set; }

        public static async ValueTask<UserEditModel> BindAsync(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();
            return new UserEditModel()
            {
                Id = int.Parse(form["Id"]),
                Username = form["Username"],
                Email = form["Email"],
                Password = form["Password"],
                Type = int.Parse(form["Type"]),
                Name = form["Name"],
                PhoneNumber = form["PhoneNumber"],
                ProfilePictureFile = form.Files["ProfilePictureFile"],
                Bio = form["Bio"],
                Location = form["Location"],
            };
        }
    }
}
