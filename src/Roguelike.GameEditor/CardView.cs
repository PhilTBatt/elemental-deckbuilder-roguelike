using Godot;
using Roguelike.Rules;

public partial class CardView : PanelContainer
{
	public Button ClickArea => GetNode<Button>("%ClickArea");

	public void Setup(Card card)
	{
		GetNode<Label>("%CostLabel").Text = card.Cost.ToString();
		GetNode<Label>("%NameLabel").Text = card.Name;
		GetNode<Label>("%TypeLabel").Text = card.CardType.ToString();
		GetNode<Label>("%DescriptionLabel").Text = card.Description;
	}
}