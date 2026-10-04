namespace Roguelike.Rules;

public class Card {
    public string Name { get; private set; }
    public int Cost { get; private set; }

    public Card(string name, int cost) {
        Name = name;
        Cost = cost;
    }

    public void PlayCard() {
        
    }
}