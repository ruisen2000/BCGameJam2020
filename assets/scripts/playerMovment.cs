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
	float wallSlideSpeed = 100.0f;

	[Export]
	float maxYVel = 500.0f;

	[Export]
	float airDashMod = 0.5f;

	[Export]
	 float gravity = 200.0f;

	[Export]
	 int walkSpeed = 200;

	[Export]
	int jumpStrength = 400;

	[Export]
	int player = -1;

	[Export]
	float wallNoSlideTime = 1.0f;

	float currentWallNoSlideTime = 0.0f;
	bool inAir = false;
	bool onWall = false;
	bool onFloor = false;

	Vector2 velocity;
	

	public override void _PhysicsProcess(float delta)
	{

		if (!onWall)
		{
			velocity.y += delta * gravity;
			velocity.y = Math.Min(velocity.y, maxYVel);
		}
		else
		{
			currentWallNoSlideTime += delta;
			if(currentWallNoSlideTime < wallNoSlideTime)
			{

				velocity.y = 0;
			}
			else
			{
				velocity.y = wallSlideSpeed;

			}
			//GD.Print("On the wall!");
		}

		if (onFloor)
		{
			currentWallNoSlideTime = 0;
			inAir = false;
		}

		//Aight this is pretty bad I admit but w/e game jam!
		if (player == 0)
		{

			if (Input.IsActionPressed("player1_move_left"))
			{
				velocity.x =  -walkSpeed;
			}
			else if (Input.IsActionPressed("player1_move_right"))
			{
				velocity.x = walkSpeed;

			}
			else
			{
				velocity.x = 0;
			}

			if (Input.IsActionPressed("player1_move_jump"))
			{
				if (onFloor)
				{
					velocity.y = -jumpStrength;
				}
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

			if (Input.IsActionPressed("player2_move_jump"))
			{
				if (onFloor)
				{
					velocity.y = -jumpStrength;
				}
			}
		}

		if (inAir)
		{
			velocity.x *= airDashMod;
		}

		// We don't need to multiply velocity by delta because "MoveAndSlide" already takes delta time into account.

		// The second parameter of "MoveAndSlide" is the normal pointing up.
		// In the case of a 2D platformer, in Godot, upward is negative y, which translates to -1 as a normal.
		MoveAndSlide(velocity, new Vector2(0, -1));

		 onFloor = IsOnFloor();
		 onWall = IsOnWall();

		//  // Called every frame. 'delta' is the elapsed time since the previous frame.
		//  public override void _Process(float delta)
		//  {
		//      
		//  }
	}


	public void applyForce(Vector2 force)
	{
		velocity += force;
	}
}
