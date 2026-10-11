using Godot;
public partial class TipButton : Button
{
	public override GodotObject _MakeCustomTooltip(string forText) =>
		string.IsNullOrWhiteSpace(forText) ? null : Tooltip.Build(forText);
}
