using Newtonsoft.Json;

namespace Cipher.Games.SocioSphere.Utility;
public static class Utility
{
  public static Dictionary<T, U> DeepClone<T, U>(this Dictionary<T, U> nDict)
  {
    if (nDict == null)
      return null!;

    string _j = JsonConvert.SerializeObject(nDict);
    return (Dictionary<T, U>)JsonConvert.DeserializeObject(_j)!;
  }
}
