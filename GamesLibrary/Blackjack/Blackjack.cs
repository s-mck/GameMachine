using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary.Entities;
using DBLibrary.DAO;
using System.Configuration;

namespace GamesLibrary.Blackjack
{
    /// <summary>
    /// Subclass of Game. Provides general methods used to test the game status.
    /// </summary>
    public class Blackjack : Game
    {
        Deck deck;
        Dealer dealer;
        BJPlayer player;
        ResultDAO resultDAO;

        /// <summary>
        /// Sets all parameters to the class-level fields. Sets GameID to 'BLA'.
        /// </summary>
        /// <param name="deck">The deck to be used</param>
        /// <param name="dealer">The dealer of the game</param>
        /// <param name="player">The player of the game</param>
        public Blackjack(Deck deck, Dealer dealer, BJPlayer player)
        {
            this.deck = deck;
            this.dealer = dealer;
            this.player = player;
            GameID = "BLA";
        }

        /// <summary>
        /// Performs multiple checks using blackjack rules to determine if any player has won or busted.
        /// </summary>
        /// <param name="playerID">The player's ID. Needed to query the database.</param>
        /// <returns>A message to the player containing the result of the game.</returns>
        public string CheckWin(int playerID)
        {
            resultDAO = new ResultDAO(ConfigurationManager.ConnectionStrings["myConn"].ConnectionString);
           
            // If the player has busted, Dealer wins and game is over
            if (CheckHandStatus(player) == 0)
            {
                this.GameResult = DBLibrary.GameResult.AIWIN;
                resultDAO.AddResult(new Result(this.GameID, playerID, 0));
                return "You busted!";
            }
            // Dealer has busted, player wins and game is over
            else if (CheckHandStatus(dealer) == 0)
            {
                this.GameResult = DBLibrary.GameResult.PLAYERWIN;
                resultDAO.AddResult(new Result(this.GameID, playerID, 1));
                return "Dealer busted. You win!";
            }
            // If both dealer and player have completed their turns, check for highest hand
            else if (player.TurnComplete && dealer.TurnComplete) {

                // If player and dealer have the same hand value(including blackjacks), its a draw
                if (player.PlayerHand.HandValue == dealer.PlayerHand.HandValue)
                {
                    this.GameResult = DBLibrary.GameResult.PUSH;
                    return "It's a push!";
                }
                // Player's hand is higher than the dealers hand
                else if (player.PlayerHand.HandValue > dealer.PlayerHand.HandValue)
                {
                    this.GameResult = DBLibrary.GameResult.PLAYERWIN;
                    resultDAO.AddResult(new Result(this.GameID, playerID, 1));
                    return "You win!";
                }
                // Dealers hand is higher than players hand
                else if (dealer.PlayerHand.HandValue > player.PlayerHand.HandValue)
                {
                    this.GameResult = DBLibrary.GameResult.AIWIN;
                    resultDAO.AddResult(new Result(this.GameID, playerID, 0));
                    return "You lose!";
                }
                else
                    return null;
            }
            else
                return null;
        }

        /// <summary>
        /// Performs the initial deal of 2 cards. Used by both dealer and player.
        /// </summary>
        /// <returns>A new hand consisting of 2 cards.</returns>
        public Hand DealNewHand()
        {
            Card card1 = deck.Draw();
            deck.UpdateDeck(card1);

            Card card2 = deck.Draw();
            deck.UpdateDeck(card2);
            Hand hand = new Hand(card1, card2);
            return hand;
        }

        /// <summary>
        /// Checks the boolean properties of Bust, BlackjackWin, and Stand to determine the player's
        /// current hand status.
        /// </summary>
        /// <param name="player">Either human or dealer, since BJPlayer can hold a Dealer instance</param>
        /// <returns>0 if the 'player' has busted. 1 if the 'player' has blackjack/21. 2 if the 'player'
        /// has decided to stand. Otherwise returns -1</returns>
        public int CheckHandStatus(BJPlayer player)
        {
            // did player bust?
            if (player.PlayerHand.Bust)
                return 0;
            // does player have blackjack?
            else if (player.PlayerHand.BlackjackWin)
                return 1;
            // has player decided to stand?
            else if (player.Stand)
                return 2;
            // none of these applies, game will continue
            else
                return -1;
        }

    }
}
