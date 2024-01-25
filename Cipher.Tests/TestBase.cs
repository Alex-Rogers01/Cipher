using System.Configuration;
using System.Diagnostics;

namespace Cipher.Tests;
[TestClass]
public class TestBase
{
  protected string ConnectionString
  { 
    get => ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;
  }

  protected bool IsStringsSame(bool ignoreCase, params string[] stringsToCompare)
  {
    for (int x = 1; x < stringsToCompare.Length; x++)
    {
      if (string.Compare(stringsToCompare[x - 1], stringsToCompare[x], ignoreCase) != 0)
        return false;
    }
    return true;
  }
}