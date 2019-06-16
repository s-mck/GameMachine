using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary.Entities;

namespace GamesLibrary.Blackjack
{
    /// <summary>
    /// Contains a list of cards used by both player and dealer.
    /// </summary>
    public class Hand
    {
        List<Card> cardsInHand;
        int handValue;
        bool blackjackWin = false;
        bool bust = false;

        public List<Card> CardsInHand {
            get { return cardsInHand; }
            set
            {
                if (value.Count >= 1)
                    cardsInHand = value;
                else
                    throw new ArgumentException("Hand must have at least 1 cards");
            }
        }

        public int HandValue{
            get {return handValue;}
            set {handValue = value;}
        }

        public bool Bust {
            get { return bust; }
            set { bust = value; }
        }
        public bool BlackjackWin {
            get { return blackjackWin; }
            set { blackjackWin = value; }
        }

        /// <summary>
        /// Counts the value of each card and updates the HandValue.
        /// </summary>
        /// <param name="handDealt">Full initial hand of 2 cards in list form</param>
        public Hand(List<Card> handDealt)
        {
            CardsInHand = handDealt;

            foreach (Card card in handDealt)
                HandValue += card.Number;
        }

        /// <summary>
        /// Adds each card to the master card list. Updates HandValue.
        /// </summary>
        /// <param name="card1">First card dealt</param>
        /// <param name="card2">Second card dealt</param>
        public Hand(Card card1, Card card2)
        {
            cardsInHand = new List<Card>();
            CardsInHand.Add(card1);
            CardsInHand.Add(card2);

            HandValue += card1.Number;
            HandValue += card2.Number;
        }

        /// <summary>
        /// Swaps values of an ace from 11 to 1 or 1 to 11. Updates HandValue.
        /// </summary>
        /// <param name="card">The card to be checked</param>
        public void SwitchAceValue(Card card)
        {
            if (card.Number == 1)
            {
                card.Number = 11;
                handValue += 10;
            }
            else if(card.Number == 11)
            {
                card.Number = 1;
                handValue = handValue - 10;
            }
        }

        /// <summary>
        /// Adds a new card to the players CardsInHand.
        /// </summary>
        /// <param name="card">The card to be added to the hand.</param>
        public void AddCard(Card card)
        {
            cardsInHand.Add(card);
            HandValue += card.Number;
        }

        /// <summary>
        /// Checks if card is both ace and if the HandValue after being dealt the ace is greater than 21.
        /// Calls SwitchAceValue to swap value if conditions are met.
        /// </summary>
        /// <param name="card">The card to be checked</param>
        public void CheckAceAfterHit(Card card)
        {
            Card ace = card;

            if (ace.Face == "Ace" && HandValue > 21)
                SwitchAceValue(ace);
        }

        /// <summary>
        /// Checks each card in CardsInHand for aces. If HandValue is greater than 21, calls SwitchAceValue
        /// to swap the value. Used when aces previously dealt with a value of 11 now will result in a bust.
        /// </summary>
        public void CheckAcesInHand()
        {
            foreach(Card c in CardsInHand)
            {
                if (c.Face == "Ace" && HandValue > 21 && c.Number == 11)
                    SwitchAceValue(c);
            }
        }


    }
}
