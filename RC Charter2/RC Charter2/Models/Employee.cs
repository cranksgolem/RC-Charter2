using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class Employee
	{
		public Employee()
		{
			CrewAssignments = new HashSet<CrewAssignment>();
			Results = new HashSet<Result>();
			Licensures = new HashSet<Licensure>();
			CharterFlightCharges = new HashSet<CharterFlightCharge>();
		}

		public int? EmployeeId { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }

		public ICollection<CrewAssignment> CrewAssignments { get; set; }
		public ICollection<Result> Results { get; set; }
		public ICollection<Licensure> Licensures { get; set; }
		public ICollection<CharterFlightCharge> CharterFlightCharges { get; set; }
	}
}
