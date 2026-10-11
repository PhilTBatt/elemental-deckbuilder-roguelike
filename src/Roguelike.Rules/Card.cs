namespace Roguelike.Rules;

public enum CardType { Attack, Skill }

public class Card {
    public int Id { get; private set; }

    public string Name { get; private set; }

    public int Cost { get; private set; }

    public List<Effect> Effects { get; private set; } = [];

    public string Description => string.Join("\n", Effects.Select(effect => effect.Description));

    public CardType CardType => Effects.Any(effect => effect.Kind == EffectType.Attack) ? CardType.Attack : CardType.Skill;

    public static readonly int[] RewardPool = [2, 3, 4, 5];

    public static List<Card> PickRewards(Random random, int count) {
        var pool = new List<int>(RewardPool);
        var picks = new List<Card>();
        while (picks.Count < count && pool.Count > 0) {
            int index = random.Next(pool.Count);
            picks.Add(new Card(pool[index]));
            pool.RemoveAt(index);
        }
        return picks;
    }

    public Card(int id) {
        Id = id;
        switch (id) {
            case 0:
                Name = "Strike";
                Cost = 1;
                Effects = [new Effect(EffectType.Attack, 3)];
                break;
                
            case 1:
                Name = "Defend";
                Cost = 1;
                Effects = [new Effect(EffectType.Block, 3)];
                break;

            case 2:
                Name = "Ignite";
                Cost = 1;
                Effects = [new Effect(EffectType.Burn, 2)];
                break;

            case 3:
                Name = "Kindle";
                Cost = 0;
                Effects = [new Effect(EffectType.Burn, 1)];
                break;

            case 4:
                Name = "Ember";
                Cost = 1;
                Effects = [new Effect(EffectType.Attack, 2), new Effect(EffectType.Burn, 1)];
                break;

            case 5:
                Name = "Fireball";
                Cost = 2;
                Effects = [new Effect(EffectType.Attack, 6), new Effect(EffectType.Burn, 1)];
                break;
                
            default:
                throw new ArgumentException("No card has id " + id);
        }
    }
}