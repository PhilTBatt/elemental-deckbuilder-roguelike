namespace Roguelike.Rules.Tests;

public class BattleTests {
    [Fact]
    public void Battle_Starts_With_Player_And_Enemy() {
        var battle = new Battle(new Player(40), new Enemy(15, 4));

        Assert.NotNull(battle.Player);
        Assert.NotNull(battle.Enemy);
        Assert.Equal(40, battle.Player.Health);
        Assert.Equal(15, battle.Enemy.Health);
        Assert.False(battle.IsOver);
    }

    [Fact]
    public void Start_Of_Turn_Gives_Energy_And_Player_Turn() {
        var battle = new Battle(new Player(40), new Enemy(15, 4));

        battle.StartTurn();

        Assert.Equal(3, battle.PlayerEnergy);
        Assert.Equal(BattleState.PlayerTurn, battle.State);
    }

    [Fact]
    public void End_Turn_Lets_Enemy_Hit_Player() {
        var battle = new Battle(new Player(40), new Enemy(15, 4));

        battle.StartTurn();
        battle.EndTurn();

        Assert.Equal(36, battle.Player.Health);
    }

    [Fact]
    public void Block_Reduces_Enemy_Hit() {
        var battle = new Battle(new Player(40), new Enemy(15, 4));

        battle.StartTurn();
        battle.Player.AddBlock(3);
        battle.EndTurn();

        Assert.Equal(39, battle.Player.Health);
    }

    [Fact]
    public void Full_Battle_Ends_When_Player_Dies() {
        var battle = new Battle(new Player(12), new Enemy(20, 4));

        battle.StartBattle();

        Assert.Equal(BattleState.EnemyWins, battle.State);
        Assert.False(battle.Player.IsAlive);
        Assert.True(battle.IsOver);
    }

    [Fact]
    public void Dead_Enemy_Does_Not_Attack() {
        var battle = new Battle(new Player(40), new Enemy(15, 4));

        battle.StartTurn();
        battle.Enemy.TakeDamage(15);
        battle.EndTurn();

        Assert.Equal(BattleState.PlayerWins, battle.State);
        Assert.Equal(40, battle.Player.Health);
        Assert.True(battle.IsOver);
    }

    [Fact]
    public void Finished_Battle_Does_Nothing_More() {
        var battle = new Battle(new Player(4), new Enemy(15, 4));
        battle.StartTurn();
        battle.EndTurn();

        battle.ProcessTurn();

        Assert.Equal(BattleState.EnemyWins, battle.State);
        Assert.Equal(0, battle.Player.Health);
        Assert.True(battle.IsOver);
    }
}