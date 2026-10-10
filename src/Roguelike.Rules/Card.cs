namespace Roguelike.Rules;

public enum CardType { Attack, Skill }

public class Card {
    public int Id { get; private set; }

    public string Name { get; private set; }

    public int Cost { get; private set; }

    public List<Effect> Effects { get; private set; } = [];

    public string Description => string.Join("\n", Effects.Select(effect => effect.Description));

    public CardType CardType => Effects.Any(effect => effect.Kind == EffectType.Attack) ? CardType.Attack : CardType.Skill;

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
                
            default:
                throw new ArgumentException("No card has id " + id);
        }
    }
}