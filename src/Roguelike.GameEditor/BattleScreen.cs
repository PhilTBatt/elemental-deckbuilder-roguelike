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
		GetNode<Label>("%TurnCountLabel").Text = game.Playthrough.Battle.TurnCount.ToString();


		GetNode<Label>("%PlayerHealth").Text = "Health: " + game.Playthrough.Player.Health.ToString();

		GetNode<Label>("%PlayerBlock").Text = "Block: " + game.Playthrough.Player.Block.ToString();

		GetNode<Label>("%PlayerStatuses").Text = string.Join(", ", game.Playthrough.Player.Statuses.Select(status => $"{status.Kind}: {status.Stacks}"));


		GetNode<Label>("%EnemyHealth").Text = "Health: " + game.Playthrough.Battle.Enemy.Health.ToString();

		GetNode<Label>("%EnemyBlock").Text = "Block: " + game.Playthrough.Battle.Enemy.Block.ToString();

		GetNode<Label>("%EnemyStatuses").Text = string.Join(", ", game.Playthrough.Battle.Enemy.Statuses.Select(status => $"{status.Kind}: {status.Stacks}"));

		GetNode<Label>("%EnemyIntent").Text = $"{game.Playthrough.Battle.Enemy.Intent.Name}: {game.Playthrough.Battle.Enemy.Intent.Amount}";


		GetNode<Label>("%GoopAmountLabel").Text = game.Playthrough.Battle.PlayerEnergy.ToString();

		GetNode<Label>("%DrawPileLabel").Text = game.Playthrough.Battle.DrawPile.Count.ToString();

		GetNode<Label>("%DiscardPileLabel").Text = game.Playthrough.Battle.DiscardPile.Count.ToString();

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
