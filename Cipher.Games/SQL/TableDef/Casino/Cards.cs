using Cipher.CommonDB;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using static Cipher.Games.Casino.Card;

namespace Cipher.Games.SQL.TableDef.Casino;
public class Cards : SqlTableBase
{
  public int ID { get; set; }
  public CardValue CardValue { get; set; }
  public CardSuit CardSuit { get; set; }
  public string CardImage { get; set; }

  public Cards() : base("Cards", "dbo")
  {
  }

  public override async Task GetSingleEntryAsync(Dictionary<string, object> nParameters, Dictionary<string, OrderByEnum> nOrderBy, SqlConnection nConn)
  {
    using(SqlCommand cmd = new SqlCommand($"SELECT * FROM {GetTableName()}"))
    { 
      AddWhereParameters(nParameters, cmd);
      AddOrderByParameters(nOrderBy, cmd);

      using(SqlDataReader r = await cmd.ExecuteReaderAsync())
      {
        if(r.HasRows)
        {
          await r.ReadAsync();
          this.ID = r.GetInt32(r.GetOrdinal(nameof(ID)));
          this.CardValue = (CardValue)r.GetInt32(r.GetOrdinal(nameof(CardValue)));
          this.CardSuit = (CardSuit)r.GetInt32(r.GetOrdinal(nameof(CardSuit)));
          this.CardImage = r.GetString(r.GetOrdinal(nameof(CardImage)));
        }
      }
    }
  }

  public override Task<bool> InsertSingleEntryAsync(DataTable nTable, SqlConnection nConn)
  {
    throw new NotImplementedException();
  }

  public override Task<bool> InsertMultipleEntryAsync(DataRow nDataRow, SqlConnection nConn, DbTransaction nTrans)
  {
    throw new NotImplementedException();
  }

  public override void AddSelfToDataTable(DataTable nTable)
  {
    if(nTable.Columns.Count < 1)
      nTable = CreateDataTable();
    nTable.Rows.Add(this.ID, (int)this.CardValue, (int)this.CardSuit, this.CardImage);
  }

  public override DataTable CreateDataTable()
  {
    DataTable dt = new DataTable();
    dt.Columns.Add(nameof(ID), typeof(int));
    dt.Columns.Add(nameof(CardValue), typeof(int));
    dt.Columns.Add(nameof(CardSuit), typeof(int));  
    dt.Columns.Add(nameof(CardImage), typeof(string));

    return dt;
  }

  public override bool CheckColumnNames(List<string> nColumnNamesToCheck, bool nThrowException)
  {
    foreach(string colName in nColumnNamesToCheck)
    {
      switch(colName.ToLower())
      { 
        case "id":
        case "cardvalue":
        case "cardsuit":
        case "cardimage":
          continue;
        default:
          if(nThrowException)
            throw new Exception($"Column name {colName} is not valid for table {this.TableName}");
          else
            return false;
      }
    }

    return true;
  }
}
