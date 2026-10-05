namespace Roguelike.Rules.Tests;

public class ShufflerTests {
    private static List<Card> MakeCards(int count) {
        List<Card> cards = [];
        for (int i = 0; i < count; i++) {
            cards.Add(new Card("Card" + i, 1));
        }
        return cards;
    }

    [Fact]
    public void Shuffle_Keeps_The_Same_Cards() {
        var original = MakeCards(10);
        List<Card> shuffled = [..original];

        Shuffler.Shuffle(new Random(123), shuffled);

        Assert.Equal(10, shuffled.Count);
        Assert.All(original, card => Assert.Contains(card, shuffled));
    }

    [Fact]
    public void Shuffle_Changes_The_Order() {
        var original = MakeCards(10);
        List<Card> shuffled = [..original];

        Shuffler.Shuffle(new Random(123), shuffled);

        Assert.NotEqual(original, shuffled);
    }

    [Fact]
    public void Same_Seed_Gives_The_Same_Order() {
        var original = MakeCards(10);
        List<Card> first = [..original];
        List<Card> second = [..original];

        Shuffler.Shuffle(new Random(42), first);
        Shuffler.Shuffle(new Random(42), second);

        Assert.Equal(first, second);
    }

    [Fact]
    public void Shuffling_An_Empty_List_Does_Nothing() {
        List<Card> cards = [];

        Shuffler.Shuffle(new Random(1), cards);

        Assert.Empty(cards);
    }
}