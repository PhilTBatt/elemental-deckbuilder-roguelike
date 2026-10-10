using Godot;
using System;
using Roguelike.Rules;
using System.Linq;

public partial class BattleScreen : Control
{
	public Game game;

	private PackedScene cardScene = GD.Load<PackedScene>("res://card_view.tscn");

	public override void _Ready()
	{
		game = GetNode<Session>("/root/Session").Game;

		var endTurnButton = GetNode<Button>("%EndTurnButton");
		endTurnButton.Pressed += OnEndTurnButtonPressed;

		var seed = game.Playthrough.Seed;
		var seedLabel = GetNode<Label>("%SeedLabel");
		seedLabel.Text = seed.ToString();

		UpdateLabels();
	}

	public override void _Process(double delta)
	{
	}

	private void OnEndTurnButtonPressed()
	{
		game.Playthrough.Battle.EndTurn();
		
		UpdateLabels();
	}

	public void UpdateLabels()
	{
		var turnCount = game.Playthrough.Battle.TurnCount;
		var turnCountLabel = GetNode<Label>("%TurnCountLabel");
		turnCountLabel.Text = turnCount.ToString();


		var playerHealth = game.Playthrough.Player.Health;
		var playerHealthLabel = GetNode<Label>("%PlayerHealth");
		playerHealthLabel.Text = "Health: " + playerHealth.ToString();

		var playerBlock = game.Playthrough.Player.Block;
		var playerBlockLabel = GetNode<Label>("%PlayerBlock");
		playerBlockLabel.Text = "Block: " +playerBlock.ToString();

		var playerStatus = game.Playthrough.Player.Statuses;
		var playerStatusLabel = GetNode<Label>("%PlayerStatuses");
		playerStatusLabel.Text = string.Join(", ", playerStatus.Select(status => $"{status.Kind}: {status.Stacks}"));


		var enemyHealth = game.Playthrough.Battle.Enemy.Health;
		var enemyHealthLabel = GetNode<Label>("%EnemyHealth");
		enemyHealthLabel.Text = "Health: " +enemyHealth.ToString();

		var enemyBlock = game.Playthrough.Battle.Enemy.Block;
		var enemyBlockLabel = GetNode<Label>("%EnemyBlock");
		enemyBlockLabel.Text = "Block: " +enemyBlock.ToString();

		var enemyStatus = game.Playthrough.Battle.Enemy.Statuses;
		var enemyStatusLabel = GetNode<Label>("%EnemyStatuses");
		enemyStatusLabel.Text = string.Join(", ", enemyStatus.Select(status => $"{status.Kind}: {status.Stacks}"));

		var enemyIntent = game.Playthrough.Battle.Enemy.Intent;
		var enemyIntentLabel = GetNode<Label>("%EnemyIntent");
		enemyIntentLabel.Text = $"{enemyIntent.Name}: {enemyIntent.Amount}";


		var goopAmount = game.Playthrough.Battle.PlayerEnergy;
		var goopAmountLabel = GetNode<Label>("%GoopAmountLabel");
		goopAmountLabel.Text = goopAmount.ToString();

		var drawPile = game.Playthrough.Battle.DrawPile;
		var drawPileLabel = GetNode<Label>("%DrawPileLabel");
		drawPileLabel.Text = drawPile.Count.ToString();

		var discardPile = game.Playthrough.Battle.DiscardPile;
		var discardPileLabel = GetNode<Label>("%DiscardPileLabel");
		discardPileLabel.Text = discardPile.Count.ToString();

		UpdateHand();
	}

	public void UpdateHand()
	{
		var hand = GetNode<HBoxContainer>("%Hand");
		foreach (var child in hand.GetChildren()) child.QueueFree();

		foreach (var card in game.Playthrough.Battle.PlayerHand)
		{
			var cardView = cardScene.Instantiate<CardView>();
			hand.AddChild(cardView);
			cardView.Setup(card);
			cardView.ClickArea.Pressed += () => OnCardPressed(card);
		}
	}

	private void OnCardPressed(Card card)
	{
		game.Playthrough.Battle.PlayCard(card);
		UpdateLabels();
	}
}
