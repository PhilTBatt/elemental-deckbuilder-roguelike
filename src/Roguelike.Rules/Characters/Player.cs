namespace Roguelike.Rules;

public class Player(int health) : Character(health) {
    public List<Card> Deck { get; private set; } = [];

    public int Gold { get; private set; } = 0;

    public Player() : this(40) {
        for (int i = 0; i < 5; i++) Deck.Add(new Card(0));
        for (int i = 0; i < 5; i++) Deck.Add(new Card(1));
    }

    public void GainGold(int amount) => Gold += amount;
}