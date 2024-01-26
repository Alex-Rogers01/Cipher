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
}
