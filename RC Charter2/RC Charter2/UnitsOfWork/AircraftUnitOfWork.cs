using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RC_Charter2.Models;
using RC_Charter2.Repositories;

namespace RC_Charter2.UnitsOfWork
{
	public class AircraftUnitOfWork : IDisposable
	{
		private RC_Charter2Context _context;
		private IRepository<Aircraft> AircraftRepository { get; set; }
		private IRepository<AircraftProperties> AircraftPropertiesRepository { get; set; }
		private IRepository<License> LicenseRepository { get; set; }
		private IRepository<CrewRequirement> CrewRequirementRepository { get; set; }

		public AircraftUnitOfWork()
		{
			_context = new RC_Charter2Context();
			AircraftRepository = new EfRepository<Aircraft>(_context);
			AircraftPropertiesRepository = new EfRepository<AircraftProperties>(_context);
			LicenseRepository = new EfRepository<License>(_context);
			CrewRequirementRepository = new EfRepository<CrewRequirement>(_context);
		}

		public void AddAircraft(Aircraft aircraft, AircraftProperties aircraftProperties)
		{
			aircraft.AircraftPropertiesId = aircraftProperties.AircraftPropertiesId;
			AircraftRepository.Add(aircraft);
			AircraftRepository.SaveChanges();
		}

		public void AddAircraftProperties(AircraftProperties aircraftProperties)
		{
			AircraftPropertiesRepository.Add(aircraftProperties);
			AircraftPropertiesRepository.SaveChanges();
		}

		public void AddCrewRequirement(CrewRequirement crewRequirement, AircraftProperties aircraftProperties, License license)
		{
			crewRequirement.AircraftPropertiesId = aircraftProperties.AircraftPropertiesId;
			crewRequirement.LicenseType = license.LicenseType;
			CrewRequirementRepository.Add(crewRequirement);
			CrewRequirementRepository.SaveChanges();
		}

		public void UpdateAircraft(Aircraft aircraft)
		{
			AircraftRepository.Update(aircraft);
			AircraftRepository.SaveChanges();
		}

		public async Task<IEnumerable<Aircraft>> GetAircraft(Expression<Func<Aircraft, bool>> query)
		{
			return await AircraftRepository.Query()
				.Where(query)
				.ToListAsync()
				.ConfigureAwait(false);
		}

		public async Task<IEnumerable<Aircraft>> GetAllAircraft()
		{
			return await GetAircraft(c => true).ConfigureAwait(false);
		}

		public IEnumerable<CrewRequirement> GetCrewRequirements(Expression<Func<CrewRequirement, bool>> query)
		{
			return CrewRequirementRepository.GetRange(query);
		}


		private bool _isDisposing;
		private bool _isDisposed;

		public void Dispose()
		{
			if (!_isDisposing)
			{
				_isDisposing = true;
				if (!_isDisposed)
				{
					_context?.Dispose();
					_isDisposed = true;
				}
			}
		}
	}
}
