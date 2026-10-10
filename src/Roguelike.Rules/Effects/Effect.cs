namespace Roguelike.Rules;

public abstract class Effect(string name, int amount) {
    public string Name { get;} = name;

    public int Amount { get; } = amount;

    public abstract string Description { get; }

    public abstract void Apply(Battle battle, Character caster, Character target);

}