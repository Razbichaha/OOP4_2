using System;
using System.Collections.Generic;

namespace OOP4_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            Game game = new Game();

            menu.OutputHeader();

            bool isContinueCycle = true;

            while (isContinueCycle)
            {
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "еще":

                        isContinueCycle = game.TakeCard();

                        break;
                    case "все":

                        game.StopGame();

                        break;
                    default:

                        menu.OutputAWarning();

                        break;
                }
            }
        }
    }

    class Menu
    {
        public void OutputHeader()
        {
            Console.WriteLine("Далее в игре присутствую следующие команды");
            Console.WriteLine("Получить карту - еще");
            Console.WriteLine("Остановить получение карт - все");
        }

        public void OutputAWarning()
        {
            Console.WriteLine("Введена не верная команда");
        }

        public void OutputCardsPlayer(Queue<string> cardsPlaer)
        {
            Console.WriteLine("\nВы выбрали карты");
            foreach (string card in cardsPlaer)
            {
                Console.WriteLine(card);
            }
        }

        public void OutputCardsPlayer(int sumCards)
        {
            Console.WriteLine("Ваши очки = " + sumCards);
        }
    }

    class Game
    {
        private Queue<string> _sufledDeck;
        private Queue<string> _cardsPlayer = new Queue<string>();

        Cards cards = new Cards();

        public Game()
        {
            _sufledDeck = new Queue<string>(cards.GetSufledDeckCards());
        }

        public bool TakeCard()
        {
            bool isContinueCycle = true;

            if (_sufledDeck.Count > 0)
            {
                string tempString = _sufledDeck.Dequeue();
                _cardsPlayer.Enqueue(tempString);
                Console.WriteLine(tempString);
            }
            else
            {
                Console.Write("Колода выдана полностью ");
                isContinueCycle = false;
            }

            return isContinueCycle;
        }

        Menu menu = new Menu();

        public void StopGame()
        {
            menu.OutputCardsPlayer(_cardsPlayer);
           int sum= cards.SumCardsPlayer(_cardsPlayer);
            menu.OutputCardsPlayer(sum);
        }
    }

    class Cards
    {
        private Dictionary<string, int> _cards = new Dictionary<string, int>();
        private List<string> _cardsList = new List<string>();

        public Cards()
        {
            GenerateCards();
        }

        private void GenerateCards()
        {
            int[] cardValue = { 6, 7, 8, 9, 10, 2, 3, 4, 11 };
            string[] nameCards = { "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };
            string[] cardSuit = { "Трефа", "Пика", "Черва", "Буба" };

            for (int i = 0; i < nameCards.Length; i++)
            {
                for (int j = 0; j < cardSuit.Length; j++)
                {
                    string temp = nameCards[i] + " " + cardSuit[j];
                    _cards.Add(temp, cardValue[i]);
                    _cardsList.Add(temp);
                }
            }
        }

        public List<string> GetSufledDeckCards()
        {
            string[] array = new string[_cardsList.Count];

            _cardsList.CopyTo(array);

            Random random = new Random();

            for (int i = array.Length - 1; i >= 1; i--)
            {
                int randomValue = random.Next(i + 1);
                string tempValue = array[randomValue];
                array[randomValue] = array[i];
                array[i] = tempValue;
            }

            List<string> tempList = new List<string>();
            tempList.AddRange(array);

            return tempList;
        }

        public int SumCardsPlayer(Queue<string> cardsPlaer)
        {
            int sumCards = 0;
            int cardsPlaerlength = cardsPlaer.Count;

            for (int i = 0; i <cardsPlaerlength ; i++)
            {
                int temp;

                _cards.TryGetValue(cardsPlaer.Dequeue(), out temp);

                sumCards += temp;
            }
            return sumCards;
        }
    }
}
