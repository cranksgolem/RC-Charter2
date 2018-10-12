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
		private IRepository<Aircraft> _aircraftRepository { get; set; }
		private IRepository<AircraftProperties> _aircraftPropertiesRepository { get; set; }
		private IRepository<License> _licenseRepository { get; set; }
		private IRepository<CrewRequirement> _crewRequirementRepository { get; set; }

		public AircraftUnitOfWork()
		{
			_context = new RC_Charter2Context();
			_aircraftRepository = new EfRepository<Aircraft>(_context);
			_aircraftPropertiesRepository = new EfRepository<AircraftProperties>(_context);
			_licenseRepository = new EfRepository<License>(_context);
			_crewRequirementRepository = new EfRepository<CrewRequirement>(_context);
		}

		public void AddAircraft(Aircraft aircraft, AircraftProperties aircraftProperties)
		{
			aircraft.AircraftPropertiesId = aircraftProperties.AircraftPropertiesId;
			_aircraftRepository.Add(aircraft);
			_aircraftRepository.SaveChanges();
		}

		public void AddAircraftProperties(AircraftProperties aircraftProperties)
		{
			_aircraftPropertiesRepository.Add(aircraftProperties);
			_aircraftPropertiesRepository.SaveChanges();
		}

		public void AddCrewRequirement(CrewRequirement crewRequirement, AircraftProperties aircraftProperties, License license)
		{
			crewRequirement.AircraftPropertiesId = aircraftProperties.AircraftPropertiesId;
			crewRequirement.LicenseType = license.LicenseType;
			_crewRequirementRepository.Add(crewRequirement);
			_crewRequirementRepository.SaveChanges();
		}

		public void UpdateAircraft(Aircraft aircraft)
		{
			_aircraftRepository.Update(aircraft);
			_aircraftRepository.SaveChanges();
		}

		public IEnumerable<Aircraft> GetAircraft(Expression<Func<Aircraft, bool>> query)
		{
			return _aircraftRepository.GetRange(query);
		}

		public Aircraft GetSingleAircraft(Expression<Func<Aircraft, bool>> query)
		{
			return _aircraftRepository.Get(query);
		}

		public IEnumerable<Aircraft> GetAllAircraft()
		{
			return GetAircraft(c => true);
		}

		public License GetLicense(Expression<Func<License, bool>> query)
		{
			return _licenseRepository.Get(query);
		}

		public IEnumerable<CrewRequirement> GetCrewRequirements(Expression<Func<CrewRequirement, bool>> query)
		{
			return _crewRequirementRepository.GetRange(query);
		}

		public AircraftProperties GetAircraftProperties(Expression<Func<AircraftProperties, bool>> query)
		{
			return _aircraftPropertiesRepository.Get(query);
		}

		public IEnumerable<Aircraft> GetAircraftByPage(int itemPerPage, int page)
		{
			return _aircraftRepository.Query()
				.Skip(itemPerPage * (page - 1))
				.Take(itemPerPage)
				.ToList();
		}

		public int GetAircraftCount()
		{
			return _aircraftRepository.Query().Count();
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
