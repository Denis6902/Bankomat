namespace cash_machine
{
    class Program
    {
        static void Main(string[] args)
        {
            ATM atm = new ATM("Automat", 1);
            atm.Start(); // zapnutí ATM
        }
    }
}