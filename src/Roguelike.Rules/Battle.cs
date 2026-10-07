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

    public List<Card> PlayerHand { get; private set; } = new List<Card>();

    public List<Card> DiscardPile { get; private set; } = [];

    public int TurnNumber = 0;

    public Battle(Random random, Player player, Enemy enemy) {
        Random = random;
        Player = player;
        Enemy = enemy;
        DrawPile = [..Player.Deck];
        Shuffler.Shuffle(Random, DrawPile);
    }

    public void StartTurn() {
        State = BattleState.PlayerTurn;
        TurnNumber++;
        PlayerEnergy = 3;
        for (int i = 0; i < 5; i++) PlayerDraw();

        Player.StartTurn();
    }

    public void EndTurn() {
        Player.EndTurn();
        DiscardHand();

        ResolveResult();
        if (IsOver) return;

        State = BattleState.EnemyTurn;
        Enemy.StartTurn();
        Enemy.TakeAction(this, Player);

        ResolveResult();
        if (IsOver) return;

        Enemy.EndTurn();
        ResolveResult();
    }

    public void StartBattle() {
        while (Player.IsAlive && Enemy.IsAlive) {
            ProcessTurn();
        }

        EndBattle();
    }

    public void ProcessTurn() {
        ResolveResult();
        if (IsOver) return;

        StartTurn();
        ResolveResult();
        if (IsOver) return;

        PlayPlayerTurn();
        ResolveResult();
        if (IsOver) return;
    
        EndTurn();
    }

    public void ResolveResult() {
        if (!Player.IsAlive) State = BattleState.EnemyWins;
        else if (!Enemy.IsAlive) State = BattleState.PlayerWins;
    }

    public void EndBattle() {
        if (State == BattleState.PlayerWins) {
            
        } else if (State == BattleState.EnemyWins) {
            
        }
    }

    public void DiscardHand() {
        DiscardPile.AddRange(PlayerHand);
        PlayerHand.Clear();
    }

    public void PlayerDraw() {
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

    public bool PlayCard(Card card) {
        if (card.Cost > PlayerEnergy) return false;
        if (!PlayerHand.Remove(card)) return false;
        PlayerEnergy -= card.Cost;

        foreach (var effect in card.Effects) effect.Apply(this, Player, Enemy);
        DiscardPile.Add(card);
        return true;
    }

    public void PlayPlayerTurn() {
        foreach (var card in PlayerHand.ToList()) {
            PlayCard(card);
            ResolveResult();
            if (IsOver) return;
        }
    }
}