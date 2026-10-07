namespace Roguelike.Rules.Tests;

public class SimulationTests {
    [Fact]
    public void Auto_Play_Stops_When_The_Energy_Is_Gone() {
        var player = new Player(40);
        for (int i = 0; i < 10; i++) {
            player.Deck.Add(new Card(0));
        }
        var battle = new Battle(new Random(42), player, new Enemy(15, [new DamageEffect(4)]));
        battle.StartTurn();

        battle.PlayPlayerTurn();

        Assert.Equal(0, battle.PlayerEnergy);
        Assert.Equal(6, battle.Enemy.Health);
        Assert.Equal(2, battle.PlayerHand.Count);
        Assert.Equal(3, battle.DiscardPile.Count);
    }

    [Fact]
    public void Starting_Player_Beats_The_Slime() {
        var battle = new Battle(new Random(42), new Player(), new Slime());

        battle.StartBattle();

        Assert.True(battle.IsOver);
        Assert.Equal(BattleState.PlayerWins, battle.State);
    }
}