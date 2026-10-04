namespace Roguelike.Rules.Tests;

public class BattleTests
{
    [Fact]
    public void Battle_Starts_With_Player_And_Enemy()
    {
        var battle = new Battle(new Player(40), new Enemy(15, 4));
        
        Assert.NotNull(battle.Player);
        Assert.NotNull(battle.Enemy);
        Assert.Equal(40, battle.Player.Health);
        Assert.Equal(15, battle.Enemy.Health);
    }

    [Fact]
    public void One_Turn_Damages_Both_Sides()
    {
        var battle = new Battle(new Player(40), new Enemy(15, 4));

        Assert.Equal(4, battle.Enemy.Damage);

        battle.ProcessOneTurn();

        Assert.Equal(36, battle.Player.Health);
        Assert.Equal(10, battle.Enemy.Health);
    }

    [Fact]
    public void Dead_Enemy_Does_Not_Attack() {
        var battle = new Battle(new Player(40), new Enemy(5, 4));

        battle.ProcessOneTurn();

        Assert.False(battle.Enemy.IsAlive);
        Assert.Equal(40, battle.Player.Health);
    }

    [Fact]
    public void Full_Battle_Won_By_Player() {
        var battle = new Battle(new Player(40), new Enemy(15, 4));

        battle.StartBattle();

        Assert.False(battle.Enemy.IsAlive);
        Assert.True(battle.Player.IsAlive);
        Assert.Equal(32, battle.Player.Health);
    }

    [Fact]
    public void Full_Battle_Won_By_Enemy() {
        var battle = new Battle(new Player(12), new Enemy(20, 4));

        battle.StartBattle();

        Assert.False(battle.Player.IsAlive);
        Assert.True(battle.Enemy.IsAlive);
        Assert.Equal(5, battle.Enemy.Health);
    }
}
