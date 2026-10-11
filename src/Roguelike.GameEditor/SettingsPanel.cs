using Godot;

// The settings pop-up. It only opens and closes for now; the controls inside are placeholders.
public partial class SettingsPanel : Control
{
	public override void _Ready()
	{
		GetNode<Button>("%CloseButton").Pressed += Close;
	}

	public void Open()
	{
		Show();
		// Tell Godot the mouse is now over the panel, so a tooltip from the button
		// you just clicked doesn't pop up on top of it.
		GetViewport().UpdateMouseCursorState();
	}

	public void Close() => Hide();

	// Escape (the built-in "ui_cancel" action) closes the panel too.
	public override void _UnhandledInput(InputEvent @event)
	{
		if (Visible && @event.IsActionPressed("ui_cancel"))
		{
			Close();
			GetViewport().SetInputAsHandled();
		}
	}
}
