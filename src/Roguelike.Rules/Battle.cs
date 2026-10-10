namespace Roguelike.Rules;

public enum BattleState {
    PlayerTurn,
    EnemyTurn,
    PlayerWins,
    EnemyWins
}

public class Battle {
    public Random Random { get; }
    public BattleState State { get; private set; } = BattleState.PlayerTurn;

    public Player Player { get; }

    public Enemy Enemy { get; }

    public int PlayerEnergy { get; private set; }

    public bool IsOver => State == BattleState.PlayerWins || State == BattleState.EnemyWins;

    public List<Card> DrawPile { get; private set; } = [];

    public List<Card> PlayerHand { get; private set; } = [];

    public List<Card> DiscardPile { get; private set; } = [];

    public int TurnCount = 0;

    public Battle(Random random, Player player, Enemy enemy) {
        Random = random;
        Player = player;
        Enemy = enemy;
        DrawPile = [..Player.Deck];
        Shuffler.Shuffle(Random, DrawPile);
    }

    public void StartBattle() {
        StartTurn();
    }

    public void ProcessTurn() {
        ResolveResult();
        if (IsOver) return;

        StartTurn();
        ResolveResult();
        if (IsOver) return;
    
        EndTurn();
    }

    public void StartTurn() {
        State = BattleState.PlayerTurn;
        TurnCount++;
        PlayerEnergy = 3;
        for (int i = 0; i < 5; i++) PlayerDraw();

        Player.StartOfTurn();
    }

    public void EndTurn() {
        Player.EndOfTurn();
        DiscardHand();
        ResolveResult();
        if (IsOver) return;

        State = BattleState.EnemyTurn;
        Enemy.StartOfTurn();
        ResolveResult();
        if (IsOver) return;
        
        Enemy.TakeAction(this, Player);
        ResolveResult();
        if (IsOver) return;

        Enemy.EndOfTurn();
        ResolveResult();

        StartTurn();
        ResolveResult();
    }

    
    public void EndBattle() {
        if (State == BattleState.PlayerWins) {
            
        } else if (State == BattleState.EnemyWins) {
            
        }
    }

    public void ResolveResult() {
        if (!Player.IsAlive) {
            State = BattleState.EnemyWins;
            EndBattle();
        }
        else if (!Enemy.IsAlive) {
            State = BattleState.PlayerWins;
            EndBattle();
        }
    }

    public void PlayerDraw() {
        if (PlayerHand.Count >= 10) return;
        if (DrawPile.Count <= 0) {
            DrawPile.AddRange(DiscardPile);
            DiscardPile.Clear();
            Shuffler.Shuffle(Random, DrawPile);
        }

        if (DrawPile.Count == 0) return;
        
        var card = DrawPile[^1];
        DrawPile.RemoveAt(DrawPile.Count - 1);
        PlayerHand.Add(card);
    }

    public void DiscardHand() {
        DiscardPile.AddRange(PlayerHand);
        PlayerHand.Clear();
    }

    public bool PlayCard(Card card) {
        if (card.Cost > PlayerEnergy) return false;
        if (!PlayerHand.Remove(card)) return false;
        PlayerEnergy -= card.Cost;

        foreach (var effect in card.Effects) effect.Apply(this, Player, Enemy);
        DiscardPile.Add(card);
        ResolveResult();
        return true;
    }
}