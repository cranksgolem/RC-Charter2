using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class CharterTrip
	{
		public CharterTrip()
		{
			CrewAssignments = new HashSet<CrewAssignment>();
			Flights = new HashSet<Flight>();
			CharterFlightCharges = new HashSet<CharterFlightCharge>();
			BalanceHistories = new HashSet<BalanceHistory>();
			PaymentModes = new HashSet<PaymentMode>();
			TotalCharge = 0;
			RemainingBalance = 0;
		}

		public int? CharterTripId { get; set; }
		public int? AircraftNumber { get; set; }
		public int? CustomerId { get; set; }
		public float? TotalDistanceFlown { get; set; }
		public float? TotalWaitingTime { get; set; }
		public float? TotalFuelUsage { get; set; }
		public string Origin { get; set; }
		public string FinalDestination { get; set; }
		public string Purpose { get; set; }
		public decimal? TotalCharge { get; set; }
		public decimal? RemainingBalance { get; set; }
		public DateTime? DateLastPaid { get; set; }

		public ICollection<CrewAssignment> CrewAssignments { get; set; }
		public ICollection<Flight> Flights { get; set; }
		public ICollection<CharterFlightCharge> CharterFlightCharges { get; set; }
		public ICollection<BalanceHistory> BalanceHistories { get; set; }
		public ICollection<PaymentMode> PaymentModes { get; set; }
		public Aircraft Aircraft { get; set; }
		public Customer Customer { get; set; }
	}
}
