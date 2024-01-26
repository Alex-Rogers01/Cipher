using CipherCommonDB;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Cipher.CommonDB;

public enum OrderByEnum
{ 
  ASC,
  DESC
}

public abstract class SqlTableBase
{
  protected string TableName { get; set; }
  protected string Schema { get; set; }

  protected SqlTableBase(string nTableName, string nSchema)
  { 
    this.TableName = nTableName;
    this.Schema = nSchema;
  }

  public async Task GetSingleEntryAsync(string nParameterName, object nValue)
  {
    using(SqlConnection nConn = new SqlConnection(SqlManager.ConnectionString))
    {
      await nConn.OpenAsync();
      await GetSingleEntryAsync(nParameterName, nValue, nConn);
    }
  }
  public async Task GetSingleEntryAsync(string nParameterName, object nValue, SqlConnection nConn) => await GetSingleEntryAsync(new Dictionary<string, object>() { { nParameterName, nValue } }, nConn);
  public async Task GetSingleEntryAsync(Dictionary<string, object> nParameters, SqlConnection nConn) => await GetSingleEntryAsync(nParameters, null!, nConn);
  public async Task GetSingleEntryAsync(Dictionary<string, object> nParameters, string nOrderByColumn, OrderByEnum nOrderByType, SqlConnection nConn) => await GetSingleEntryAsync(nParameters, new Dictionary<string, OrderByEnum>() { { nOrderByColumn, nOrderByType } }, nConn);
  public abstract Task GetSingleEntryAsync(Dictionary<string, object> nParameters, Dictionary<string, OrderByEnum> nOrderBy, SqlConnection nConn);

  public async Task<bool> InsertSingleEntryAsync()
  { 
    using(SqlConnection nConn = new SqlConnection(SqlManager.ConnectionString))
    { 
      await nConn.OpenAsync();
      return await InsertSingleEntryAsync(nConn);
    }
  }
  public async Task<bool> InsertSingleEntryAsync(SqlConnection nConn) => await InsertSingleEntryAsync(this, nConn);
  public async Task<bool> InsertSingleEntryAsync(SqlTableBase nData, SqlConnection nConn) => await InsertSingleEntryAsync(nData.ToDataTable(), nConn);
  public abstract Task<bool> InsertSingleEntryAsync(DataTable nTable, SqlConnection nConn);

  public async Task<bool> InsertMultipleEntryAsync(List<DataTable> nTables, SqlConnection nConn)
  {
    using (DbTransaction trans = await nConn.BeginTransactionAsync())
    {
      try
      { 
        foreach (DataTable table in nTables)
        {
          if (!await InsertMultipleEntryAsync(nTables, nConn, trans))
          { 
            await trans.RollbackAsync();
            return false;
          }
        }
      }
      catch(Exception ex)
      { 
        // TODO: Implement logging
      }
    }
    return true;
  }
  public async Task<bool> InsertMultipleEntryAsync(List<SqlTableBase> nData)
  { 
    using(SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
    {
      await conn.OpenAsync();
      return await InsertMultipleEntryAsync(nData, conn);
    }
  }
  public async Task<bool> InsertMultipleEntryAsync(List<SqlTableBase> nData, SqlConnection nConn) => await InsertMultipleEntryAsync(nData.Select(x => x.ToDataTable()).ToList(), nConn);
  public async Task<bool> InsertMultipleEntryAsync(List<SqlTableBase> nData, SqlConnection nConn, DbTransaction nTrans) => await InsertMultipleEntryAsync(nData.Select(x => x.ToDataTable()).ToList(), nConn, nTrans);
  public abstract Task<bool> InsertMultipleEntryAsync(List<DataTable> nTables, SqlConnection nConn, DbTransaction nTrans);

  public abstract DataTable ToDataTable();

  protected string GetTableName() => $"{this.Schema}.{this.TableName}";
}
