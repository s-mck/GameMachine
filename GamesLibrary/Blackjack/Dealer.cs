using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary.Entities;

namespace GamesLibrary.Blackjack
{
    /// <summary>
    /// Subclass of BJPlayer. Acts as the opponent/AI in the game.
    /// </summary>
    public class Dealer : BJPlayer
    {
        const int dealerStand = 17;

        /// <summary>
        /// Sets the playerID to 0 to identify as AI.
        /// </summary>
        /// <param name="id">The ID </param>
        /// <param name="name">The dealer's name</param>
        /// <param name="email">The dealer's email</param>
        public Dealer(int id, string name, string email) : base(id, name, email)
        {
            PlayerID = 0;
        }

        /// <summary>
        /// Sets the playerID to 0 to identify as AI.
        /// </summary>
        /// <param name="name">The dealer's name</param>
        /// <param name="email">The dealer's email</param>
        public Dealer(string name, string email) : base(name, email)
        {
            PlayerID = 0;
        }

        /// <summary>
        /// Imitates dealer hitting until they reach at least 17. Performs checks for blackjack win, bust, and equal to
        /// 17. If HandValue is less than 17, dealer will continue to draw until it evaluates to false.
        /// </summary>
        /// <param name="deck">The current deck/available cards being used</param>
        /// <returns>0 if dealer has blackjack. 1 if dealer has busted. 2 if dealer has 17 exactly.
        /// 3 if dealer has hit at least once.</returns>
        public override int Hit(Deck deck)
        {
            Card card;

            if(PlayerHand.HandValue == 21)
            {
                PlayerHand.BlackjackWin = true;
                Stand = true;
                TurnComplete = true;
                PlayerHand.Bust = false;
                return 0;
            }
            else if (PlayerHand.HandValue > 21)
            {
                Stand = true;
                TurnComplete = true;
                PlayerHand.Bust = true;
                return 1;
            }
            else if(PlayerHand.HandValue == 17)
            {
                Stand = true;
                TurnComplete = true;
                return 2;
            }

            else
            {
                while (PlayerHand.HandValue < dealerStand)
                {
                    card = deck.Draw();
                    PlayerHand.AddCard(card);
                    PlayerHand.CheckAceAfterHit(card);
                    deck.UpdateDeck(card);
                }

                PlayerHand.CheckAcesInHand();

                if (PlayerHand.HandValue > 21)
                    PlayerHand.Bust = true;
                Stand = true;
                TurnComplete = true;
                return 3;
            }
        }
    }
}
