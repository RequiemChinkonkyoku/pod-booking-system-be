using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Services.Interface
{
    public interface ITransactionService
    {
        Task<Models.Transaction> CreateTransaction(int id, bool success);
    }
}
