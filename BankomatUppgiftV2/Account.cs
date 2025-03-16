using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatUppgiftV2
{
    internal class Account(TheBank theBank, string firstName, string lastName, int accountNumber, decimal balance)
    {
        public TheBank TheBank { get; } = theBank;
        public string FirstName { get; } = firstName;
        public string LastName { get; } = lastName;
        public int AccountNumber { get; } = accountNumber;
        public decimal Balance { get; private set; } = balance;

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Insättningsbeloppet måste vara större än 0.");
            }
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Uttagsbeloppet måste vara större än 0.");
            }
            if (Balance < amount)
            {
                throw new ArgumentException("Det finns inte tillräckligt med pengar på kontot.");
            }
            Balance -= amount;
        }
    }
}
