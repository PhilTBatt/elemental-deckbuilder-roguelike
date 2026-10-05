namespace Roguelike.Rules;

public class Character {
    public int Health { get; private set; }

    public int Block { get; private set; }

    public bool IsAlive => Health > 0;

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

    public void EndTurn() {
    }

    public void StartTurn() {
        Block = 0;
    }
}