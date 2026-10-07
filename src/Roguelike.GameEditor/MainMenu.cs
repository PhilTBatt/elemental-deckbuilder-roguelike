using Godot;
using System;
using Roguelike.Rules;

public partial class MainMenu : Control
{
	private Game game = new Game();

	public override void _Ready()
	{
		var newRunButton = GetNode<Button>("CenterContainer/VBoxContainer/NewRunButton");
		newRunButton.Pressed += OnNewRunButtonPressed;

		var quitButton = GetNode<Button>("CenterContainer/VBoxContainer/QuitButton");
		quitButton.Pressed += OnQuitButtonPressed;
	}

	public override void _Process(double delta)
	{
	}

	private void OnNewRunButtonPressed()
	{
		game.ChooseMenuOption(MenuOption.NewRun);
		GD.Print($"Fight result: {game.Playthrough?.Battle?.State}");

	}

	private void OnQuitButtonPressed()
	{
		game.ChooseMenuOption(MenuOption.Quit);
		if (game.HasQuit) GetTree().Quit();
	}
}
