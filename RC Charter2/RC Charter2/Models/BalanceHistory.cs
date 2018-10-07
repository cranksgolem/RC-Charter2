using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class BalanceHistory
	{
		public int? BalanceHistoryId { get; set; }
		public int? CharterTripId { get; set; }
		public decimal? RemainingBalance { get; set; }
		public DateTime? DatePaid { get; set; }
		public string ModeOfPayment { get; set; }

		public CharterTrip CharterTrip { get; set; }
	}
}
