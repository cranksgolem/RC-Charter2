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
	public class EmployeeUnitOfWork : IDisposable
	{
		private RC_Charter2Context _context;
		private IRepository<Employee> EmployeeRepository { get; set; }
		private IRepository<Test> TestRepository { get; set; }
		private IRepository<Result> ResultRepository { get; set; }
		private IRepository<License> LicenseRepository { get; set; }
		private IRepository<Licensure> LicensureRepository { get; set; }

		public EmployeeUnitOfWork()
		{
			_context = new RC_Charter2Context();
			EmployeeRepository = new EfRepository<Employee>(_context);
			TestRepository = new EfRepository<Test>(_context);
			ResultRepository = new EfRepository<Result>(_context);
			LicenseRepository = new EfRepository<License>(_context);
			LicensureRepository = new EfRepository<Licensure>(_context);
		}

		public void AddEmployee(Employee employee)
		{
			EmployeeRepository.Add(employee);
			EmployeeRepository.SaveChanges();
		}

		public void UpdateEmployee(Employee employee)
		{
			EmployeeRepository.Update(employee);
			EmployeeRepository.SaveChanges();
		}

		public void AddLicensure(Licensure licensure, Employee employee, License license)
		{
			licensure.EmployeeId = employee.EmployeeId;
			licensure.LicenseType = licensure.LicenseType;
			LicensureRepository.Add(licensure);
			LicensureRepository.SaveChanges();
		}

		public void AddResult(Result result, Employee employee, Test test)
		{
			result.EmployeeId = employee.EmployeeId;
			result.TestCode = test.TestCode;
			ResultRepository.Add(result);
			ResultRepository.SaveChanges();
		}

		public async Task<IEnumerable<Employee>> GetEmployees(Expression<Func<Employee,bool>> query)
		{
			return await EmployeeRepository.Query()
				.Where(query)
				.ToListAsync()
				.ConfigureAwait(false);
		}

		public async Task<IEnumerable<Employee>> GetAllEmployees()
		{
			return await GetEmployees(c => true).ConfigureAwait(false);
		}

		public IEnumerable<Licensure> GetLicensures(Expression<Func<Licensure, bool>> query)
		{
			return LicensureRepository.GetRange(query);
		}

		public IEnumerable<Result> GetResults(Expression<Func<Result, bool>> query)
		{
			return ResultRepository.GetRange(query);
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
