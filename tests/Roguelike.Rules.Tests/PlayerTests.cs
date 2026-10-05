namespace Roguelike.Rules.Tests;

public class PlayerTests {
    [Fact]
    public void New_Player_Has_Empty_Deck() {
        var player = new Player(40);

        Assert.Empty(player.Deck);
    }
}