using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class Check
	{
		public int? CheckId { get; set; }
		public int? PaymentModeId { get; set; }
		public int? CheckNumber { get; set; }
		public DateTime? CheckDate { get; set; }
		public Decimal? Amount { get; set; }

		public PaymentMode PaymentMode { get; set; }
	}
}
