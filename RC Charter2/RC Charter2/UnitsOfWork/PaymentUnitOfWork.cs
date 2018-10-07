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
		private IRepository<PaymentMode> PaymentModeRepository { get; set; }
		private IRepository<Check> CheckRepository { get; set; }
		private IRepository<Cash> CashRepository { get; set; }
		private IRepository<Customer> CustomerRepository { get; set; }
		private IRepository<CharterTrip> CharterTripRepository { get; set; }
		private IRepository<BalanceHistory> BalanceHistoryRepository { get; set; }

		public PaymentUnitOfWork()
		{
			_context = new RC_Charter2Context();
			PaymentModeRepository = new EfRepository<PaymentMode>(_context);
			CheckRepository = new EfRepository<Check>(_context);
			CashRepository = new EfRepository<Cash>(_context);
			CustomerRepository = new EfRepository<Customer>(_context);
			CharterTripRepository = new EfRepository<CharterTrip>(_context);
			BalanceHistoryRepository = new EfRepository<BalanceHistory>(_context);
		}

		public void AddPaymentMode(PaymentMode paymentMode, Customer customer, CharterTrip charterTrip, Cash cash)
		{
			paymentMode.CustomerId = customer.CustomerId;
			paymentMode.CharterTripId = charterTrip.CharterTripId;
			paymentMode.CashId = cash.CashId;
			cash.PaymentModeId = paymentMode.PaymentModeId;
			CashRepository.Update(cash);
			CashRepository.SaveChanges();
			PaymentModeRepository.Add(paymentMode);
			PaymentModeRepository.SaveChanges();
		}

		public void AddPaymentMode(PaymentMode paymentMode, Customer customer, CharterTrip charterTrip, Check check)
		{
			paymentMode.CustomerId = customer.CustomerId;
			paymentMode.CharterTripId = charterTrip.CharterTripId;
			paymentMode.CheckId = check.CheckId;
			check.PaymentModeId = paymentMode.PaymentModeId;
			CheckRepository.Update(check);
			CashRepository.SaveChanges();
			PaymentModeRepository.Add(paymentMode);
			PaymentModeRepository.SaveChanges();
		}

		public void UpdatePaymentMode(PaymentMode paymentMode)
		{
			PaymentModeRepository.Update(paymentMode);
			PaymentModeRepository.SaveChanges();
		}

		public async Task<IEnumerable<PaymentMode>> GetPaymentModes(Expression<Func<PaymentMode, bool>> query)
		{
			return await PaymentModeRepository.Query()
				.Where(query)
				.ToListAsync()
				.ConfigureAwait(false);
		}

		public async Task<IEnumerable<PaymentMode>> GetAllPaymentModes()
		{
			return await GetPaymentModes(c => true).ConfigureAwait(false);
		}

		public void AddBalanceHistory(BalanceHistory balanceHistory, CharterTrip charterTrip)
		{
			balanceHistory.CharterTripId = charterTrip.CharterTripId;
			BalanceHistoryRepository.Add(balanceHistory);
			BalanceHistoryRepository.SaveChanges();
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
