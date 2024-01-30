using System.Configuration;

namespace Cipher.CommonDB;
public static class SqlManager
{
  public static string ConnectionString => ConfigurationManager.ConnectionStrings["DBConnStr"]?.ConnectionString;
}
