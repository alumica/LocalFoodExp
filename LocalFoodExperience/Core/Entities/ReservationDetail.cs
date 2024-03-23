using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ReservationDetail : IEntity
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }
        
        public int DishId { get; set; }

        //public Reservation Reservation { get; set; }

        //public Dish Dish { get; set; }

        public int NumberOfDishes { get; set; }

        public double Price { get; set; }

        public string Note { get; set; }
    }
}
