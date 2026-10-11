using Godot;

public enum IconKind
{
	Enemy, Elite, Boss, Rest, Chest, Event, Shop, Fusion, Upgrade, Remove, Evolve,
	Deck, Map, Gold, Settings, Health, Block, Planet, Heat, Wet, Charge, Pressure, Card,
}

public static class Icons
{
	public static Texture2D Texture(IconKind kind) =>
		GD.Load<Texture2D>($"res://art/icons/{kind.ToString().ToLowerInvariant()}.svg");

	public static Color Tint(IconKind kind) => kind switch
	{
		IconKind.Elite => new Color("e8692e"),
		IconKind.Boss => new Color("e2483a"),
		IconKind.Rest => new Color("8db7e0"),
		IconKind.Chest => new Color("f2a65a"),
		IconKind.Event => new Color("c9a0dc"),
		IconKind.Shop => new Color("e0b455"),
		IconKind.Fusion => new Color("7ccb6e"),
		IconKind.Upgrade => new Color("6fd3b5"),
		IconKind.Remove => new Color("e07a86"),
		IconKind.Evolve => new Color("b5d86a"),
		IconKind.Gold => new Color("e0b455"),
		IconKind.Health => new Color("e36c7b"),
		IconKind.Block => new Color("7fb0e0"),
		IconKind.Planet => new Color("9fc6d8"),
		IconKind.Heat => new Color("e8692e"),
		IconKind.Wet => new Color("6fa8dc"),
		IconKind.Charge => new Color("f2d45c"),
		IconKind.Pressure => new Color("b8a9c9"),
		IconKind.Card => new Color("8fd47f"),
		IconKind.Deck or IconKind.Map or IconKind.Settings => new Color("c9ced8"),
		_ => new Color("e4e7ee"),
	};

	// Tooltip text for a map node of this kind. Placeholder wording.
	public static string Describe(IconKind kind) => kind switch
	{
		IconKind.Enemy => "Enemy: A fight.",
		IconKind.Elite => "Elite: A tougher fight with a better reward.",
		IconKind.Boss => "Boss: The guardian at the end of the floor.",
		IconKind.Rest => "Rest: Recover health or improve a card.",
		IconKind.Chest => "Chest: A free reward.",
		IconKind.Event => "Event: Something unexpected.",
		IconKind.Shop => "Shop: Spend gold on cards and services.",
		IconKind.Fusion => "Fusion: Fuse two cards into one.",
		IconKind.Upgrade => "Upgrade: Make a card stronger.",
		IconKind.Remove => "Remove: Take a card out of your deck.",
		IconKind.Evolve => "Evolve: Evolve a card.",
		_ => kind.ToString(),
	};
}
