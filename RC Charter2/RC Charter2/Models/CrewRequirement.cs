using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class CrewRequirement
	{
		public int? CrewRequirementId { get; set; }
		public string LicenseType { get; set; }
		public int? AircraftPropertiesId { get; set; }
		public int? RequiredNumber { get; set; }

		public License License { get; set; }
		public AircraftProperties AircraftProperties { get; set; }
	}
}
