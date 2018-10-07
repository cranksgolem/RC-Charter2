using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class Licensure
	{
		public int? LicensureId { get; set; }
		public string LicenseType { get; set; }
		public int? EmployeeId { get; set; }
		public DateTime? DateEarned { get; set; }

		public License License { get; set; }
		public Employee Employee { get; set; }
	}
}
