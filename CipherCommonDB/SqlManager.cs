using System.Configuration;
using System.Data.SqlClient;

namespace CipherCommonDB;
public static class SqlManager
{
  public static string ConnectionString => ConfigurationManager.ConnectionStrings["DBConnStr"]?.ConnectionString;
}
