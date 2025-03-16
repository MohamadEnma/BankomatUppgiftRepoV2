using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatUppgiftV2
{
    internal class TheBank
    {
        public string Name { get; set; }=string.Empty;
        public List<Account> Accounts { get; set; } = new List<Account>();
        private SelfServiceTerminal? terminal;
    }
}
