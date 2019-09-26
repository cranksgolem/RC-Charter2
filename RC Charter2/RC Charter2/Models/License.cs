using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class License
	{
		public License()
		{
			CrewRequirements = new HashSet<CrewRequirement>();
		}

		public string LicenseType { get; set; }
		public string Description { get; set; }
		public decimal? HourlyCharge { get; set; }

		public ICollection<CrewRequirement> CrewRequirements { get; set; }
		public ICollection<Licensure> Licensures { get; set; }

		public override string ToString()
		{
			return Description;
		}
	}
}
