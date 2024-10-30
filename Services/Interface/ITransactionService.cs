using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Transaction = Models.Transaction;

namespace Services.Interface
{
    public interface ITransactionService
    {
        public Task<Transaction> CreateTransaction(int id, bool success);
        public Task<List<Transaction>> GetAllTransaction();
        public Task<TransactionServiceResponse> GetSuccessTransactionByBookingId(int id);
        public Task<TransactionServiceResponse> GetTransactionById(int id);
    }
}
