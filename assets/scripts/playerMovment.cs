using Godot;
using System;

public class playerMovment : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}
	[Export]
	 float gravity = 200.0f;

	[Export]
	 int walkSpeed = 200;

	[Export]
	int player = -1;

	Vector2 velocity;

	public override void _PhysicsProcess(float delta)
	{
		velocity.y += delta * gravity;

		if(player == 0)
		{

			if (Input.IsActionPressed("player1_move_left"))
			{
				velocity.x = -walkSpeed;
			}
			else if (Input.IsActionPressed("player1_move_right"))
			{
				velocity.x = walkSpeed;
			}
			else
			{
				velocity.x = 0;
			}
		}
		else
		{

			if (Input.IsActionPressed("player2_move_left"))
			{
				velocity.x = -walkSpeed;
			}
			else if (Input.IsActionPressed("player2_move_right"))
			{
				velocity.x = walkSpeed;
			}
			else
			{
				velocity.x = 0;
			}
		}


		// We don't need to multiply velocity by delta because "MoveAndSlide" already takes delta time into account.

		// The second parameter of "MoveAndSlide" is the normal pointing up.
		// In the case of a 2D platformer, in Godot, upward is negative y, which translates to -1 as a normal.
		MoveAndSlide(velocity, new Vector2(0, -1));

		//  // Called every frame. 'delta' is the elapsed time since the previous frame.
		//  public override void _Process(float delta)
		//  {
		//      
		//  }
	}
}
