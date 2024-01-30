namespace Cipher.Games.Casino;
public class Card
{
  public enum CardSuit
  {
    Clubs = 1,
    Diamonds = 2,
    Hearts = 3,
    Spades = 4
  }

  public enum CardValue
  { 
    ACE = 1, 
    TWO = 2, 
    THREE = 3,
    FOUR = 4,
    FIVE = 5,
    SIX = 6,
    SEVEN = 7,
    EIGHT = 8,
    NINE = 9,
    TEN = 10,
    JACK = 11,
    QUEEN = 12,
    KING = 13
  }

  public CardSuit Suit { get; init; }
  public int Value { get; init; }
  public CardValue CardNumber
  {
    get => (CardValue)Value;
  }

  private byte[,] CardImage;

  public Card(CardSuit nSuit, int nValue)
  { 
    this.Suit = nSuit;
    this.Value = nValue;
  }

  public Card(CardSuit nSuit, CardValue nValue) : this(nSuit, (int)nValue)
  { 
  }
}
