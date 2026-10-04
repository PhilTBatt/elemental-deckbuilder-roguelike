namespace Roguelike.Rules.Tests;

public class CharacterTests
{
    [Fact]
    public void Character_Takes_damage()
    {
        var player = new Player(40);
        var enemy = new Enemy(15);

        player.TakeDamage(10);
        enemy.TakeDamage(5);

        Assert.Equal(30, player.Health);
        Assert.Equal(10, enemy.Health);
    }
}
