using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RC_Charter2.Models;
using RC_Charter2.UnitsOfWork;

namespace RC_Charter2
{
	class Program
	{
		static void Main(string[] args)
		{
		}

		public void TestAddCharterTrip()
		{
			using (var uw = new CharterTripUnitOfWork())
			{
				var newCharterTrip = new CharterTrip();

				var aircraftToAssign = uw.AircraftRepository.Get(c => c.AircraftNumber == 1);
			}

		}
	}
}
