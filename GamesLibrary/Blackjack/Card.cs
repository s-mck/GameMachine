using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary.Entities;

namespace GamesLibrary.Blackjack
{
    /// <summary>
    /// Holds values, faces, and images for cards used in Hand and Deck.
    /// </summary>
    public class Card
    {
        Suits suit;
        int number;
        string face;

        public Suits Suit {
            get { return suit; }
            private set {
                if (value.GetType() == typeof(Suits))
                    suit = value;
                else
                    throw new ArgumentException("Invalid data type.");
            }
        }
        public int Number {
            get { return number; }
            set {
                if (value > 0 && value < 12)
                    number = value;
                else
                    throw new ArgumentException("Card number must be between 1 and 11");
            }
        }

        /// <summary>
        /// Setter will update the Number property of the card when setting the Face value.
        /// </summary>
        public string Face {
            get { return face; }
            set {
                switch (value)
                {
                    case "King":
                        Number = 10;
                        face = value;
                        break;

                    case "Queen":
                        Number = 10;
                        face = value;
                        break;

                    case "Jack":
                        Number = 10;
                        face = value;
                        break;

                    // 11 will be the default value for Ace. Can be modified using method in Hand class.
                    case "Ace":
                        Number = 11;
                        face = value;
                        break;

                    default:
                        face = null;
                        break;
                }
            }
        }

        public string Image { get; set; }

        /// <summary>
        /// Sets the Suit and Number properties using parameters. Face is null since the card is not a face card.
        /// Image path is set using the newly set Suit and Number properties.
        /// </summary>
        /// <param name="suit">The suit of the card</param>
        /// <param name="number">The number on the card</param>
        public Card(Suits suit, int number)
        {
            Suit = suit;
            Number = number;
            Face = null;
            Image = $"../images/cards/{Suit.ToString().ToLower()}{Number}.png";
        }

        /// <summary>
        /// Sets the Suit and Face properties using parameters. Face property will set the Number property.
        /// Image path is set using the newly set Suit and Face properties.
        /// </summary>
        /// <param name="suit">The suit of the card</param>
        /// <param name="face">The face of the card</param>
        public Card(Suits suit, string face)
        {
            Suit = suit;
            Face = face;
            Image = $"../images/cards/{Suit.ToString().ToLower()}{Face}.png";
        }

        /// <summary>
        /// Compares the properties of the card passed with those of this instance to
        /// determine if they are the same card.
        /// </summary>
        /// <param name="card">The card to be compared</param>
        /// <returns>True if card is the same, false otherwise</returns>
        public bool Equals(Card card)
        {
            if (card.Number == Number && card.Suit == Suit && card.Face == Face)
                return true;
            else
                return false;
        }
    }
}
