using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class TransactionServiceResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public bool TransactionExists { get; set; }

        public Transaction Transaction { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
