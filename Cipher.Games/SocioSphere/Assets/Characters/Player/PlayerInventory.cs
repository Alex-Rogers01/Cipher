using Cipher.Games.SocioSphere.Assets.Items;
using Cipher.Games.SocioSphere.Utility;

namespace Cipher.Games.SocioSphere.Assets.Characters.Player;
public class PlayerInventory
{
  private Dictionary<IItem, int> Items { get; set; } = new Dictionary<IItem, int>();

  public PlayerInventory()
  { 
  }

  internal PlayerInventory(Dictionary<IItem, int> nItems) => Items = nItems;

  public void AddItem(IItem nItem, int nQuantity)
  { 
    if(Items.ContainsKey(nItem))
    { 
      Items[nItem] += nQuantity;
      return;
    }

    Items.Add(nItem, nQuantity);
  }

  public void RemoveItem(IItem nItem, int nQuantity)
  {
    if(Items.ContainsKey(nItem))
    {
      Items[nItem] -= nQuantity;
      if(Items[nItem] <= 0)
        Items.Remove(nItem);
    }
  }

  public void RemoveItem(IItem nItem)
  { 
    if(Items.ContainsKey(nItem))
      Items.Remove(nItem);
  }

  public void SwapItem(IItem nToRemove, IItem nToAdd, int nQuantity)
  { 
    RemoveItem(nToRemove);
    AddItem(nToAdd, nQuantity);
  }

  // By creating a deep copy, it prevents manipulation of the origional dictionary and allows for the origional to remain private
  public Dictionary<IItem, int> GetInventory() => Items.DeepClone();
}
