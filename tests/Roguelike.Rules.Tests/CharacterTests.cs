namespace Roguelike.Rules.Tests;

public class CharacterTests
{
    [Fact]
    public void Character_Takes_Damage()
    {
        var player = new Player(40);
        var enemy = new Enemy(15, 4);

        player.TakeDamage(10);
        enemy.TakeDamage(5);

        Assert.Equal(30, player.Health);
        Assert.Equal(10, enemy.Health);
    }

    [Fact]
    public void Character_Survives_And_Dies()
    {
        var player = new Player(40);
        var enemy = new Enemy(15, 4);

        player.TakeDamage(10);
        enemy.TakeDamage(5);

        Assert.True(player.IsAlive);
        Assert.True(enemy.IsAlive);

        player.TakeDamage(30);
        enemy.TakeDamage(10);

        Assert.False(player.IsAlive);
        Assert.False(enemy.IsAlive);
    }

    [Fact]
    public void Block_Absorbs_Smaller_Hit() {
        var player = new Player(40);
        player.AddBlock(5);

        player.TakeDamage(3);

        Assert.Equal(40, player.Health);
        Assert.Equal(2, player.Block);
    }

    [Fact]
    public void Bigger_Hit_Spills_Past_Block() {
        var player = new Player(40);
        player.AddBlock(5);

        player.TakeDamage(8);

        Assert.Equal(37, player.Health);
        Assert.Equal(0, player.Block);
    }

    [Fact]
    public void Block_Stacks() {
        var player = new Player(40);

        player.AddBlock(3);
        player.AddBlock(3);

        Assert.Equal(6, player.Block);
    }

    [Fact]
    public void Start_Of_Turn_Clears_Block() {
        var player = new Player(40);
        player.AddBlock(5);

        player.StartTurn();

        Assert.Equal(0, player.Block);
}
}
