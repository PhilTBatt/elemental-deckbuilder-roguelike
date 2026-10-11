using Godot;

// The bar across the top of every run screen. Call the Set methods to fill it in.
// Settings opens by itself; hook up the map and deck buttons from your screen's script.
public partial class RunHud : PanelContainer
{
	public Button MapButton => GetNode<Button>("%MapButton");
	public Button DeckButton => GetNode<Button>("%DeckButton");

	public override void _Ready()
	{
		GetNode<Button>("%SettingsButton").Pressed += () => GetNode<SettingsPanel>("%SettingsPanel").Open();
	}

	public void SetHealth(int current, int? max = null) =>
		GetNode<Button>("%HealthButton").Text = max is null ? current.ToString() : $"{current}/{max}";

	public void SetGold(int gold) => GetNode<Button>("%GoldButton").Text = gold.ToString();

	public void SetDeckCount(int count) => GetNode<Button>("%DeckButton").Text = count.ToString();

	public void SetFloor(int floor, int node) => GetNode<Label>("%FloorLabel").Text = $"Floor {floor}  ·  Node {node}";
}
