using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatUppgiftV2
{
    internal class Transction
    {
        public DateTimeOffset TransactionDate = DateTimeOffset.Now;
        public string TypOfTransction {  get; set; } = string.Empty;

        public decimal TransctionAmount { get; set; }

        public override string? ToString()
        {
            return $"Datum: {TransactionDate}, Typ av transaktion {TypOfTransction}, Belopp {TransctionAmount}";
        }
    }
}
