namespace Roguelike.Rules.Tests;

public class BattleTests
{
    [Fact]
    public void Battle_Starts_With_Player_And_Enemy()
    {
        var battle = new Battle();
        
        Assert.NotNull(battle.Player);
        Assert.NotNull(battle.Enemy);
        Assert.Equal(40, battle.Player.Health);
        Assert.Equal(40, battle.Enemy.Health);
    }
}
