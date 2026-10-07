using Godot;
using System;
using Roguelike.Rules;

public partial class Session : Node
{
	public Game Game { get; } = new Game();

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
}
