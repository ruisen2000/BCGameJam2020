using Godot;
using System;

public class playerMovment : KinematicBody2D
{

	[Export]
	NodePath otherPlayerPath;

	[Export]
	NodePath ropeNode;

	[Export]
	float wallSlideSpeed = 100.0f;

	[Export]
	float maxXVel = 300.0f;
	float maxYVel = 500.0f;

	[Export]
	float airDashMod = 0.4f;

	[Export]
	 float gravity = 500;

	[Export]
	 int walkSpeed = 200;
	 int strafeSpeed = 100;

	[Export]
	int jumpStrength = 560;

	[Export]
	int player = -1;

	[Export]
	float wallNoSlideTime = 1.0f;

	playerMovment otherPlayer;

	float currentWallNoSlideTime = 0.0f;
	bool isAnchored = false;
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
		//gravity =  2 * (float)maxJumpHeight / (float) Math.Pow(jumpDuration, 2);
		//maxJumpVelocity = Math.Sqrt(2 * gravity * maxJumpHeight);
		//minJumpVelocity = -Math.Sqrt(2 * gravity * minJumpHeight);
		
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
		float airXAccel = 0.0f;

		if (onFloor || onWall)
		{
			isAnchored = true;
		}
		else
		{
			isAnchored = false;
		}

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
		}

		//Aight this is pretty bad I admit but w/e game jam!
		if (player == 0)
		{

			if (Input.IsActionPressed("player1_move_left"))
			{
				if (onFloor)
				{
					velocity.x = -walkSpeed;
				}
				else if (!onWall)
				{
					airXAccel = -walkSpeed * airDashMod;
				}
			}
			else if (Input.IsActionPressed("player1_move_right"))
			{
				if (onFloor)
				{
					velocity.x = walkSpeed;
				}
				else if (!onWall)
				{
					airXAccel = walkSpeed * airDashMod;
				}
			}
			else
			{
				if (onFloor)
				{
					velocity.x = 0;
				}
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
				if (onFloor)
				{
					velocity.x = -walkSpeed;
				}
				else if (!onWall)
				{
					airXAccel = -walkSpeed * airDashMod;
				}
			}
			else if (Input.IsActionPressed("player2_move_right"))
			{
				if (onFloor)
				{
					velocity.x = walkSpeed;
				}
				else if (!onWall)
				{
					airXAccel = walkSpeed * airDashMod;
				}
			}
			else
			{
				if (onFloor)
				{
					velocity.x = 0;
				}
			}
			if (Input.IsActionPressed("player2_move_jump"))
			{
				if (onFloor)
				{
					velocity.y = -jumpStrength;
				}
			}
		}

		// rope physics below
		ropePhysics rope = (ropePhysics)GetNode(ropeNode);
		if (rope.Length <= Position.DistanceTo(otherPlayer.Position))
		{
			Vector2 ropePullVector = (otherPlayer.Position - Position).Normalized();
			// tug on other player
			if (ropePullVector.Dot(velocity) < 0)
			{
				if (otherPlayer.isAnchored)
				{
					//otherPlayer.MoveAndSlide(velocity, new Vector2(0, -1));
					//velocity *= 0.9f;
				}
				else
				{
					otherPlayer.MoveAndSlide(velocity * 0.5f, new Vector2(0, -1));
					velocity *= 0.5f;
				}
			}

			if (rope.Length <= Position.DistanceTo(otherPlayer.Position))
			{
				float strafeInfluence;
				if ((-ropePullVector).Angle() > Math.PI)
				{
					strafeInfluence = 0;
				}
				else if (airXAccel > 0)
				{
					strafeInfluence = airXAccel * (float)(Math.Cos((-ropePullVector).Angle()) + 0.5);
				}
				else
				{
					strafeInfluence = airXAccel * (-1) * (float)(Math.Cos((-ropePullVector).Angle()) - 0.5);
				}
				//velocity = velocity + new Vector2(strafeInfluence, 0);
				Vector2 parallelPart = (velocity.Dot(ropePullVector) / ropePullVector.Dot(ropePullVector)) * ropePullVector;
				Vector2 orthogonalPart = (velocity - parallelPart) * 0.9f;
				if (parallelPart.Dot(ropePullVector) < 0)
				{
					parallelPart = new Vector2(0, 0);
				}
				Vector2 strafeVector = new Vector2(strafeInfluence * (float)Math.Cos(velocity.Normalized().x), strafeInfluence * (float)Math.Sin(velocity.Normalized().y));
				Vector2 newVelocity = parallelPart + orthogonalPart + strafeVector;
				velocity = newVelocity;
			}
		}

		if (GetSlideCount() > 0 && GetSlideCollision(0) != null && !onFloor)
		{
			velocity = new Vector2(0, 0);
			velocity.y += delta * gravity;
			velocity.y = Math.Min(velocity.y, maxYVel);
		}

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
