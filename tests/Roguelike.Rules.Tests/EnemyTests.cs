namespace Roguelike.Rules.Tests;

public class EnemyTests {
    [Fact]
    public void Intent_Starts_As_The_First_Move() {
        var attack = new DamageEffect(4);
        var block = new BlockEffect(3);
        var enemy = new Enemy(15, [attack, block]);

        Assert.Same(attack, enemy.Intent);
    }

    [Fact]
    public void Enemy_Works_Through_Its_Moves_And_Starts_Again() {
        var attack = new DamageEffect(4);
        var block = new BlockEffect(3);
        var enemy = new Enemy(15, [attack, block]);
        var battle = new Battle(new Random(42), new Player(40), enemy);

        enemy.TakeAction(battle, battle.Player);
        Assert.Equal(36, battle.Player.Health);
        Assert.Same(block, enemy.Intent);

        enemy.TakeAction(battle, battle.Player);
        Assert.Equal(3, enemy.Block);
        Assert.Same(attack, enemy.Intent);
    }

    [Fact]
    public void Slime_Attacks_For_4_Then_Blocks_For_3() {
        var slime = new Slime();
        var battle = new Battle(new Random(42), new Player(40), slime);

        slime.TakeAction(battle, battle.Player);
        Assert.Equal(15, slime.Health);
        Assert.Equal(36, battle.Player.Health);
        Assert.Equal(0, slime.Block);

        slime.TakeAction(battle, battle.Player);
        Assert.Equal(36, battle.Player.Health);
        Assert.Equal(3, slime.Block);
    }
}