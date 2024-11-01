using Models;
using Models.DTOs;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction = Models.Transaction;

namespace Services.Implement
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepositoryBase<Transaction> _transRepo;
        private readonly IRepositoryBase<Booking> _bookingRepo;
        private readonly IRepositoryBase<Method> _methodRepo;

        public TransactionService(IRepositoryBase<Transaction> transRepo,
                                  IRepositoryBase<Booking> bookingRepo,
                                  IRepositoryBase<Method> methodRepo)
        {
            _transRepo = transRepo;
            _bookingRepo = bookingRepo;
            _methodRepo = methodRepo;
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
                TotalPrice = booking.ActualPrice,
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

        public async Task<List<Transaction>> GetAllTransaction()
        {
            var trans = await _transRepo.GetAllAsync();

            return trans;
        }

        public async Task<TransactionServiceResponse> GetTransactionById(int id)
        {
            if (id < 0)
            {
                return new TransactionServiceResponse { Success = false, Message = "TransactionId must be given." };
            }

            var transaction = await _transRepo.FindByIdAsync(id);

            if (transaction == null)
            {
                return new TransactionServiceResponse { Success = false, Message = "Unable to find transaction with " + id };
            }

            var method = await _methodRepo.FindByIdAsync(transaction.MethodId);
            transaction.Method = method;

            return new TransactionServiceResponse { Success = true, Transaction = transaction };
        }

        public async Task<TransactionServiceResponse> GetSuccessTransactionByBookingId(int id)
        {
            if (id < 0)
            {
                return new TransactionServiceResponse { Success = false, Message = "BookingId must be given." };
            }

            var transactions = await _transRepo.GetAllAsync();
            var successTrans = transactions.FirstOrDefault(t => t.BookingId == id && t.Status == 1);

            if (successTrans == null)
            {
                return new TransactionServiceResponse { Success = true, TransactionExists = false, Message = "There has been no successful transaction for this booking." };
            }

            var method = await _methodRepo.FindByIdAsync(successTrans.MethodId);
            successTrans.Method = method;

            return new TransactionServiceResponse { Success = true, TransactionExists = true, Transaction = successTrans };
        }

    }
}
