using Godot;
using System;
using Roguelike.Rules;

public partial class MainMenu : Control
{
	public Game game;

	public override void _Ready()
	{
		game = GetNode<Session>("/root/Session").Game;

		GetNode<Button>("%NewRunButton").Pressed += OnNewRunButtonPressed;

		GetNode<Button>("%QuitButton").Pressed += OnQuitButtonPressed;

		GetNode<Button>("%SettingsButton").Pressed += () => GetNode<SettingsPanel>("%SettingsPanel").Open();
	}

	public override void _Process(double delta)
	{
	}

	private void OnNewRunButtonPressed()
	{
		game.ChooseMenuOption(MenuOption.NewRun);
		GetTree().ChangeSceneToFile("res://screens/battle/battle_screen.tscn");
	}

	private void OnQuitButtonPressed()
	{
		game.ChooseMenuOption(MenuOption.Quit);
		if (game.HasQuit) GetTree().Quit();
	}
}
