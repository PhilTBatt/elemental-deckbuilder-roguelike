namespace Roguelike.Rules;

public class Player : Character {
    public List<Card> Deck { get; private set; }
    public Player(int health) : base(health) {
        Deck = new List<Card>();
    }
}