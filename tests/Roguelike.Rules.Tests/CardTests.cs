namespace Roguelike.Rules.Tests;

public class CardTests {
    [Fact]
    public void Card_0_Is_Strike() {
        var card = new Card(0);

        Assert.Equal(0, card.Id);
        Assert.Equal("Strike", card.Name);
        Assert.Equal(1, card.Cost);
    }

    [Fact]
    public void Card_1_Is_Defend() {
        var card = new Card(1);

        Assert.Equal(1, card.Id);
        Assert.Equal("Defend", card.Name);
        Assert.Equal(1, card.Cost);
    }

    [Fact]
    public void Unknown_Card_Id_Throws() {
        Assert.Throws<ArgumentException>(() => new Card(7));
    }

    [Fact]
    public void Two_Cards_With_The_Same_Id_Are_Separate_Cards() {
        var first = new Card(0);
        var second = new Card(0);

        Assert.NotSame(first, second);
    }
}