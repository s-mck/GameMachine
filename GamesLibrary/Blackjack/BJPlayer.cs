using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary.Entities;

namespace GamesLibrary.Blackjack
{
    /// <summary>
    /// Subclass of Player, Superclass of Dealer. Used for human players.
    /// </summary>
    public class BJPlayer : Player
    {
        Hand playerHand;
        const int bjack = 21;
        bool stand = false;

        public Hand PlayerHand {
            get { return playerHand; }
            set {
                if (value.GetType() == typeof(Hand))
                    playerHand = value;
                else
                    throw new ArgumentException("Value must be an instance of Hand");
            }
        }

        public bool Stand {
            get { return stand; }
            set {
                if (value.GetType() == typeof(bool))
                    stand = value;
                else
                    throw new ArgumentException("Stand must be true or false.");
            }
        }

        public bool TurnComplete { get; set; }

        /// <summary>
        /// Uses default settings from base class.
        /// </summary>
        /// <param name="id">The player's ID</param>
        /// <param name="name">The players full name</param>
        /// <param name="email">The player's email</param>
        public BJPlayer(int id, string name, string email) : base(id, name, email)
        {

        }

        /// <summary>
        /// Uses default settings from base class.
        /// </summary>
        /// <param name="name">The player's name</param>
        /// <param name="email">The player's email</param>
        public BJPlayer(string name, string email) : base(name, email)
        {

        }

        /// <summary>
        /// If player's hand is less than 21, a card will be added to the player's hand. After card is drawn,
        /// checks are made for a blackjack win or a bust.
        /// </summary>
        /// <param name="deck">The current deck/available cards being used.</param>
        /// <returns>1 if player's hand value is less than 21. 0 otherwise</returns>
        public virtual int Hit(Deck deck)
        {
            Card card;

            if (PlayerHand.HandValue < bjack)
            {
                card = deck.Draw();
                playerHand.AddCard(card);
                playerHand.CheckAceAfterHit(card);
                deck.UpdateDeck(card);

                PlayerHand.CheckAcesInHand();

                if (playerHand.HandValue == 21)
                {
                    PlayerHand.BlackjackWin = true;
                    Stand = true;
                    TurnComplete = true;
                    PlayerHand.Bust = false;
                }
                else if (playerHand.HandValue > 21)
                {
                    Stand = true;
                    TurnComplete = true;
                    PlayerHand.Bust = true;
                }
                return 1;
            }
            return 0;
        }

    }
}
