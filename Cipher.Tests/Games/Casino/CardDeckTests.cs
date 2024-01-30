using Cipher.Games.Casino;

namespace Cipher.Tests.Games.Casino;

[TestClass]
public class CardDeckTests : TestBase
{
  private CardDeck Deck;
  private int CardNumberCount = 52;

  [TestMethod]
  public void CreateDeckTest()
  { 
    Deck = new CardDeck();
    Assert.AreEqual(52, Deck.Cards.Count);
  }

  [TestMethod]
  [DataRow(1)]
  [DataRow(2)]
  [DataRow(5)]
  [DataRow(100)]
  [DataRow(0)]
  [DataRow(-1)]
  public void CreateDeckWithMultipleDecksTest(int nDecksUsed)
  {
    Deck = new CardDeck(nDecksUsed);
    
    bool isDeckUsedValid = nDecksUsed >= 1;
    int expectedCardCount = isDeckUsedValid ? CardNumberCount * nDecksUsed : CardNumberCount;
    Assert.AreEqual(expectedCardCount, Deck.Cards.Count);
  }


  [TestMethod]
  public void TestCardDraw()
  { 
    Deck = new CardDeck(1);

    int cardCount = Deck.Cards.Count;
    Card c = Deck.DrawCard();

    int cardCountNew = Deck.Cards.Count;
    bool cardExists = Deck.Cards.Where(x => x.CardNumber == c.CardNumber && x.Suit == c.Suit).Count() > 0;

    Assert.IsTrue((cardCount == cardCountNew + 1) && !cardExists);
  }

  [TestMethod]
  [DataRow(1)]
  [DataRow(2)]
  [DataRow(5)]
  [DataRow(10)]
  [DataRow(0)]
  [DataRow(-1)]
  [DataRow(-int.MaxValue)]
  public void TestCardDeckReset(int nNumberOfDecks)
  { 
    CardDeck _deck = new CardDeck(nNumberOfDecks);
    
    if(nNumberOfDecks < 1)
      nNumberOfDecks = 1;

    Random r = new Random();
    int _x = r.Next(1, _deck.Cards.Count - 3);
    int startingDeckCardCount = _deck.Cards.Count;

    for (int x = 0; x < _x; x++)
      _deck.DrawCard();

    int deckCountAfterDraws = _deck.Cards.Count;
    _deck.Reset();

    int deckCountAfterReset = _deck.Cards.Count;

    Assert.IsTrue((startingDeckCardCount == nNumberOfDecks * CardNumberCount) &&
                  (deckCountAfterDraws < startingDeckCardCount) &&
                  (startingDeckCardCount == deckCountAfterReset));
  }
}
