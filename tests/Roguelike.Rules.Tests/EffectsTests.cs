namespace Roguelike.Rules.Tests;

public class EffectTests {
    [Fact]
    public void Damage_Effect_Hurts_The_Target() {
        var battle = new Battle(new Player(40), new Enemy(15, [new DamageEffect(4)]));

        new DamageEffect(3).Apply(battle, battle.Player, battle.Enemy);

        Assert.Equal(12, battle.Enemy.Health);
        Assert.Equal(40, battle.Player.Health);
    }

    [Fact]
    public void Block_Effect_Gives_Block_To_The_Caster() {
        var battle = new Battle(new Player(40), new Enemy(15, [new DamageEffect(4)]));

        new BlockEffect(3).Apply(battle, battle.Player, battle.Enemy);

        Assert.Equal(3, battle.Player.Block);
        Assert.Equal(0, battle.Enemy.Block);
    }

    [Fact]
    public void Enemy_Can_Be_The_Caster() {
        var battle = new Battle(new Player(40), new Enemy(15, [new DamageEffect(4)]));

        new DamageEffect(4).Apply(battle, battle.Enemy, battle.Player);

        Assert.Equal(36, battle.Player.Health);
        Assert.Equal(15, battle.Enemy.Health);
    }
}