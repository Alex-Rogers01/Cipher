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

  #region SELECT 
  
  public async Task SelectAllAsync() => await GetSingleEntryAsync(null!, null!);
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

  #endregion SELECT


  #region INSERT 

  public async Task<bool> InsertSingleEntryAsync()
  { 
    using(SqlConnection nConn = new SqlConnection(SqlManager.ConnectionString))
    { 
      await nConn.OpenAsync();
      return await InsertSingleEntryAsync(nConn);
    }
  }
  public async Task<bool> InsertSingleEntryAsync(SqlConnection nConn) => await InsertSingleEntryAsync(this, nConn);
  public async Task<bool> InsertSingleEntryAsync(SqlTableBase nData, SqlConnection nConn) => await InsertSingleEntryAsync(nData.CreateDataTable(), nConn);
  public abstract Task<bool> InsertSingleEntryAsync(DataTable nTable, SqlConnection nConn);

  public async Task<bool> InsertMultipleEntryAsync(DataTable nTable, SqlConnection nConn)
  {
    using (DbTransaction trans = await nConn.BeginTransactionAsync())
    {
      await InsertMultipleEntryAsync(nTable, nConn, trans);
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
  public async Task<bool> InsertMultipleEntryAsync(List<SqlTableBase> nData, SqlConnection nConn)
  { 
    using(DbTransaction trans = await nConn.BeginTransactionAsync())
    { 
      return await InsertMultipleEntryAsync(nData, nConn, trans);
    }
  }

  public async Task<bool> InsertMultipleEntryAsync(List<SqlTableBase> nData, SqlConnection nConn, DbTransaction nTrans)
  { 
    DataTable tbl = new DataTable();
    foreach(SqlTableBase table in nData)
    { 
      table.AddSelfToDataTable(tbl);
    }

    return await InsertMultipleEntryAsync(tbl, nConn, nTrans);
  }

  public async Task<bool> InsertMultipleEntryAsync(DataTable nTable, SqlConnection nConn, DbTransaction nTrans)
  { 
    foreach(DataRow row in nTable.Rows)
    { 
      if(!await InsertMultipleEntryAsync(row, nConn, nTrans))
        return false;
    }
    return true;
  }

  public abstract Task<bool> InsertMultipleEntryAsync(DataRow nDataRow, SqlConnection nConn, DbTransaction nTrans);

  #endregion INSERT

  public abstract void AddSelfToDataTable(DataTable nTable);
  public abstract DataTable CreateDataTable();


  public abstract bool CheckColumnNames(List<string> nColumnNamesToCheck, bool nThrowException);

  public virtual void AddOrderByParameters(Dictionary<string, OrderByEnum> nOrderBy, SqlCommand nCommand, bool nThrowException = false)
  {
    if (nOrderBy != null && nOrderBy.Count > 0)
    {
      if(CheckColumnNames(nOrderBy.Keys.ToList(), nThrowException))
      { 
        List<string> orderByLst = new List<string>();
        foreach (string key in nOrderBy.Keys)
        {
          orderByLst.Add($"[{key}] {Enum.GetName(typeof(OrderByEnum), nOrderBy[key])}");
        }

        nCommand.CommandText += $" ORDER BY {string.Join(',', orderByLst)}";
      }
    }
  }
  public virtual void AddWhereParameters(Dictionary<string, object> nParams, SqlCommand nCommand, bool nThrowException = false)
  {
    if (nParams != null && nParams.Count > 0)
    {
      if(CheckColumnNames(nParams.Keys.ToList(), nThrowException))
      { 
        nCommand.CommandText += " WHERE";

        int counter = 0;
        foreach (string key in nParams.Keys)
        {
          nCommand.CommandText += $" [{key}] = @param{counter}";
          nCommand.Parameters.AddWithValue($"@param{counter++}", nParams[key]);
        }
      }
    }
  }

  protected string GetTableName() => $"{this.Schema}.{this.TableName}";
}
