using static Cipher.Games.Casino.Card;

namespace Cipher.Games.Casino;
public class CardDeck
{
  public List<Card> Cards { get; private set; } = new List<Card>();
  private int DecksUsed = 1;

  public CardDeck(int nNumberOfDecksUsed = 1)
  { 
    if(nNumberOfDecksUsed < 1)
      nNumberOfDecksUsed = 1;

    this.DecksUsed = nNumberOfDecksUsed;

    for(int x = 0; x < nNumberOfDecksUsed; x++)
    { 
      for(int y = 0; y < 4; y++)
      { 
        for(int z = 0; z < 13; z++)
        { 
          this.Cards.Add(new Card((CardSuit)y + 1, z+1));
        }
      }
    }
  }

  public CardDeck() : this(1)
  {  
  }

  public Card DrawCard()
  { 
    Random r = new Random();
    int index = r.Next(0, this.Cards.Count);

    Card c = this.Cards[index];
    this.Cards.RemoveAt(index);

    return c;
  }

  public void Reset(int nNewDecksAddedCount = 0)
  { 
    if(nNewDecksAddedCount > 0)
      DecksUsed = nNewDecksAddedCount;

    CardDeck newDeck = new CardDeck(DecksUsed);
    this.Cards = newDeck.Cards;
  }
}
