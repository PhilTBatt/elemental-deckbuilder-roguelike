using System.Security.Cryptography;
using System.Text;

namespace Roguelike.Rules;


public class Playthrough(int seed) {
    public int Seed { get; } = seed;

    public Random GoldRandom { get; } = MakeRandom(seed, "gold");

    public Random CardRewardRandom { get; } = MakeRandom(seed, "card reward");

    public int EncounterCount { get; private set; } = 0;

    public Player Player { get; private set; } = new Player();

    public Map Map { get; } = Map.FirstFloor();

    public Map Map { get; } = Map.FirstFloor();

    public Battle? Battle { get; private set; } = null;

    public int GoldReward { get; private set; } = 0;

    public List<Card> CardChoices { get; private set; } = [];

    public bool IsOver => !Player.IsAlive;

    public void StartPlaythrough()  {
        StartNextEncounter();
    }

    public void StartNextEncounter() {
        EncounterCount++;
        GoldReward = 0;
        CardChoices = [];

        var enemy = new Slime();
        var shuffleRandom = MakeRandom(Seed, $"shuffle {EncounterCount}");
        Battle = new Battle(shuffleRandom, Player, enemy);
        Battle.Ended += OnBattleEnded;
        Battle.StartOfBattle();
    }

    private void OnBattleEnded(BattleState result) {
        if (result == BattleState.PlayerWins) {
            GoldReward = GoldRandom.Next(15, 26);
            CardChoices = Card.PickRewards(CardRewardRandom, 3);
        }
    }

    public void TakeGoldReward() {
        Player.GainGold(GoldReward);
        GoldReward = 0;
    }

    public void TakeCardReward(Card card) {
        if (!CardChoices.Contains(card)) return;
        Player.Deck.Add(card);
        CardChoices.Clear();
    }

    private static Random MakeRandom(int seed, string name) {
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes($"{seed}:{name}"));
        return new Random(BitConverter.ToInt32(hash, 0));
    }
}
