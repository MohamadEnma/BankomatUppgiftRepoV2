using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatUppgiftV2
{
    internal class Account
    {
        public string FirstName { get; }
        public string LastName { get; }
        public int AccountNumber { get; }
        public decimal Balance { get; private set; }

        public Account(string firstName, string lastName, int accountNumber, decimal balance)
        {
            FirstName = firstName;
            LastName = lastName;
            AccountNumber = accountNumber;
            Balance = balance;
        }

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
            Balance -= amount;
        }

    }
}
