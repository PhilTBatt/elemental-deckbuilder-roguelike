namespace Roguelike.Rules.Tests;

public class PlayerTests {
    [Fact]
    public void New_Player_Has_Empty_Deck() {
        var player = new Player(40);

        Assert.Empty(player.Deck);
    }

    [Fact]
    public void Starting_Player_Has_40_Health_And_Ten_Cards() {
        var player = new Player();

        Assert.Equal(40, player.Health);
        Assert.Equal(10, player.Deck.Count);
    }

    [Fact]
    public void Starting_Deck_Is_Five_Strikes_And_Five_Defends() {
        var player = new Player();

        Assert.Equal(5, player.Deck.Count(card => card.Id == 0));
        Assert.Equal(5, player.Deck.Count(card => card.Id == 1));
    }
}