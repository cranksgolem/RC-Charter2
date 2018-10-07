using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class CrewAssignment
	{
		public int? CrewAssignmentId { get; set; }
		public int?	EmployeeId { get; set; }
		public int? CharterTripId { get; set; }
		public DateTime? DateAssigned { get; set; }
		public string Role { get; set; }
		public float? WorkHours { get; set; }

		public Employee Employee { get; set; }
		public CharterTrip CharterTrip { get; set; }
	}
}
