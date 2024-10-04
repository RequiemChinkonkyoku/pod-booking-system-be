using Models;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepositoryBase<Transaction> _transRepo;
        private readonly IRepositoryBase<Booking> _bookingRepo;

        public TransactionService(IRepositoryBase<Transaction> transRepo, IRepositoryBase<Booking> bookingRepo)
        {
            _transRepo = transRepo;
            _bookingRepo = bookingRepo;
        }

        public async Task<Transaction> CreateTransaction(int id, bool success)
        {
            var booking = await _bookingRepo.FindByIdAsync(id);

            if (booking == null)
            {
                return null;
            }

            var trans = new Transaction
            {
                PaymentTime = DateTime.UtcNow,
                TotalPrice = booking.BookingPrice,
                BookingId = id,
            };

            try
            {
                await _transRepo.AddAsync(trans);

                return trans;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
