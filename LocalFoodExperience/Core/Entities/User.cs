using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int Type { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePicture { get; set; }

        public string Bio { get; set; }

        public string Location { get; set; }

        //public IList<Dish> Dishes { get; set; }

        //public IList<Reservation> Reservations { get; set; }
    }
}
