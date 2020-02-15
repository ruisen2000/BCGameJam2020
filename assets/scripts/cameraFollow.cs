using Godot;
using System;

public class cameraFollow : Camera2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	Node2D p1;
	Node2D p2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		p1 = (Node2D)GetNode("../Player 1");
		p2 = (Node2D)GetNode("../Player 2");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		Vector2 avgPos = (p1.Position + p2.Position) / 2;
		Position = avgPos;
	}
}
