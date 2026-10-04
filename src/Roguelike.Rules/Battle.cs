namespace Roguelike.Rules;

public enum BattleState {
    PlayerTurn,
    EnemyTurn
}

public class Battle {

    public BattleState State { get; private set; } = BattleState.PlayerTurn;
    public Player Player { get; }

    public Enemy Enemy { get; }

    public Battle(Player player, Enemy enemy) {
        Player = player;
        Enemy = enemy;
    }

    public void EndTurn() {
    }

    public void StartBattle() {
        while (Player.IsAlive && Enemy.IsAlive) {
            ProcessOneTurn();
        }
    }

    public void ProcessOneTurn() {
        TakePlayerTurn();
        EndTurn();
        if (Enemy.IsAlive) {
            TakeEnemyTurn();
        }
    }

    public void TakePlayerTurn() {
        State = BattleState.PlayerTurn;
        Enemy.TakeDamage(5);
    }

    public void TakeEnemyTurn() {
        State = BattleState.EnemyTurn;
        Player.TakeDamage(Enemy.Damage);
    }
}