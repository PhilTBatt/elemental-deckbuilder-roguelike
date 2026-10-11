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

    public void AddStatus(StatusType kind, int stacks) {
        var existing = Statuses.Find(status => status.Kind == kind);
        if (existing != null) {
            existing.Stacks += stacks;
        } else {
            Statuses.Add(new Status(kind, stacks));
        }
    }

    public void EndOfTurn() {
    }

    public void StartOfTurn() {
        Block = 0;
        foreach (var status in Statuses) status.Tick(this);
        Statuses.RemoveAll(status => status.Stacks <= 0);
    }
}