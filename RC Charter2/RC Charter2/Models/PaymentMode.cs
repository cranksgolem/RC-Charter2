using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class PaymentMode
	{
		public int? PaymentModeId { get; set; }
		public int? CustomerId { get; set; }
		public int? CharterTripId { get; set; }
		public int? CheckId { get; set; }
		public int? CashId { get; set; }
		public string ModeOfPayment { get; set; }

		public Customer Customer { get; set; }
		public CharterTrip CharterTrip { get; set; }
		public Check Check { get; set; }
		public Cash Cash { get; set; }
	}
}
