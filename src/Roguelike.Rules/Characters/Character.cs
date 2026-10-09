namespace Roguelike.Rules;

public class Character {
    public int Health { get; private set; }

    public int Block { get; private set; }

    public bool IsAlive => Health > 0;

    public Battle? Battle { get; set; }

    public List<Status> Statuses {get;} = [];

    public Character(int health) {
        Health = health;
    }

    public void TakeDamage(int damage) {
        if (Block >= damage) {
            Block -= damage;
        } else {
            Health -= damage - Block;
            Block = 0;
        }
    }

    public void AddBlock(int block) {
        Block += block;
    }

    public void EndOfTurn() {
        foreach (var status in Statuses) status.EndTurn(this);
        Statuses.RemoveAll(status => status.Stacks <= 0);
    }

    public void StartOfTurn() {
        Block = 0;
    }
}