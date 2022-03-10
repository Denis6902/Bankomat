using System;
using System.Collections.Generic;

namespace cash_machine
{
    class BankAccount
    {
        /// <summary>
        /// ID bankovního účtu
        /// </summary>
        private int Id;

        /// <summary>
        /// Název bankovního účtu
        /// </summary>
        public string Name;

        /// <summary>
        /// Vlastník bankovního účtu
        /// </summary>
        public string Owner;

        /// <summary>
        /// Aktuálně vybráná karta
        /// </summary>
        public Card SelectedCard;

        /// <summary>
        /// Seznam všech karet
        /// </summary>
        private List<Card> cards = new();

        /// <summary>
        /// Seznam všech bankovních účtů
        /// </summary>
        private static List<BankAccount> bankAccounts = new();

        /// <summary>
        /// Výchozí zůstatek v bankovním účtu
        /// </summary>
        private int Amount = 1000;

        /// <summary>
        /// Konstruktor. Přidá bankovní účet do seznamu bankovních účtů.
        /// Automaticky nastaví ID o 1 větší než předchozí bankovní účet.
        /// </summary>
        /// <param name="name">Název bankovního účtu</param>
        /// <param name="owner">Vlastník bankovního účtu</param>
        public BankAccount(string name, string owner)
        {
            bankAccounts.Add(this);
            Id = bankAccounts.Count + 1;
            Name = name;
            Owner = owner;
            cards.Add(new Card(Id, 1111));
            cards.Add(new Card(Id, 2222));
            cards.Add(new Card(Id, 8888));
        }

        /// <summary>
        /// Metoda na zadání pinu od bankovního účtu
        /// </summary>
        public void EnterPin()
        {
            listAllAvailableCards(); // výpis všech karet k danému bankovnímu účtu

            Console.WriteLine("Jakou kartu chcete vybrat?");
            int cardIndex = Int32.Parse(Console.ReadLine()); // výběr dané karty se seznamu karet

            Console.WriteLine($"Zadej PIN ke kartě: ");
            string pinAsString = Console.ReadLine(); // načtení z konzole

            if (int.TryParse(pinAsString, out int pinAsInt)) // Zkusí převést string (pinAsString) na int (pinAsInt)
            {
                if (!Card.CheckPin(pinAsInt, cards,
                        cardIndex - 1)) // Jestli PIN jde převést na číslo, zkontroluje správnost zadaného pinu
                {
                    Console.WriteLine("Špatný pin");
                    Environment.Exit(0);
                }

                SelectedCard = Card.SelectCard(pinAsInt, cards); // nastaví aktuálně používanou kartu
                Console.WriteLine($"Správný pin ke kartě {SelectedCard.EditedNumber}");
                Console.ReadKey();
            }
            else // Jestli PIN nejde převést na číslo, spustí se znova metoda EnterPin()
            {
                EnterPin();
            }
        }

        private void listAllAvailableCards() // výpis všech karet k danému bankovnímu účtu
        {
            for (int index = 0;
                 index < cards.Count;
                 index++) // For cyklus, který vypíše všechny karty k danému bankovnímu účtu
            {
                Card card = cards[index];
                Console.WriteLine(
                    $"{index + 1}) Číslo karty {card.EditedNumber}. Název bankovního účtu - " +
                    $"{BankAccount.bankAccounts.Find(bc => bc.Id == card.BankAccountId).Name}");
            }
        }

        /// <summary>
        /// Zjištění zůstatku
        /// </summary>
        public void getAmount()
        {
            Console.WriteLine($"Na účtu je zůstatek: $ {Amount}");
            Console.WriteLine("------------------------------");
            Console.ReadKey();
        }

        /// <summary>
        /// Výběr z bankomatu
        /// </summary>
        public void Withdraw()
        {
            Console.WriteLine("Zadej částku (ve stovkách):");
            int withDraw = int.Parse(Console.ReadLine()); // načtení částky z konzole

            if (ATM.Check100Divide(withDraw)) // zkontroluje jestli je částka dělitelná 100
            {
                Console.WriteLine("Částka není dělitelná 100.");
            }
            else if (CheckWithDraw(withDraw)) // zkontroluje jestli je dostatečný zůstatek
            {
                Console.WriteLine("Nemáte dostatečný zůstatek.");
                Console.WriteLine($"Váš zůstatek je: $ {Amount}.");
            }
            else
            {
                RemoveFromDeposit(withDraw); // vybere peníze z účtu
                Console.WriteLine($"Zde jsou peníze: $ {withDraw}");
            }

            Console.WriteLine("------------------------------");
            Console.ReadKey();
        }

        /// <summary>
        /// Vložení peněz
        /// </summary>
        public void Deposit()
        {
            Console.WriteLine("Vložte částku:");
            int deposit = int.Parse(Console.ReadLine());

            if (ATM.Check100Divide(deposit)) // zkontroluje jestli je částka dělitelná 100
            {
                Console.WriteLine("Tyto peníze nelze příjmout!");
                Console.WriteLine("Vložte částku dělitelnou 100.");
            }
            else
            {
                AddToDeposit(deposit); // vloží peníže na účet
                Console.WriteLine("Vklad byl úspěšný.");
                Console.WriteLine($"Váš zůstatek nyní je: $ {Amount}.");
            }

            Console.WriteLine("------------------------------");
            Console.ReadKey();
        }

        /// <summary>
        /// Přičtení peněz k zůstatku
        /// </summary>
        /// <param name="sum">Částka k přičtení</param>
        public void AddToDeposit(int sum)
        {
            Amount += sum;
        }

        /// <summary>
        /// Kontrola jestli mu vybrat. Mám dostatečný zůstatek?
        /// </summary>
        /// <param name="withDraw">Částka k odečtení</param>
        /// <returns>Jestli mám dostatečný zůstatek</returns>
        public bool CheckWithDraw(int withDraw)
        {
            return Amount - withDraw < 0;
        }

        /// <summary>
        /// Odečtení ze současného zůstatku
        /// </summary>
        /// <param name="sum">Částka k odečtení</param>
        public void RemoveFromDeposit(int sum)
        {
            Amount -= sum;
        }
    }
}