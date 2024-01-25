using System.Data.SqlClient;

namespace CipherCommonDB.TableDef;
public class Config : SqlTableBase
{
  public string CKey;
  public string CValue;

  public Config() : base("Config")
  {
  }

  public Config(string nKey, string nValue) : base("Config")
  {
    CKey = nKey;
    CValue = nValue;
  }

  public static async Task<string> GetConfigValueAsync(string nKey) => ((Config)await new Config().SelectSingleAsync(nameof(CKey), nKey))?.CValue;

  public override bool CheckColumnNames(List<string> nColumnNames, bool nThrowException = false)
  {
    for (int x = 0; x < nColumnNames.Count; x++)
    {
      switch (nColumnNames[x])
      {
        case "CKey":
          continue;
        case "CValue":
          continue;
        default:
          if (nThrowException)
            throw new ArgumentException("Invalid column name in Config");
          return false;
      }
    }

    return true;
  }

  public static async Task<string> SelectSingleAsync(string nKey) => ((Config) await new Config().SelectSingleAsync(nameof(CKey), nKey)).CValue;

  public override async Task<ISqlTable> SelectSingleAsync(string nParamName, object nValue)
    => await SelectSingleAsync(new Dictionary<string, object>() { { nParamName, nValue } });

  public override async Task<ISqlTable> SelectSingleAsync(Dictionary<string, object> nParams)
    => await SelectSingleAsync(nParams, null);

  public override async Task<ISqlTable> SelectSingleAsync(Dictionary<string, object> nParams, string nOrderByCol, OrderByEnum nOrderByType)
    => await SelectSingleAsync(nParams, new Dictionary<string, OrderByEnum>() { { nOrderByCol, nOrderByType } });

  public override async Task<ISqlTable> SelectSingleAsync(Dictionary<string, object> nParams, Dictionary<string, OrderByEnum> nOrderBy)
  {
    try
    {
      string query = $"SELECT TOP(1) * FROM {GetTableName()}";

      using(SqlConnection conn = await GetOpenSQLConnection())
      { 
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
          AddWhereParameters(nParams, cmd);
          AddOrderByParameters(nOrderBy, cmd);

          using (SqlDataReader r = await cmd.ExecuteReaderAsync())
          {
            while (await r.ReadAsync())
            {
              string _key = r.GetString(r.GetOrdinal(nameof(CKey)));
              string _value = r.GetString(r.GetOrdinal(nameof(CValue)));

              return new Config(_key, _value);
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }

    return null!;
  }
}