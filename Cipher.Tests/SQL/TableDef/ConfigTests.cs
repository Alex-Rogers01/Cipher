using CipherCommonDB.TableDef;
using System.Data.SqlClient;

namespace Cipher.Tests.SQL.TableDef;

[TestClass]
public class ConfigTests : TestBase
{
  [TestMethod]
  [DataRow("DiscordToken")]
  [DataRow("NOT_A_REAL_CONFIG_VALUE")]
  public async Task TestConfigGetMethod(string nCKey)
  { 
    string manualResult = null!;
    using(SqlConnection conn = new SqlConnection(ConnectionString))
    {
      await conn.OpenAsync();
      using(SqlCommand cmd = new SqlCommand($"SELECT * FROM [Config] WHERE [CKey] = @p", conn))
      { 
        cmd.Parameters.AddWithValue("@p", nCKey);
        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
        {
          if (await reader.ReadAsync())
            manualResult = reader.GetString(reader.GetOrdinal("CValue"));
        }
      }
    }

    string configResult = await Config.GetConfigValueAsync(nCKey);

    Assert.IsTrue(IsStringsSame(true, manualResult, configResult));
  }
}
