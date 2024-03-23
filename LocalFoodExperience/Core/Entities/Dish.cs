using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Dish : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string DietaryRestrictions { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }

        public string Photos { get; set; }

        public int HostId { get; set; }

        public int CategoryId { get; set; }

        //public User Host { get; set; }

        //public Category Category { get; set; }

        //public IList<ReservationDetail> ReservationDetails { get; set; }
    }
}
