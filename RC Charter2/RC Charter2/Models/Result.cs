using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class Result
	{
		public int? ResultId { get; set; }
		public string TestCode { get; set; }
		public int? EmployeeId { get; set; }
		public DateTime? TestDate { get; set; }
		public string TestResult { get; set; }
		public DateTime? Expiration { get; set; }

		public Test Test { get; set; }
		public Employee Employee { get; set; }
	}
}
