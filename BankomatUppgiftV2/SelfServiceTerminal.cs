using System.Reflection;
using System.Text.RegularExpressions;

namespace BankomatUppgiftV2
{
    internal class SelfServiceTerminal
    {
        private TheBank? bank;

        public SelfServiceTerminal(TheBank? bank)
        {
            this.bank = bank;
        }

        public void ShowMainMenu()
        {
            Console.WriteLine("\n---------------- HUVUDMENY ----------------");
            Console.WriteLine("1: Registrera ett nytt konto");
            Console.WriteLine("2: Visa alla registrerade konton");
            Console.WriteLine("3: Kontoalternativ");
            Console.WriteLine("4: Avsluta applikationen");
            Console.Write("Ange ditt val (1-4): ");
        }

        private void AccountMenu()
        {
            Console.WriteLine("\n------------- KONTOMENY ------------");
            Console.WriteLine("1: Gör en insättning");
            Console.WriteLine("2: Gör ett uttag");
            Console.WriteLine("3: Visa saldo");
            Console.WriteLine("4: Gå tillbaka till huvudmenyn");
            Console.WriteLine("5: Avsluta applikationen");
            Console.Write("Ange ditt val (1-5): ");
        }

        public void HandleMainMenu()
        {
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 4)
            {
                Console.WriteLine("Felaktig inmatning. Ange ett tal mellan 1 och 4.");
                return;
            }

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    RegisterNewAccount();
                    break;
                case 2:
                    Console.Clear();
                    ReadAllRegAccounts();
                    break;
                case 3:
                    Console.Clear();
                    AccountMenu();
                    HandleAccountMenu();
                    break;
                case 4:
                    Console.Clear();
                    EndApp();
                    break;
                default:
                    Console.WriteLine("Okänt val, försök igen.");
                    break;
            }
        }

        private void HandleAccountMenu()
        {
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 5)
            {
                Console.WriteLine("Felaktig inmatning. Ange ett tal mellan 1 och 5.");
                return;
            }

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Deposit();
                    break;
                case 2:
                    Console.Clear();
                    Withdraw();
                    break;
                case 3:
                    Console.Clear();
                    ViewBalance();
                    break;
                case 4:
                    Console.Clear();
                    return;
                case 5:
                    Console.Clear();
                    EndApp();
                    break;
                default:
                    Console.WriteLine("Okänt val, försök igen.");
                    break;
            }
        }

        private void RegisterNewAccount()
        {
            Console.WriteLine("\n<------ Registrera ett nytt konto ------>");

            // Förnamn
            Console.Write("Ange förnamn: ");
            string? firstName = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(firstName) ||
                !Regex.IsMatch(firstName, "^[A-Åa-å]+$") ||
                firstName.Length > 30)
            {
                Console.WriteLine("Fel: Ange ett korrekt förnamn (endast bokstäver, max 30 tecken).");
                return;
            }

            // Efternamn
            Console.Write("Ange efternamn: ");
            string? lastName = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(lastName) ||
                !Regex.IsMatch(lastName, "^[A-Åa-å]+$") ||
                lastName.Length > 30)
            {
                Console.WriteLine("Fel: Ange ett korrekt efternamn (endast bokstäver, max 30 tecken).");
                return;
            }

            // Kontonummer
            Console.Write("Ange kontonummer: ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int accountNumber))
            {
                Console.WriteLine("Fel: Ange ett giltigt kontonummer (heltal).");
                return;
            }

            // Balance
            Console.Write("Ange startbelopp: ");
            if (!decimal.TryParse(Console.ReadLine()?.Trim(), out decimal balance))
            {
                Console.WriteLine("Fel: Ange ett giltigt belopp (decimaltal).");
                return;
            }

            // Skapa konto och lägg till i listan
            Account newAccount = new Account(bank!, firstName, lastName, accountNumber, balance);
            bank!.Accounts.Add(newAccount);
            Console.WriteLine("Kontot har registrerats framgångsrikt.");
        }

        private void ReadAllRegAccounts()
        {
            Console.WriteLine("\n----- Lista över registrerade konton -----");
            if (bank?.Accounts == null || bank.Accounts.Count == 0)
            {
                Console.WriteLine("Inga konton har registrerats än.");
                return;
            }
            else
            {
                foreach (var acc in bank.Accounts)
                {
                    Console.WriteLine($"Namn: {acc.FirstName} {acc.LastName} | Kontonummer: {acc.AccountNumber} | Saldo: {acc.Balance:C}");
                }
            }
        }

        private void Deposit()
        {
            if (bank?.Accounts == null || bank.Accounts.Count == 0)
            {
                Console.WriteLine("Inga konton har registrerats än.");
                return;
            }

            Console.Write("Ange kontonummer: ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int activeAccountNumber))
            {
                Console.WriteLine("Felaktigt kontonummer.");
                return;
            }

            var activeAccount = bank.Accounts.SingleOrDefault(x => x.AccountNumber == activeAccountNumber);
            if (activeAccount == null)
            {
                Console.WriteLine("Kontot hittades inte.");
                return;
            }

            Console.Write("Ange belopp att sätta in: ");
            if (!decimal.TryParse(Console.ReadLine()?.Trim(), out decimal depositAmount))
            {
                Console.WriteLine("Felaktigt belopp.");
                return;
            }

            try
            {
                activeAccount.Deposit(depositAmount);
                Console.WriteLine($"Insättning genomförd. Nytt saldo: {activeAccount.Balance:C}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Fel: " + ex.Message);
            }
        }

        private void Withdraw()
        {
            if (bank?.Accounts == null || bank.Accounts.Count == 0)
            {
                Console.WriteLine("Inga konton har registrerats än.");
                return;
            }

            Console.Write("Ange kontonummer: ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int activeAccountNumber))
            {
                Console.WriteLine("Felaktigt kontonummer.");
                return;
            }

            var activeAccount = bank.Accounts.SingleOrDefault(x => x.AccountNumber == activeAccountNumber);
            if (activeAccount == null)
            {
                Console.WriteLine("Kontot hittades inte.");
                return;
            }

            Console.Write("Ange belopp att ta ut: ");
            if (!decimal.TryParse(Console.ReadLine()?.Trim(), out decimal withdrawAmount))
            {
                Console.WriteLine("Felaktigt belopp.");
                return;
            }
            if (activeAccount.Balance < withdrawAmount)
            {
                Console.WriteLine("Det finns inte tillräckligt med pengar på kontot.");
                return;
            }
            if (withdrawAmount <= 0)
            {
                Console.WriteLine("Uttagsbeloppet måste vara större än 0.");
            }

            try
            {
                activeAccount.Withdraw(withdrawAmount);
                Console.WriteLine($"Insättning genomförd. Nytt saldo: {activeAccount.Balance:C}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Fel: " + ex.Message);
            }
        }

        private void ViewBalance()
        {
            if (bank?.Accounts == null || bank.Accounts.Count == 0)
            {
                Console.WriteLine("Inga konton har registrerats än.");
                return;
            }

            Console.Write("Ange kontonummer: ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int activeAccountNumber))
            {
                Console.WriteLine("Felaktigt kontonummer.");
                return;
            }

            var activeAccount = bank.Accounts.SingleOrDefault(x => x.AccountNumber == activeAccountNumber);
            if (activeAccount == null)
            {
                Console.WriteLine("Kontot hittades inte.");
                return;
            }
            Console.WriteLine($"Saldo är: {activeAccount.Balance:C} SEK");
        }

        private void EndApp()
        {
            Console.WriteLine("\nApplikationen avslutas. Tack för att du använde Bankomaten!");
            Environment.Exit(0);
        }
    }
}

