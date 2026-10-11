using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Rules;

public static class Keywords
{
	public static readonly Dictionary<string, string> Text = new()
	{
		["Goop"] = "Spend it to play cards. Refills to 3 at the start of your turn.",
		["Block"] = "Stops damage. Wears off at the start of your next turn.",
		["Burn"] = "At the end of its turn, takes 1 damage per stack, then loses 1 stack.",
		["Shock"] = "When hit, every enemy takes 1 damage per stack, then loses 1 stack.",
		["Frost"] = "Its next attack is weakened.",
		["Brittle"] = "Takes more damage from attacks.",
		["Exhaust"] = "Removed for the rest of the fight once played.",
		["Heat"] = "How hot the planet is.",
		["Wet"] = "How much water there is.",
		["Charge"] = "How much static is in the air.",
		["Pressure"] = "How heavy the air is.",
	};

	private static readonly Dictionary<string, Color> Colours = new()
	{
		["Goop"] = new Color("8fd47f"),
		["Block"] = new Color("7fb0e0"),
		["Burn"] = new Color("f29a5c"),
		["Shock"] = new Color("f2d45c"),
		["Frost"] = new Color("9fd8ec"),
		["Brittle"] = new Color("c9b8e0"),
		["Exhaust"] = new Color("9aa3b5"),
		["Health"] = new Color("e36c7b"),
		["Gold"] = new Color("e0b455"),
	};

	public static Color ColorFor(string name)
	{
		if (Colours.TryGetValue(name, out var colour)) return colour;
		if (Enum.TryParse<IconKind>(name, out var kind)) return Icons.Tint(kind);
		return new Color("e4e7ee");
	}
 
	public static string For(IEnumerable<string> names) =>
		string.Join("\n", names.Distinct().Where(Text.ContainsKey).Select(name => $"{name}: {Text[name]}"));
 
	public static string ForCard(Card card)
	{
		var names = new List<string>();
		foreach (var effect in card.Effects)
		{
			if (effect.Kind == EffectType.Block) names.Add("Block");
		}
		return For(names);
	}
}
