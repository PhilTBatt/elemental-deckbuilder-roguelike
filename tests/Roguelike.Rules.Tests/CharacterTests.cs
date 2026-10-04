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
}
