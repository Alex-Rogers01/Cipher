using Discord.WebSocket;

namespace Cipher.Games.SocioSphere.Assets.Characters.Player;
public class PlayerCharacter
{ 
  private DiscordSocketClient Player { get; init; }
  public PlayerInventory Inventory { get; }

  public PlayerCharacter(DiscordSocketClient nPlayer)
  { 
    Player = nPlayer;
    LoadPlayerAsync().ConfigureAwait(false);
  }

  private async Task LoadPlayerAsync()
  { 
    // Try and retrieve player info from DB
    // If they dont exist, generate a new player

    await GenerateNewPlayerAsync();
  }

  private async Task GenerateNewPlayerAsync()
  { 
    // Generate a new player and populate DB
  }
}
