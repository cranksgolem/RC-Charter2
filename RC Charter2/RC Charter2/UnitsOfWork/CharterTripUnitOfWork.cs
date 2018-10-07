using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RC_Charter2.Models;
using RC_Charter2.Repositories;

namespace RC_Charter2.UnitsOfWork
{
	public class CharterTripUnitOfWork : IDisposable
	{
		private RC_Charter2Context _context;
		private IRepository<CharterTrip> CharterTripRepository { get; set; }
		private IRepository<Flight> FlightRepository { get; set; }
		private IRepository<Aircraft> AircraftRepository { get; set; }
		private IRepository<CrewAssignment> CrewAssignmentRepository { get; set; }
		private IRepository<Employee> EmployeeRepository { get; set; }
		private IRepository<CharterFlightCharge> CharterFlightChargeRepository { get; set; }
		private IRepository<BalanceHistory> BalanceHistoryRepository { get; set; }

		public CharterTripUnitOfWork(IRepository<BalanceHistory> balanceHistoryRepository, IRepository<CharterTrip> charterTripRepository, IRepository<Flight> flightRepository, IRepository<Aircraft> aircraftRepository, IRepository<CrewAssignment> crewAssignmentRepository, IRepository<Employee> employeeRepository, IRepository<CharterFlightCharge> charterFlightChargeRepository)
		{
			CharterTripRepository = charterTripRepository;
			FlightRepository = flightRepository;
			AircraftRepository = aircraftRepository;
			CrewAssignmentRepository = crewAssignmentRepository;
			EmployeeRepository = employeeRepository;
			CharterFlightChargeRepository = charterFlightChargeRepository;
			BalanceHistoryRepository = balanceHistoryRepository;
		}

		public CharterTripUnitOfWork()
		{
			_context = new RC_Charter2Context();
			CharterTripRepository = new EfRepository<CharterTrip>(_context);
			FlightRepository = new EfRepository<Flight>(_context);
			AircraftRepository = new EfRepository<Aircraft>(_context);
			CrewAssignmentRepository = new EfRepository<CrewAssignment>(_context);
			EmployeeRepository = new EfRepository<Employee>(_context);
			CharterFlightChargeRepository = new EfRepository<CharterFlightCharge>(_context);
			BalanceHistoryRepository = new EfRepository<BalanceHistory>(_context);
		}

		public void AddCharterTrip(CharterTrip charterTrip, Aircraft aircraft, Customer customer)
		{
			charterTrip.AircraftNumber = aircraft.AircraftNumber;
			charterTrip.CustomerId = customer.CustomerId;
			CharterTripRepository.Add(charterTrip);
			CharterTripRepository.SaveChanges();
		}

		public void AddFlightToCharterTrip(Flight flight, CharterTrip charterTrip)
		{
			flight.CharterTripId = charterTrip.CharterTripId;
			FlightRepository.Add(flight);
			FlightRepository.SaveChanges();
		}

		public void AddCrewMember(CrewAssignment crewAssignment, Employee employee, CharterTrip charterTrip)
		{
			crewAssignment.EmployeeId = employee.EmployeeId;
			crewAssignment.CharterTripId = charterTrip.CharterTripId;
			CrewAssignmentRepository.Add(crewAssignment);
			CrewAssignmentRepository.SaveChanges();
		}

		public void AddCharterFlightCharge(CharterFlightCharge charterFlightCharge, CharterTrip charterTrip)
		{
			charterFlightCharge.CharterTripId = charterTrip.CharterTripId;
			CharterFlightChargeRepository.Add(charterFlightCharge);
			CharterFlightChargeRepository.SaveChanges();
		}

		public void AddBalanceHistory(BalanceHistory balanceHistory, CharterTrip charterTrip)
		{
			balanceHistory.CharterTripId = charterTrip.CharterTripId;
			BalanceHistoryRepository.Add(balanceHistory);
			BalanceHistoryRepository.SaveChanges();
		}

		public void UpdateCharterTrip(CharterTrip charterTrip)
		{
			CharterTripRepository.Update(charterTrip);
			CharterTripRepository.SaveChanges();
		}

		public async Task<IEnumerable<CharterTrip>> GetCharterTrips(Expression<Func<CharterTrip, bool>> query)
		{
			return await CharterTripRepository.Query()
				.Where(query)
				.ToListAsync()
				.ConfigureAwait(false);
		}

		public async Task<IEnumerable<CharterTrip>> GetAllCharterTrips()
		{
			return await GetCharterTrips(c => true).ConfigureAwait(false);
		}

		public IEnumerable<Flight> GetFlights(Expression<Func<Flight, bool>> query)
		{
			return FlightRepository.GetRange(query);
		}

		public IEnumerable<CharterFlightCharge> GetCharterFlightCharges(Expression<Func<CharterFlightCharge, bool>> query)
		{
			return CharterFlightChargeRepository.GetRange(query);
		}

		public IEnumerable<BalanceHistory> GetBalanceHistories(Expression<Func<BalanceHistory, bool>> query)
		{
			return BalanceHistoryRepository.GetRange(query);
		}

		public IEnumerable<CrewAssignment> GetCrewAssignments(Expression<Func<CrewAssignment, bool>> query)
		{
			return CrewAssignmentRepository.GetRange(query);
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
