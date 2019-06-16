using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary.Entities;

namespace GamesLibrary.Blackjack
{
    /// <summary>
    /// Holds a list of cards which are dealt to players and dealers.
    /// </summary>
    public class Deck
    {
        List<Card> availableCards;
        List<string> faces;
        Random rGenerator;

        public List<Card> AvailableCards { get { return availableCards; } }

        /// <summary>
        /// Uses loops, Suits enum, and a List of string faces to populate the AvailableCards list.
        /// Shuffling is done by repeatedly using the Reverse method in various locations in the deck.
        /// </summary>
        public Deck()
        {
            availableCards = new List<Card>();
            faces = new List<string>();
            rGenerator = new Random();

            faces.Add("King");
            faces.Add("Queen");
            faces.Add("Jack");
            faces.Add("Ace");

            // adds cards numbered 2-10 for every suit (total 36 cards)
            for (int x = 2; x < 11; x++)
            {
                availableCards.Add(new Card(Suits.CLUB, x));
                availableCards.Add(new Card(Suits.HEART, x));
                availableCards.Add(new Card(Suits.DIAMOND, x));
                availableCards.Add(new Card(Suits.SPADE, x));
            }

            // Shuffle around the cards a bit
            availableCards.Reverse(0, 10);
            availableCards.Reverse(9, 5);
            availableCards.Reverse(20, 6);
            availableCards.Reverse(30, 5);

            // adds face cards (kings, queens, jacks, aces) for every suit (total 16 cards)
            foreach (string face in faces)
            {
                availableCards.Add(new Card(Suits.CLUB, face));
                availableCards.Add(new Card(Suits.HEART, face));
                availableCards.Add(new Card(Suits.DIAMOND, face));
                availableCards.Add(new Card(Suits.SPADE, face));
            }

            // Shuffle around the cards
            availableCards.Reverse();
            availableCards.Reverse(0, 30);
            availableCards.Reverse(30, 12);
            availableCards.Reverse(35, 10);
        }

        /// <summary>
        /// Uses a random number generator to select an index between 0 and the last index of the deck.
        /// </summary>
        /// <returns>The card at the randomly generated index of the deck.</returns>
        public Card Draw()
        {
            int numCardsInDeck = AvailableCards.Count;

            // Will give a random index between 0 and the last index of the current deck
            int randomInt = rGenerator.Next(0, numCardsInDeck - 1);

            return AvailableCards.ElementAt(randomInt);
        }

        /// <summary>
        /// After a card has been drawn, this method removes that card from AvailableCards list.
        /// </summary>
        /// <param name="card">The card that was dealt</param>
        public void UpdateDeck(Card card)
        {
            availableCards.Remove(card);
        }

        /// <summary>
        /// After the initial deal of 2 cards, this method removes those cards from the AvailableCards list.
        /// </summary>
        /// <param name="card1">First card dealt</param>
        /// <param name="card2">Second card dealt</param>
        public void UpdateDeck(Card card1, Card card2)
        {
            availableCards.Remove(card1);
            availableCards.Remove(card2);
        }

    }
}
