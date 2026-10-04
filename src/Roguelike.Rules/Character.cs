namespace Roguelike.Rules;

public class Character {
    public int Health { get; private set; }

    public Character(int health) {
        Health = health;
    }

    public void TakeDamage(int damage) {
        Health -= damage;
    }
}