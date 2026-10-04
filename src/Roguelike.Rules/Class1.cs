namespace Roguelike.Rules;

public class Character {
    public int Health { get; set; } = 40;
}

public class Player : Character {
}

public class Enemy : Character {
}

public class Battle {
    public Player Player { get; set; } = new Player();

    public Enemy Enemy { get; set; } = new Enemy();
}
