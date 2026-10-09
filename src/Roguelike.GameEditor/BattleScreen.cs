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
		var turnCountLabel = GetNode<Label>("%TurnCountLabel");
		turnCountLabel.Text = turnCount.ToString();

		var endTurnButton = GetNode<Button>("%EndTurnButton");
		endTurnButton.Pressed += OnEndTurnButtonPressed;
	}

	public override void _Process(double delta)
	{
	}

	private void OnEndTurnButtonPressed()
	{
		game.Playthrough.Battle.EndTurn();
		
		var turnCount = game.Playthrough.Battle.TurnCount;
		var turnCountLabel = GetNode<Label>("%TurnCountLabel");
		turnCountLabel.Text = turnCount.ToString();
	}
}
