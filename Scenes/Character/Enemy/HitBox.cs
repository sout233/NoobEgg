using Godot;
using System;

public partial class HitBox : Area2D
{
	public Node2D Parent;

	public override void _Ready()
	{
		Parent = GetParent<Node2D>();
	}

	public override void _Process(double delta)
	{
	}
}
