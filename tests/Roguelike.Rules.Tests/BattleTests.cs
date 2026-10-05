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

    private static Player PlayerWithDeck(int size) {
        var player = new Player(40);
        for (int i = 0; i < size; i++) {
            player.Deck.Add(new Card(0));
        }
        return player;
    }

    private static void AssertEveryCardIsInOnePile(Player player, Battle battle) {
        List<Card> allPiles = [..battle.DrawPile, ..battle.PlayerHand, ..battle.DiscardPile];

        Assert.Equal(player.Deck.Count, allPiles.Count);
        Assert.All(player.Deck, card => Assert.Contains(card, allPiles));
    }

    [Fact]
    public void New_Battle_Puts_The_Whole_Deck_In_The_Draw_Pile() {
        var player = PlayerWithDeck(10);

        var battle = new Battle(player, new Enemy(15, 4));

        Assert.Equal(10, battle.DrawPile.Count);
        Assert.All(player.Deck, card => Assert.Contains(card, battle.DrawPile));
        Assert.Empty(battle.PlayerHand);
        Assert.Empty(battle.DiscardPile);
    }

    [Fact]
    public void Start_Of_Turn_Draws_Five_Cards() {
        var battle = new Battle(PlayerWithDeck(10), new Enemy(15, 4));

        battle.StartTurn();

        Assert.Equal(5, battle.PlayerHand.Count);
        Assert.Equal(5, battle.DrawPile.Count);
        Assert.Empty(battle.DiscardPile);
    }

    [Fact]
    public void Drawing_Leaves_The_Deck_Alone() {
        var player = PlayerWithDeck(10);
        var battle = new Battle(player, new Enemy(15, 4));

        battle.StartTurn();

        Assert.Equal(10, player.Deck.Count);
    }

    [Fact]
    public void End_Of_Turn_Discards_The_Hand() {
        var battle = new Battle(PlayerWithDeck(10), new Enemy(15, 4));

        battle.StartTurn();
        battle.EndTurn();

        Assert.Empty(battle.PlayerHand);
        Assert.Equal(5, battle.DiscardPile.Count);
        Assert.Equal(5, battle.DrawPile.Count);
    }

    [Fact]
    public void Empty_Draw_Pile_Is_Refilled_From_The_Discard_Pile() {
        var battle = new Battle(PlayerWithDeck(10), new Enemy(15, 4));
        battle.StartTurn();
        battle.EndTurn();
        battle.StartTurn();
        battle.EndTurn();
        Assert.Empty(battle.DrawPile);

        battle.StartTurn();

        Assert.Equal(5, battle.PlayerHand.Count);
        Assert.Equal(5, battle.DrawPile.Count);
        Assert.Empty(battle.DiscardPile);
    }

    [Fact]
    public void Cards_Are_Never_Lost_Or_Duplicated() {
        var player = PlayerWithDeck(10);
        var battle = new Battle(player, new Enemy(15, 4));

        for (int turn = 0; turn < 6; turn++) {
            battle.StartTurn();
            AssertEveryCardIsInOnePile(player, battle);

            battle.EndTurn();
            AssertEveryCardIsInOnePile(player, battle);
        }
    }

    [Fact]
    public void Deck_Smaller_Than_A_Hand_Draws_What_It_Has() {
        var battle = new Battle(PlayerWithDeck(3), new Enemy(15, 4));

        battle.StartTurn();

        Assert.Equal(3, battle.PlayerHand.Count);
        Assert.Empty(battle.DrawPile);
        Assert.Empty(battle.DiscardPile);
    }
}