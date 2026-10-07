using Godot;
using System;
using Roguelike.Rules;

public partial class MainMenu : Control
{
	public Game game;

	public override void _Ready()
	{
		game = GetNode<Session>("/root/Session").Game;

		var newRunButton = GetNode<Button>("CenterContainer/MarginContainer/VBoxContainer/NewRunButton");
		newRunButton.Pressed += OnNewRunButtonPressed;

		var quitButton = GetNode<Button>("CenterContainer/MarginContainer/VBoxContainer/QuitButton");
		quitButton.Pressed += OnQuitButtonPressed;
	}

	public override void _Process(double delta)
	{
	}

	private void OnNewRunButtonPressed()
	{
		game.ChooseMenuOption(MenuOption.NewRun);
		GetTree().ChangeSceneToFile("res://battle_screen.tscn");
	}

	private void OnQuitButtonPressed()
	{
		game.ChooseMenuOption(MenuOption.Quit);
		if (game.HasQuit) GetTree().Quit();
	}
}
