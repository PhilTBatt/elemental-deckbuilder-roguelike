namespace Roguelike.Rules;

public class Player(int health) : Character(health) {
    public Battle? Battle { get; set; }

    public List<Card> Deck { get; private set; } = [];
}