using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Reservation : IEntity
    {
        public int Id { get; set; }

        public int TravelerId { get; set; }

        public User Traveler { get; set; }

        //public int HostId { get; set; }

        //public User Host { get; set; }

        //public DateTime Date { get; set; }

        //public TimeSpan Time { get; set; }

        //public int NumberOfGuests { get; set; }

        //public int Status { get; set; }

        //public string SpecialRequests { get; set; }

        //public string HostComments { get; set; }

        //public IList<ReservationDetail> ReservationDetails { get; set; }
    }
}
