namespace Roguelike.Rules;

public class DamageEffect(int amount) : Effect {
    public int Amount { get; } = amount;

    public override void Apply(Battle battle, Character caster, Character target) {
        target.TakeDamage(Amount);
    }
}