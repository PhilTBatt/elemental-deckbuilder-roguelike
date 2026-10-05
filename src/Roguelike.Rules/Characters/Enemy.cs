namespace Roguelike.Rules;

public class Enemy : Character {
    public int Damage { get; private set; }

    public List<Effect> Moves { get; }

    private int nextMove;

    public Effect Intent => Moves[nextMove];   

    public Enemy(int health, List<Effect> moves) : base(health) {
        Moves = moves;
    }

    public void TakeAction(Battle battle, Player player) {
        Intent.Apply(battle, this, battle.Player);
        nextMove = (nextMove + 1) % Moves.Count;
    }
}