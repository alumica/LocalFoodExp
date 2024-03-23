using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Review : IEntity
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
