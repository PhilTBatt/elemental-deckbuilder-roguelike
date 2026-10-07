using Godot;
using System;
using Roguelike.Rules;

public partial class BattleScreen : Control
{
	public Game game;

	public override void _Ready()
	{
		game = GetNode<Session>("/root/Session").Game;

		var turnCount = game.Playthrough.Battle.TurnCount;
		var turnCountLabel = GetNode<Label>("%turnCountLabel");
		turnCountLabel.Text = turnCount.ToString();
	}

	public override void _Process(double delta)
	{
	}
}
