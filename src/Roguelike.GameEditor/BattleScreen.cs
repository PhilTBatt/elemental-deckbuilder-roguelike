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

		GetNode<Button>("%EndTurnButton").Pressed += OnEndTurnButtonPressed;

		GetNode<Label>("%SeedLabel").Text = game.Playthrough.Seed.ToString();

		GetNode<Button>("%BackButton").Pressed += () => GetTree().ChangeSceneToFile("res://main_menu.tscn");

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
		GetNode<Label>("%TurnCountLabel").Text = turnCount.ToString();


		var playerHealth = game.Playthrough.Player.Health;
		GetNode<Label>("%PlayerHealth").Text = "Health: " + playerHealth.ToString();

		var playerBlock = game.Playthrough.Player.Block;
		GetNode<Label>("%PlayerBlock").Text = "Block: " +playerBlock.ToString();

		var playerStatus = game.Playthrough.Player.Statuses;
		GetNode<Label>("%PlayerStatuses").Text = string.Join(", ", playerStatus.Select(status => $"{status.Kind}: {status.Stacks}"));


		var enemyHealth = game.Playthrough.Battle.Enemy.Health;
		GetNode<Label>("%EnemyHealth").Text = "Health: " +enemyHealth.ToString();

		var enemyBlock = game.Playthrough.Battle.Enemy.Block;
		GetNode<Label>("%EnemyBlock").Text = "Block: " +enemyBlock.ToString();

		var enemyStatus = game.Playthrough.Battle.Enemy.Statuses;
		GetNode<Label>("%EnemyStatuses").Text = string.Join(", ", enemyStatus.Select(status => $"{status.Kind}: {status.Stacks}"));

		var enemyIntent = game.Playthrough.Battle.Enemy.Intent;
		GetNode<Label>("%EnemyIntent").Text = $"{enemyIntent.Name}: {enemyIntent.Amount}";


		var goopAmount = game.Playthrough.Battle.PlayerEnergy;
		GetNode<Label>("%GoopAmountLabel").Text = goopAmount.ToString();

		var drawPile = game.Playthrough.Battle.DrawPile;
		GetNode<Label>("%DrawPileLabel").Text = drawPile.Count.ToString();

		var discardPile = game.Playthrough.Battle.DiscardPile;
		GetNode<Label>("%DiscardPileLabel").Text = discardPile.Count.ToString();

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

			var canAfford = card.Cost <= game.Playthrough.Battle.PlayerEnergy;
			cardView.ClickArea.Visible = canAfford;
			cardView.Modulate = canAfford ? Colors.White : new Color(1, 1, 1, 0.4f);
		}
	}

	private void OnCardPressed(Card card)
	{
		game.Playthrough.Battle.PlayCard(card);
		UpdateLabels();
		ShowRewards();
	}

	private void ShowRewards()
	{
		var battle = game.Playthrough.Battle;
		GetNode<Control>("%ResultsOverlay").Visible = battle.IsOver;
		GetNode<Label>("%ResultsLabel").Text = battle.State == BattleState.PlayerWins ? "You win" : "You lose";
	}
}
