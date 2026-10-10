namespace Roguelike.Rules;

public enum EffectType { Attack, Block }

public class Effect {
    public EffectType Kind { get; }

    public int Amount { get; }

    public string Description { get; }

    public Effect(EffectType kind, int amount) {
        Kind = kind;
        Amount = amount;

        switch (Kind) {
            case EffectType.Attack:
                Description = $"Deal {Amount} damage";
                break;

            case EffectType.Block:
                Description = $"Gain {Amount} block";
                break;
                
            default:
                throw new ArgumentException("No effect has type " + kind);
        }
    }

    public void Apply(Battle battle, Character caster, Character target) {
        switch (Kind) {
            case EffectType.Attack:
                target.TakeDamage(Amount);
                break;

            case EffectType.Block:
                caster.AddBlock(Amount);
                break;
        }
    }

}