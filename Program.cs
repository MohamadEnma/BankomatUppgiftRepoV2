using System.Threading.Channels;

namespace BankomatUppgiftV2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TheBank bank = new TheBank();
            SelfServiceTerminal terminal = new SelfServiceTerminal(bank);
            while (true)
            {
                terminal.ShowMainMenu();
                terminal.HandleMainMenu();
            }
        }
    }
}
