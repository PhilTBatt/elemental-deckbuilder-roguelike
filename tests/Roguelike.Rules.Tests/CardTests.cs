namespace Roguelike.Rules.Tests;

public class CardTests {
    [Fact]
    public void Card_Keeps_Name_And_Cost() {
        var card = new Card("Strike", 1);

        Assert.Equal("Strike", card.Name);
        Assert.Equal(1, card.Cost);
    }
}