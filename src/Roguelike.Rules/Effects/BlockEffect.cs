namespace Roguelike.Rules;

public class BlockEffect(int amount) : Effect("Block", amount) {
    public override void Apply(Battle battle, Character caster, Character target) {
        caster.AddBlock(Amount);
    }

    public override string Description => $"Gain {Amount} block";  
}