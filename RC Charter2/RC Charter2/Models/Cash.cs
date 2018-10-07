using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class Cash
	{
		public int? CashId { get; set; }
		public int? PaymentModeId { get; set; }
		public decimal? Amount { get; set; }
		public DateTime? Date { get; set; }

		public PaymentMode PaymentMode { get; set; }
	}
}
