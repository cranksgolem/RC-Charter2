using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Models
{
	public class Test
	{
		public Test()
		{
			Results = new HashSet<Result>();
		}

		public string TestCode { get; set; }
		public string TestDescription { get; set; }
		public string TestFrequency { get; set; }

		public ICollection<Result> Results { get; set; }

		public override string ToString()
		{
			return TestDescription;
		}
	}
}
