namespace Roguelike.Rules;

public class Enemy : Character {
    public int Damage { get; private set; }

    public Enemy(int health, int damage) : base(health) {
        Damage = damage;
    }
}