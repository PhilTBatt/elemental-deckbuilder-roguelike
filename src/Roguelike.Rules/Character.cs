namespace Roguelike.Rules;

public class Character {
    public int Health { get; private set; }

    public bool IsAlive => Health > 0;

    public Character(int health) {
        Health = health;
    }

    public void TakeDamage(int damage) {
        Health -= damage;
    }
}