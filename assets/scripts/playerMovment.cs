using Godot;
using System;

public class playerMovment : KinematicBody2D
{

	playerMovment otherPlayer;

	[Export]
	NodePath otherPlayerPath;

	[Export]
	NodePath ropeNode;

	[Export]
	float wallSlideSpeed = 100.0f;

	[Export]
	float maxYVel = 500.0f;

	[Export]
	float airDashMod = 0.5f;

	[Export]
	 float gravity;

	[Export]
	 int walkSpeed = 300;
	 int strafeSpeed = 100;

	[Export]
	int jumpStrength = 640;

	[Export]
	int player = -1;

	[Export]
	float wallNoSlideTime = 1.0f;

	float currentWallNoSlideTime = 0.0f;
	bool inAir = false;
	bool onWall = false;
	bool onFloor = false;

	public Vector2 velocity;
	
	double maxJumpVelocity;
	double minJumpVelocity;
	
	double maxJumpHeight = 5 * Globals.UNIT_SIZE;
	double minJumpHeight = 0.8 * Globals.UNIT_SIZE;
	double jumpDuration =  0.6;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		otherPlayer = (playerMovment)GetNode(otherPlayerPath);
		gravity =  2 * (float)maxJumpHeight / (float) Math.Pow(jumpDuration, 2);
		maxJumpVelocity = Math.Sqrt(2 * gravity * maxJumpHeight);
		minJumpVelocity = -Math.Sqrt(2 * gravity * minJumpHeight);
		
	}
	
	public override void _Input (InputEvent @event)
	{
		if(@event.IsActionReleased("player2_move_jump") && velocity.y < minJumpVelocity)
		{
			velocity.y = (float)minJumpVelocity;
		}
		
	}

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

		ropePhysics rope = (ropePhysics)GetNode(ropeNode);
		if (rope.Length <= Position.DistanceTo(otherPlayer.Position))
		{
			//rope.calculateRope(GetPath());
			Vector2 ropePullVector = (otherPlayer.Position - Position).Normalized();
			Vector2 parallelPart = (velocity.Dot(ropePullVector) / ropePullVector.Dot(ropePullVector)) * ropePullVector;
			Vector2 orthogonalPart = velocity - parallelPart;
			if (parallelPart.Dot(ropePullVector) < 0)
			{
				parallelPart = new Vector2(0, 0);
			}
			Vector2 newVelocity = parallelPart + orthogonalPart;
			velocity = newVelocity;
		}


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
