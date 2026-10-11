using Godot;

public partial class SettingsPanel : Control
{
	public override void _Ready()
	{
		GetNode<Button>("%CloseButton").Pressed += Close;
	}

	public void Open()
	{
		Show();
		GetViewport().UpdateMouseCursorState();
	}

	public void Close() => Hide();

	public override void _UnhandledInput(InputEvent @event)
	{
		if (Visible && @event.IsActionPressed("ui_cancel"))
		{
			Close();
			GetViewport().SetInputAsHandled();
		}
	}
}
