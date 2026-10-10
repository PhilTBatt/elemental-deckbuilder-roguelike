namespace Roguelike.Rules;


public class Playthrough(int seed) {
    public int Seed { get; } = seed;

    public Random Random { get; } = new Random(seed);

    public int EncounterCount { get; private set; } = 0;

    public Player Player { get; private set; } = new Player();

    public Battle? Battle { get; private set; } = null;

    public bool IsOver => Player.Health == 0;

    public void StartPlaythrough()  {
        StartNextEncounter();
    }

    public void StartNextEncounter() {
        EncounterCount++;
        var enemy = new Enemy(20, [new DamageEffect(5), new BlockEffect(5), new DamageEffect(5)]);
        Battle = new Battle(Random, Player, enemy);
        Battle.StartBattle();

        ShowRewards();
    }

    public void ShowRewards() {
        
    }
}
