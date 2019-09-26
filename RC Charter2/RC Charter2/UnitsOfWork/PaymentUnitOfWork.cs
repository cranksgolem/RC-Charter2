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
	public class PaymentUnitOfWork : IDisposable
	{
		private RC_Charter2Context _context;
		private IRepository<PaymentMode> _paymentModeRepository { get; set; }
		private IRepository<Check> _checkRepository { get; set; }
		private IRepository<Cash> _cashRepository { get; set; }
		private IRepository<Customer> _customerRepository { get; set; }
		private IRepository<CharterTrip> _charterTripRepository { get; set; }
		private IRepository<BalanceHistory> _balanceHistoryRepository { get; set; }

		public PaymentUnitOfWork()
		{
			_context = new RC_Charter2Context();
			_paymentModeRepository = new EfRepository<PaymentMode>(_context);
			_checkRepository = new EfRepository<Check>(_context);
			_cashRepository = new EfRepository<Cash>(_context);
			_customerRepository = new EfRepository<Customer>(_context);
			_charterTripRepository = new EfRepository<CharterTrip>(_context);
			_balanceHistoryRepository = new EfRepository<BalanceHistory>(_context);
		}

		public void AddPaymentMode(PaymentMode paymentMode)
		{
			_paymentModeRepository.Add(paymentMode);
			_paymentModeRepository.SaveChanges();
		}

		public void UpdatePaymentMode(PaymentMode paymentMode)
		{
			_paymentModeRepository.Update(paymentMode);
			_paymentModeRepository.SaveChanges();
		}

		public void DeletePaymentMode(PaymentMode paymentMode)
		{
			_paymentModeRepository.Remove(paymentMode);
			_paymentModeRepository.SaveChanges();
		}

		public void DeleteCheck(Check check)
		{
			_checkRepository.Remove(check);
			_checkRepository.SaveChanges();
		}

		public void DeleteCash(Cash cash)
		{
			_cashRepository.Remove(cash);
			_cashRepository.SaveChanges();
		}

		public IEnumerable<Cash> GetCash(Expression<Func<Cash, bool>> query)
		{
			return _cashRepository.GetRange(query);
		}

		public Cash GetSingleCash(Expression<Func<Cash, bool>> query)
		{
			return _cashRepository.Get(query);
		}

		public void AddCash(Cash cash)
		{
			_cashRepository.Add(cash);
			_cashRepository.SaveChanges();
		}

		public void UpdateCash(Cash cash)
		{
			_cashRepository.Update(cash);
			_cashRepository.SaveChanges();
		}

		public void UpdateCheck(Check check)
		{
			_checkRepository.Update(check);
			_checkRepository.SaveChanges();
		}

		public void AddPaymentModeIdToCash(Cash cash, PaymentMode paymentMode)
		{
			cash.PaymentModeId = paymentMode.PaymentModeId;
			_cashRepository.Update(cash);
			_cashRepository.SaveChanges();
		}

		public void AddCheck(Check check)
		{
			_checkRepository.Add(check);
			_checkRepository.SaveChanges();
		}

		public void AddPaymentModeIdToCheck(Check check, PaymentMode paymentMode)
		{
			check.PaymentModeId = paymentMode.PaymentModeId;
			_checkRepository.Update(check);
			_checkRepository.SaveChanges();
		}

		public IEnumerable<Check> GetChecks(Expression<Func<Check, bool>> query)
		{
			return _checkRepository.GetRange(query);
		}

		public Check GetCheck(Expression<Func<Check, bool>> query)
		{
			return _checkRepository.Get(query);
		}

		public IEnumerable<PaymentMode> GetPaymentModes(Expression<Func<PaymentMode, bool>> query)
		{
			var paymentModes = _paymentModeRepository.GetRange(query);
			var cash = new List<Cash>();
			var checks = new List<Check>();

			foreach (var paymentMode in paymentModes)
			{
				if (paymentMode.ModeOfPayment == "Cash")
				{
					cash.Add(GetSingleCash(c => c.PaymentModeId == paymentMode.PaymentModeId));
				}
				else if (paymentMode.ModeOfPayment == "Check")
				{
					checks.Add(GetCheck(c => c.PaymentModeId == paymentMode.PaymentModeId));
				}
			}
			return paymentModes;
		}

		public IEnumerable<PaymentMode> GetAllPaymentModes()
		{
			return GetPaymentModes(c => true);
		}

		public void AddBalanceHistory(BalanceHistory balanceHistory, CharterTrip charterTrip)
		{
			balanceHistory.CharterTripId = charterTrip.CharterTripId;
			_balanceHistoryRepository.Add(balanceHistory);
			_balanceHistoryRepository.SaveChanges();
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
