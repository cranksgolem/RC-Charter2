using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class Flight
	{
		public int? FlightId { get; set; }
		public int? CharterTripId { get; set; }
		public DateTime? Date { get; set; }
		public DateTime? TimeDeparture { get; set; }
		public DateTime? TimeArrival { get; set; }
		public string Origin { get; set; }
		public string Destination { get; set; }
		public float? DistanceFlown { get; set; }
		public int? WaitingTime { get; set; }
		public float? FuelUsage { get; set; }
		public int? Order { get; set; }

		public CharterTrip CharterTrip { get; set; }
	}
}
