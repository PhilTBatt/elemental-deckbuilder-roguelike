namespace Roguelike.Rules;

public class Slime() : Enemy(20, [new Effect(EffectType.Attack, 5), new Effect(EffectType.Block, 5), new Effect(EffectType.Attack, 5)]) {
    
}