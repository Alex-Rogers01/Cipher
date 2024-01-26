using Cipher.Games.Casino;
using static Cipher.Games.Casino.Card;

namespace Cipher.Tests.Games.Casino;

[TestClass]
public class CardTests
{
  [TestMethod]
  public void TestCardCreationEnum()
  { 
    Card c = new Card(CardSuit.Clubs, CardValue.ACE);
    Assert.IsTrue(c.CardNumber == CardValue.ACE && c.Value == (int)CardValue.ACE && c.Suit == CardSuit.Clubs);
  }

  [TestMethod]
  public void TestCardNumberCreation()
  { 
    Card c = new Card((CardSuit)1, 1);
    Assert.IsTrue(c.CardNumber == (CardValue)1 && c.Value == 1 && c.Suit == (CardSuit)1);
  }
}
