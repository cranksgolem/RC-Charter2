using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class Aircraft
	{
		public int? AircraftNumber { get; set; }
		public int? AircraftPropertiesId { get; set; }
		public float? ChargePerMile { get; set; }
		public float? HourlyWaitingCharge { get; set; }
		public string Model { get; set; }
		public int? NumberOfSeats { get; set; }
		public int? YearManufactured { get; set; }
		public float? Weight { get; set; }

		public AircraftProperties AircraftProperties { get; set; }
		public ICollection<CharterTrip> CharterTrips { get; set; }
	}
}
