using Cipher.Games.SocioSphere.Assets.Abilities.Ability;

namespace Cipher.Games.SocioSphere.Assets.Items.Weapons;
public interface IWeapon : IItem, IStats
{
  public List<IAbility> AbilityLIst { get; }
}
