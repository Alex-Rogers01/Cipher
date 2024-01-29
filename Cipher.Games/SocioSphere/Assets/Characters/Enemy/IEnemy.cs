using Cipher.Games.SocioSphere.Assets.Items;

namespace Cipher.Games.SocioSphere.Assets.Characters.Enemy;
public interface IEnemy
{
  public Dictionary<IItem, (double?, int)> DropTable { get; }

  public List<IItem> GetDroppedItems(int? nMaxItems);
}
