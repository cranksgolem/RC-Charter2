﻿using System;
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
		private IRepository<Employee> _employeeRepository { get; set; }
		private IRepository<Test> _testRepository { get; set; }
		private IRepository<Result> _resultRepository { get; set; }
		private IRepository<License> _licenseRepository { get; set; }
		private IRepository<Licensure> _licensureRepository { get; set; }

		public EmployeeUnitOfWork()
		{
			_context = new RC_Charter2Context();
			_employeeRepository = new EfRepository<Employee>(_context);
			_testRepository = new EfRepository<Test>(_context);
			_resultRepository = new EfRepository<Result>(_context);
			_licenseRepository = new EfRepository<License>(_context);
			_licensureRepository = new EfRepository<Licensure>(_context);
		}

		public void AddEmployee(Employee employee)
		{
			_employeeRepository.Add(employee);
			_employeeRepository.SaveChanges();
		}

		public void UpdateEmployee(Employee employee)
		{
			_employeeRepository.Update(employee);
			_employeeRepository.SaveChanges();
		}

		public void DeleteEmployee(Employee employee)
		{
			_employeeRepository.Remove(employee);
			_employeeRepository.SaveChanges();
		}

		public void AddLicensure(Licensure licensure, Employee employee, License license)
		{
			licensure.EmployeeId = employee.EmployeeId;
			licensure.LicenseType = license.LicenseType;
			_licensureRepository.Add(licensure);
			_licensureRepository.SaveChanges();
		}

		public void AddResult(Result result, Employee employee, Test test)
		{
			result.EmployeeId = employee.EmployeeId;
			result.TestCode = test.TestCode;
			_resultRepository.Add(result);
			_resultRepository.SaveChanges();
		}

		public IEnumerable<Employee> GetEmployees(Expression<Func<Employee,bool>> query)
		{
			return _employeeRepository.GetRange(query);
		}

		public Employee GetEmployee(Expression<Func<Employee, bool>> query)
		{
			return _employeeRepository.Get(query);
		}

		public IEnumerable<Employee> GetAllEmployees()
		{
			return GetEmployees(c => true);
		}

		public int GetEmployeesCount()
		{
			return _employeeRepository.Query().Count();
		}

		public IEnumerable<Licensure> GetLicensures(Expression<Func<Licensure, bool>> query)
		{
			var licensures = _licensureRepository.GetRange(query);
			var licenses = new List<License>();

			foreach (var licensure in licensures)
			{
				licenses.Add(GetLicense(c => c.LicenseType == licensure.LicenseType));
			}

			return licensures;
		}

		public void UpdateLicensure(Licensure licensure)
		{
			_licensureRepository.Update(licensure);
			_licensureRepository.SaveChanges();
		}

		public void DeleteLicensure(Licensure licensure)
		{
			_licensureRepository.Remove(licensure);
			_licensureRepository.SaveChanges();
		}

		public License GetLicense(Expression<Func<License, bool>> query)
		{
			return _licenseRepository.Get(query);
		}

		public IEnumerable<License> GetAllLicenses()
		{
			return _licenseRepository.GetRange(c => true);
		}
		public Test GetTest(Expression<Func<Test, bool>> query)
		{
			return _testRepository.Get(query);
		}

		public IEnumerable<Test> GetAllTests()
		{
			return _testRepository.GetRange(c => true);
		}

		public IEnumerable<Result> GetResults(Expression<Func<Result, bool>> query)
		{
			var results = _resultRepository.GetRange(query);
			var tests = new List<Test>();

			foreach (var result in results)
			{
				tests.Add(GetTest(c => c.TestCode == result.TestCode));
			}

			return results;
		}

		public void UpdateResult(Result result)
		{
			_resultRepository.Update(result);
			_resultRepository.SaveChanges();
		}

		public void DeleteResult(Result result)
		{
			_resultRepository.Remove(result);
			_resultRepository.SaveChanges();
		}

		public IEnumerable<Employee> GetEmployeesByPage(int itemPerPage, int page)
		{
			return _employeeRepository.Query()
				.Skip(itemPerPage * (page - 1))
				.Take(itemPerPage)
				.ToList();
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
