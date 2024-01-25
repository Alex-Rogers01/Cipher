using System.Data;
using System.Data.SqlClient;

namespace CipherCommonDB;

public abstract class SqlTableBase : ISqlTable
{
  public string TableName { get; }
  public string TableSchema { get; }

  public SqlTableBase(string nTableName, string nTableSchema = "dbo")
  {
    TableName = nTableName;
    TableSchema = nTableSchema;
  }

  public virtual async Task<ISqlTable> SelectSingleAsync(string nParamName, object nValue) => throw new NotImplementedException();
  public virtual async Task<ISqlTable> SelectSingleAsync(Dictionary<string, object> nParams) => throw new NotImplementedException();
  public virtual async Task<ISqlTable> SelectSingleAsync(Dictionary<string, object> nParams, string nOrderByCol, OrderByEnum nOrderByType) => throw new NotImplementedException();
  public virtual async Task<ISqlTable> SelectSingleAsync(Dictionary<string, object> nParams, Dictionary<string, OrderByEnum> nOrderBy) => throw new NotImplementedException();
  public virtual async Task<IEnumerable<ISqlTable>> SelectMultipleAsync(Dictionary<string, object> nParams, int nMaxCol = 0) => throw new NotImplementedException();
  public virtual async Task<IEnumerable<ISqlTable>> SelectMultipleAsync(Dictionary<string, object> nParams, string nOrderByCol, OrderByEnum nOrderByType, int nMaxCol = 0) => throw new NotImplementedException();
  public virtual async Task<IEnumerable<ISqlTable>> SelectMultipleAsync(Dictionary<string, object> nParams, Dictionary<string, OrderByEnum> nOrderBy, int nMaxCol = 0) => throw new NotImplementedException();
  public virtual async Task<bool> InsertDataAsync() => throw new NotImplementedException();
  public virtual async Task<bool> InsertMultipleDataAsync(List<ISqlTable> nDataRows, SqlTransaction nTrans) => throw new NotImplementedException();
  public virtual async Task<bool> InsertMultipleDataAsync(DataTable nDataTable, SqlTransaction nTrans) => throw new NotImplementedException();
  public virtual async Task<bool> UpdateDataAsync() => throw new NotImplementedException();
  public virtual async Task<bool> UpdateMultipleDataAsync(List<ISqlTable> nDataRows, SqlTransaction nTrans) => throw new NotImplementedException();
  public virtual async Task<bool> UpdateMultipleDataAsync(DataTable nDataTable, SqlTransaction nTrans) => throw new NotImplementedException();
  public virtual async Task<bool> DeleteAsync() => throw new NotImplementedException();
  public virtual async Task<bool> DeleteMultipleAsync(List<ISqlTable> nDataRows) => throw new NotImplementedException();
  public virtual async Task<bool> DeleteMultipleAsync(DataTable nDataTable) => throw new NotImplementedException();

  public abstract bool CheckColumnNames(List<string> nColumnNames, bool nThrowException);

  public virtual void AddWhereParameters(Dictionary<string, object> nParams, SqlCommand nCommand)
  {
    if (nParams != null && nParams.Count > 0)
    {
      CheckColumnNames(nParams.Keys.ToList(), true);

      nCommand.CommandText += " WHERE";

      int counter = 0;
      foreach (string key in nParams.Keys)
      {
        nCommand.CommandText += $" [{key}] = @param{counter}";
        nCommand.Parameters.AddWithValue($"@param{counter++}", nParams[key]);
      }
    }
  }

  public virtual void AddOrderByParameters(Dictionary<string, OrderByEnum> nOrderBy, SqlCommand nCommand)
  {
    if (nOrderBy != null && nOrderBy.Count > 0)
    {
      CheckColumnNames(nOrderBy.Keys.ToList(), true);

      List<string> orderByLst = new List<string>();
      foreach (string key in nOrderBy.Keys)
      {
        orderByLst.Add($"[{key}] {Enum.GetName(typeof(OrderByEnum), nOrderBy[key])}");
      }

      nCommand.CommandText += $" ORDER BY {string.Join(',', orderByLst)}";
    }
  }

  protected string GetTableName() => $"[{TableSchema}].[{TableName}]";
}