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

    public int TurnCount { get; private set; }  = 0;

    public EffectQueue EffectQueue { get; } = new EffectQueue();

    public Battle(Random random, Player player, Enemy enemy) {
        Random = random;
        Player = player;
        Enemy = enemy;
        DrawPile = [..Player.Deck];
        Shuffler.Shuffle(Random, DrawPile);
    }

    public void StartOfBattle() {
        StartTurn();
    }

    public void StartTurn() {
        State = BattleState.PlayerTurn;
        TurnCount++;
        PlayerEnergy = 3;
        for (int i = 0; i < 5; i++) PlayerDraw();

        Player.StartOfTurn();
    }

    public void EndTurn() => RunSteps(
        Player.EndOfTurn,
        DiscardHand,
        Enemy.StartOfTurn,
        () => State = BattleState.EnemyTurn,
        () => Enemy.TakeAction(this, Player),
        Enemy.EndOfTurn,
        StartTurn
    );

    public void DiscardHand() {
        DiscardPile.AddRange(PlayerHand);
        PlayerHand.Clear();
    }

    private void RunSteps(params Action[] steps) {
        foreach (var step in steps) {
            if (IsOver) return;
            step();
            ResolveResult();
        }
    }

    public void ResolveResult() {
        if (IsOver) return;

        if (!Player.IsAlive) {
            State = BattleState.EnemyWins;
            EndBattle();
        }
        else if (!Enemy.IsAlive) {
            State = BattleState.PlayerWins;
            EndBattle();
        }
    }

    public void EndBattle() {
        if (State == BattleState.PlayerWins) {
            
        } else if (State == BattleState.EnemyWins) {
            
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

    public bool PlayCard(Card card) {
        if (card.Cost > PlayerEnergy) return false;
        if (!PlayerHand.Remove(card)) return false;
        PlayerEnergy -= card.Cost;

        foreach (var effect in card.Effects) EffectQueue.AddToBottom(effect, Player, Enemy);
        RunQueue();
        DiscardPile.Add(card);
        ResolveResult();
        return true;
    }

    public void RunQueue() {
        while (!EffectQueue.IsEmpty && !IsOver) {
            var next = EffectQueue.TakeNext();
            next.Effect.Apply(this, next.Caster, next.Target);
            ResolveResult();
        }
    }
}