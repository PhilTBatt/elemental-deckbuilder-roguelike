using Godot;
using System;
using Roguelike.Rules;
using System.Linq;

public partial class BattleScreen : Control
{
	public Game game;

	private PackedScene cardScene = GD.Load<PackedScene>("res://ui/card_view.tscn");

	public override void _Ready()
	{
		game = GetNode<Session>("/root/Session").Game;

		GetNode<Button>("%EndTurnButton").Pressed += OnEndTurnButtonPressed;

		GetNode<Label>("%SeedLabel").Text = game.Playthrough.Seed.ToString();

		GetNode<Button>("%BackButton").Pressed += () => Screens.GoTo(this, Screen.Menu);

		var cardReward = GetNode<Control>("%CardRewardScreen");
		cardReward.GetNode<Button>("%SkipButton").Pressed += () => cardReward.Visible = false;
		
		var rewards = GetNode<Control>("%RewardsPanel");
		rewards.GetNode<Button>("%CardRewardButton").Pressed += OnCardRewardPressed;
		rewards.GetNode<Button>("%GoldRewardButton").Pressed += OnGoldRewardPressed;
		rewards.GetNode<Button>("%ContinueButton").Pressed += OnContinuePressed;

		UpdateLabels();
	}

	public override void _Process(double delta)
	{
	}

	private void OnEndTurnButtonPressed()
	{
		game.Playthrough.Battle.EndTurn();
		UpdateLabels();
		ShowRewards();
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
		GetNode<Label>("%EnemyIntent").Text = $"{game.Playthrough.Battle.Enemy.Intent.Kind}: {game.Playthrough.Battle.Enemy.Intent.Amount}";

		GetNode<Label>("%GoopAmountLabel").Text = game.Playthrough.Battle.PlayerEnergy.ToString();
		GetNode<Label>("%DrawPileLabel").Text = game.Playthrough.Battle.DrawPile.Count.ToString();
		GetNode<Label>("%DiscardPileLabel").Text = game.Playthrough.Battle.DiscardPile.Count.ToString();

		var hud = GetNode<RunHud>("%RunHud");
		hud.SetHealth(game.Playthrough.Player.Health);
		hud.SetGold(game.Playthrough.Player.Gold);
		hud.SetDeckCount(game.Playthrough.Player.Deck.Count);
		hud.SetFloor(1, game.Playthrough.EncounterCount);

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
			cardView.ClickArea.Disabled = !canAfford;
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
		if (battle.State == BattleState.PlayerWins)
		{
			var rewards = GetNode<Control>("%RewardsPanel");
			rewards.GetNode<Button>("%GoldRewardButton").Text = $"{game.Playthrough.GoldReward} Gold";
			rewards.Visible = true;
		}
		else if (battle.State == BattleState.EnemyWins)
		{
			GetNode<Label>("%ResultsLabel").Text = "You lose";
			GetNode<Control>("%ResultsOverlay").Visible = true;
		}
	}
	private void OnGoldRewardPressed()
	{
		game.Playthrough.TakeGoldReward();
		GetNode<Control>("%RewardsPanel").GetNode<Button>("%GoldRewardButton").Visible = false;
		UpdateLabels();
	}

	private void OnCardRewardPressed()
	{
		var screen = GetNode<Control>("%CardRewardScreen");
		var row = screen.GetNode<HBoxContainer>("%RewardCards");
		foreach (var child in row.GetChildren()) child.QueueFree();

		foreach (var card in game.Playthrough.CardChoices)
		{
			var cardView = cardScene.Instantiate<CardView>();
			cardView.CustomMinimumSize = new Vector2(172, 240);
			row.AddChild(cardView);
			cardView.Setup(card);
			cardView.ClickArea.Pressed += () => OnRewardCardPicked(card);
		}
		screen.Visible = true;
	}

	private void OnRewardCardPicked(Card card)
	{
		game.Playthrough.TakeCardReward(card);
		GetNode<Control>("%CardRewardScreen").Visible = false;
		GetNode<Control>("%RewardsPanel").GetNode<Button>("%CardRewardButton").Visible = false;
		UpdateLabels();
	}

	private void OnContinuePressed()
	{
		game.Playthrough.StartNextEncounter();
		GetTree().ReloadCurrentScene();
	}
}
