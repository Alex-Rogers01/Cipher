using CipherCommonDB;
using CipherCommonDB.TableDef;
namespace CipherDiscord;

public class Program
{
  static async Task Main(string[] args)
  {
    Console.WriteLine(await Config.SelectSingleAsync("DiscordToken"));
  }
}
