using System.Data;
using System.Data.SqlClient;

namespace CipherCommonDB;

public enum OrderByEnum
{
  ASC,
  DESC
}

public interface ISqlTable
{
  string TableName { get; }
  string TableSchema { get; }

  Task<ISqlTable> SelectSingleAsync(string nParamName, object nValue);
  Task<ISqlTable> SelectSingleAsync(Dictionary<string, object> nParams);
  Task<ISqlTable> SelectSingleAsync(Dictionary<string, object> nParams, string nOrderByCol, OrderByEnum nOrderByType);
  Task<ISqlTable> SelectSingleAsync(Dictionary<string, object> nParams, Dictionary<string, OrderByEnum> nOrderBy);

  /* Max Col = 0 - Return all*/
  Task<IEnumerable<ISqlTable>> SelectMultipleAsync(Dictionary<string, object> nParams, int nMaxCol = 0);
  Task<IEnumerable<ISqlTable>> SelectMultipleAsync(Dictionary<string, object> nParams, string nOrderByCol, OrderByEnum nOrderByType, int nMaxCol = 0);
  Task<IEnumerable<ISqlTable>> SelectMultipleAsync(Dictionary<string, object> nParams, Dictionary<string, OrderByEnum> nOrderBy, int nMaxCol = 0);

  Task<bool> InsertDataAsync();
  Task<bool> InsertMultipleDataAsync(List<ISqlTable> nDataRows, SqlTransaction nTrans);
  Task<bool> InsertMultipleDataAsync(DataTable nDataTable, SqlTransaction nTrans);

  Task<bool> UpdateDataAsync();
  Task<bool> UpdateMultipleDataAsync(List<ISqlTable> nDataRows, SqlTransaction nTrans);
  Task<bool> UpdateMultipleDataAsync(DataTable nDataTable, SqlTransaction nTrans);

  Task<bool> DeleteAsync();
  Task<bool> DeleteMultipleAsync(List<ISqlTable> nDataRows);
  Task<bool> DeleteMultipleAsync(DataTable nDataTable);
}