namespace Roguelike.Rules;

public class BlockEffect(int amount) : Effect {
    public int Amount { get; } = amount;

    public override void Apply(Battle battle, Character caster, Character target) {
        caster.AddBlock(Amount);
    }
}