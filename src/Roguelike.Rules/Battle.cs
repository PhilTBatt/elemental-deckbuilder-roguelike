namespace Roguelike.Rules;

public class Battle {
    public Player Player { get; set; }

    public Enemy Enemy { get; set; }

    public Battle(Player player, Enemy enemy) {
        Player = player;
        Enemy = enemy;
    }
}
