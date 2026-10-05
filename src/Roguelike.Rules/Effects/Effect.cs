namespace Roguelike.Rules;

public abstract class Effect {
    public abstract void Apply(Battle battle, Character caster, Character target);

}