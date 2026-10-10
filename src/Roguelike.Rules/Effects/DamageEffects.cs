namespace Roguelike.Rules;

public class DamageEffect(int amount) : Effect("Attack", amount) {

    public override void Apply(Battle battle, Character caster, Character target) {
        target.TakeDamage(Amount);
    }

    public override string Description => $"Deal {Amount} damage";
}