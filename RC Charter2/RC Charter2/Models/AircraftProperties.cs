using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class AircraftProperties
	{
		public int? AircraftPropertiesId { get; set; }
		public string AircraftType { get; set; }
		public string AircraftCategory { get; set; }
		public string EngineType { get; set; }
		public string AircraftWeight { get; set; }

		public ICollection<Aircraft> Aircraft { get; set; }
		public ICollection<CrewRequirement> CrewRequirements { get; set; }
	}
}
