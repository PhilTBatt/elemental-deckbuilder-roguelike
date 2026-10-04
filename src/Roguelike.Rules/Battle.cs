namespace Roguelike.Rules;

public enum BattleState {
    PlayerTurn,
    EnemyTurn,
    PlayerWins,
    EnemyWins
}

public class Battle {
    public BattleState State { get; private set; } = BattleState.PlayerTurn;
    public Player Player { get; }

    public Enemy Enemy { get; }

    public int PlayerEnergy { get; private set; }

    public bool IsOver => State == BattleState.PlayerWins || State == BattleState.EnemyWins;

    public Battle(Player player, Enemy enemy) {
        Player = player;
        Enemy = enemy;
    }

    public void StartTurn() {
        State = BattleState.PlayerTurn;
        PlayerEnergy = 3;
        Player.StartTurn();
    }

    public void EndTurn() {
        Player.EndTurn();
        ResolveResult();
        if (IsOver) {
            return;
        }
        State = BattleState.EnemyTurn;
        Enemy.StartTurn();
        Enemy.TakeAction(Player);
        ResolveResult();
        if (IsOver) {
            return;
        }
        Enemy.EndTurn();
    }

    public void StartBattle() {
        while (Player.IsAlive && Enemy.IsAlive) {
            ProcessTurn();
        }
        EndBattle();
    }

    public void ProcessTurn() {
        ResolveResult();
        if (IsOver) {
            return;
        }
        StartTurn();
        EndTurn();
        ResolveResult();
    }

    public void ResolveResult() {
        if (!Player.IsAlive) {
            State = BattleState.EnemyWins;
        } else if (!Enemy.IsAlive) {
            State = BattleState.PlayerWins;
        }
    }

    public void EndBattle() {
        if (State == BattleState.PlayerWins) {
            
        } else if (State == BattleState.EnemyWins) {
            
        }
    }
}