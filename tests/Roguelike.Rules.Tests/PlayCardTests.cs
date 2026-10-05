namespace Roguelike.Rules.Tests;

public class PlayCardTests {
    private static Battle BattleWithDeckOf(int cardId, int enemyHealth) {
        var player = new Player(40);
        for (int i = 0; i < 10; i++) {
            player.Deck.Add(new Card(cardId));
        }
        return new Battle(player, new Enemy(enemyHealth, [new DamageEffect(4)]));
    }

    [Fact]
    public void Playing_A_Strike_Damages_The_Enemy() {
        var battle = BattleWithDeckOf(0, 15);
        battle.StartTurn();

        var played = battle.PlayCard(battle.PlayerHand[0]);

        Assert.True(played);
        Assert.Equal(12, battle.Enemy.Health);
    }

    [Fact]
    public void Playing_A_Defend_Blocks_The_Enemy_Hit() {
        var battle = BattleWithDeckOf(1, 15);
        battle.StartTurn();

        battle.PlayCard(battle.PlayerHand[0]);
        battle.EndTurn();

        Assert.Equal(39, battle.Player.Health);
    }

    [Fact]
    public void A_Strike_Can_Win_The_Battle() {
        var battle = BattleWithDeckOf(0, 3);
        battle.StartTurn();

        battle.PlayCard(battle.PlayerHand[0]);
        battle.EndTurn();

        Assert.Equal(BattleState.PlayerWins, battle.State);
        Assert.Equal(40, battle.Player.Health);
    }

    [Fact]
    public void Fourth_Card_Is_Rejected_When_Energy_Runs_Out() {
        var battle = BattleWithDeckOf(0, 15);
        battle.StartTurn();
        battle.PlayCard(battle.PlayerHand[0]);
        battle.PlayCard(battle.PlayerHand[0]);
        battle.PlayCard(battle.PlayerHand[0]);

        var played = battle.PlayCard(battle.PlayerHand[0]);

        Assert.False(played);
        Assert.Equal(0, battle.PlayerEnergy);
        Assert.Equal(6, battle.Enemy.Health);
        Assert.Equal(2, battle.PlayerHand.Count);
    }

    [Fact]
    public void Card_Not_In_Hand_Is_Rejected_And_Costs_Nothing() {
        var battle = BattleWithDeckOf(0, 15);
        battle.StartTurn();

        var played = battle.PlayCard(new Card(0));

        Assert.False(played);
        Assert.Equal(3, battle.PlayerEnergy);
        Assert.Equal(15, battle.Enemy.Health);
    }
}