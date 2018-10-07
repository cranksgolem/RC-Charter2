using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class CharterFlightCharge
	{
		public int? CharterFlightChargeId { get; set; }
		public int? CharterTripId { get; set; }
		public int? EmployeeId { get; set; }
		public string ChargeType { get; set; }
		public decimal? Amount { get; set; }
		public float? Quantity { get; set; }
	

		public CharterTrip CharterTrip { get; set; }
		public Employee Employee { get; set; }
	}
}
