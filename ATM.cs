using System;

namespace cash_machine
{
    /// <summary>
    /// Třída bankomatu. 
    ///
    /// Startuje bankomat a prování základní operace.
    /// </summary>
    class ATM
    {
        /// <summary>
        /// Jméno bankomatu
        /// </summary>
        private string Name;

        /// <summary>
        /// ID bankomatu
        /// </summary>
        private int ID;
        

        public ATM(string name, int id)
        {
            Name = name;
            ID = id;
        }

        /// <summary>
        /// Zapíná bankomat
        /// </summary>
        public void Start()
        {
            // zde vytvoříme bankovní účet
            BankAccount bankAccount = new BankAccount("jmeno1", "user1");

            // zde zadáme a zkontrolujeme pin
            bankAccount.EnterPin();

            // zde máme všechny možnosti ATM
            Choices(bankAccount);
        }


        /// <summary>
        /// Jednotlivé možnosti ATM
        /// </summary>
        /// <param name="bankAccount"></param>
        private void Choices(BankAccount bankAccount)
        {
            bool loop = true;

            while (loop)
            {
                Menu();

                // načtení hodnoty určené uživatelem
                int choice = int.Parse(Console.ReadLine());

                // zde se rozhoduji podle jednotlivých akcí
                switch (choice)
                {
                    case 1:
                        bankAccount.getAmount();
                        break;
                    case 2:
                        bankAccount.Withdraw();
                        break;
                    case 3:
                        bankAccount.Deposit();
                        break;
                    case 4:
                        bankAccount.SelectedCard.ChangePin();
                        break;
                    default:
                        // Ukončení cyklu
                        loop = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Print menu items
        /// </summary>
        private void Menu()
        {
            Console.Clear();
            Console.WriteLine("Vítejte u bankomatu RoBeRt");
            Console.WriteLine("==========================");
            Console.WriteLine("1 - Zjistit zůstatek");
            Console.WriteLine("2 - Vybrat peníze");
            Console.WriteLine("3 - Vložit peníze");
            Console.WriteLine("4 - Změnit PIN");
            Console.WriteLine("5 - Ukončit");
            Console.WriteLine("**************************");
            Console.WriteLine("Vyber položku:");
        }

        /// <summary>
        /// Kontrola jestli je částka dělitelná 100.
        /// </summary>
        /// <param name="sum"></param>
        /// <returns>true/false</returns>
        public static bool Check100Divide(int sum)
        {
            return sum % 100 != 0;
        }
    }
}