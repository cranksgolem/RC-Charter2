using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class Customer
	{
		public Customer()
		{
			UsedCredits = 0;
		}

		public int? CustomerId { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public decimal? AvailableCredits { get; set; }
		public decimal? UsedCredits { get; set; }

		public ICollection<CharterTrip> CharterTrips { get; set; }
		public ICollection<PaymentMode> PaymentModes { get; set; }
	}
}
