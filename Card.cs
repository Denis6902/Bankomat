using System;
using System.Collections.Generic;
using System.Linq;

namespace cash_machine
{
    class Card
    {
        /// <summary>
        /// ID bankovního účtu pro který je dělaná karta
        /// </summary>
        public int BankAccountId;

        /// <summary>
        /// Číslo karty (0123 4567 8901 2345)
        /// </summary>
        private long Number;

        /// <summary>
        /// Pin ke kartě
        /// </summary>
        private int Pin;

        /// <summary>
        /// Upravené číslo karty (0123 xxxx xxxx xxxx)
        /// </summary>
        public string EditedNumber;

        private Random random = new Random();

        /// <summary>
        /// Konstruktor. Náhodně vygeneruje číslo platební karty.
        /// </summary>
        /// <param name="bankAccountId">ID bankovního účtu pro který je dělaná karta </param>
        /// <param name="pin">PIN ke kartě</param>
        public Card(int bankAccountId, int pin)
        {
            BankAccountId = bankAccountId;
            Number = random.NextInt64(1000000000000000, 9999999999999999);
            Pin = pin;
            EditedNumber = Number.ToString().Substring(0, 4) + "************";
        }

        /// <summary>
        /// Kontrola jestli je pin zadaný uživatelem stejný jako pin od bankovního účtu
        /// </summary>
        /// <param name="pin">Pin</param>
        /// <param name="cards">Seznam karet</param>
        /// <returns>true/false</returns>
        public static bool CheckPin(int pin, List<Card> cards, int cardIndex)
        {
            return
                cards.Any(card =>
                    card.Pin == pin &&
                    cards[cardIndex].Pin ==
                    pin); // Vrátí true/false. Pokud najde kartu, kde se zvolený pin rovná pinu na zvolené kartě 
        }

        /// <summary>
        /// Vybere kartu, kterou chceme používat
        /// </summary>
        /// <param name="pin">Pin</param>
        /// <param name="cards">Seznam karet</param>
        /// <returns>danou kartu</returns>
        public static Card SelectCard(int pin, List<Card> cards)
        {
            return cards.First(card => card.Pin == pin); // vrátí první kartu, kde se zvolený pin rovná pinu na kartě
        }

        /// <summary>
        /// Metoda na změnu pinu
        /// </summary>
        public void ChangePin()
        {
            Console.WriteLine("Zadejte starý PIN");
            int oldPin = int.Parse(Console.ReadLine());

            if (oldPin == Pin) // jestli se starý (aktuální) Pin rovná pinu na kartě, dovolí změnu pinu
            {
                Console.WriteLine("Zadejte nový PIN");
                int newPin = int.Parse(Console.ReadLine());
                Pin = newPin; // nastaví nový pin na pin zadaný uživatelem
                Console.WriteLine("PIN je změněný");
            }
            else
            {
                Console.WriteLine("Špatný PIN");
            }

            Console.ReadKey();
        }
    }
}