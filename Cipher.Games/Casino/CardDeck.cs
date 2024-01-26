using static Cipher.Games.Casino.Card;

namespace Cipher.Games.Casino;
public class CardDeck
{
  public List<Card> Cards { get; private set; } = new List<Card>();

  public CardDeck(int nNumberOfDecksUsed = 1)
  { 
    if(nNumberOfDecksUsed < 1)
      nNumberOfDecksUsed = 1;

    for(int x = 0; x < nNumberOfDecksUsed; x++)
    { 
      for(int y = 0; y < 4; y++)
      { 
        for(int z = 0; z < 13; z++)
        { 
          Cards.Add(new Card((CardSuit)y + 1, z+1));
        }
      }
    }
  }

  public CardDeck() : this(1)
  {  
  }
}
