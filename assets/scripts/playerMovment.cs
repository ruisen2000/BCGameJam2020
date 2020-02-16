using Godot;
using System;

public class playerMovment : KinematicBody2D
{
	
	[Export]
	NodePath animationsPath;

	[Export]
	float ropeLength = 300.0f;

	[Export]
	float wallSlideSpeed = 130.0f;

	[Export]
	public float maxXVel = 300.0f;
	[Export]
	public float maxYVel = 500.0f;

	[Export]
	float airDashMod = 10f;

	[Export]
	public float gravity = 1000f;

	[Export]
	 int walkSpeed = 300;
	 int strafeSpeed = 100;

	[Export]
	int jumpStrength = 1000;

	[Export]
	int player = -1;

	[Export]
	float wallNoSlideTime = 0.2f;

	[Export]
	NodePath jump1;

	[Export]
	 NodePath jump2;

	[Export]
	NodePath jump3;


	[Export]
	NodePath fall1;

	[Export]
	NodePath fall2;

	[Export]
	NodePath fall3;

	[Export]
	float wallJumpCoolDown = 1.0f;

	float currentWallJumpCoolDown = 0.0f;

	AudioStreamPlayer jj1;
	AudioStreamPlayer jj2;
	AudioStreamPlayer jj3;

	AudioStreamPlayer hh1;
	AudioStreamPlayer hh2;
	AudioStreamPlayer hh3;

	int buffer_y = 0;

	AnimatedSprite animations;

	float currentWallNoSlideTime = 0.0f;
	public bool isAnchored = false;
	bool onWall = false;
	bool onFloor = false;
	bool wallJumpReady = true;

	public float airXAccel;

	public Vector2 velocity;
	
	double maxJumpVelocity;
	double minJumpVelocity;
	
	double maxJumpHeight = 5 * Globals.UNIT_SIZE;
	double minJumpHeight = 0.8 * Globals.UNIT_SIZE;
	double jumpDuration =  0.5;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		jj1 = (AudioStreamPlayer)GetNode(jump1);
		jj2 = (AudioStreamPlayer)GetNode(jump2);
		jj3 = (AudioStreamPlayer)GetNode(jump3);

		hh1 = (AudioStreamPlayer)GetNode(fall1);
		hh2 = (AudioStreamPlayer)GetNode(fall2);
		hh3 = (AudioStreamPlayer)GetNode(fall3);

		animations = (AnimatedSprite)GetNode(animationsPath);
		airXAccel = 0;
	}
	
//	public override void _ApplyMovement (InputEvent @event)
//	{
//		for (i = 0; i < getSlideCOunt(); i++)
//		{
//			var collision = getSlideCollision(i);
//			if (collision.collider.HasMethod("collideWith"))
//			{
//				collision.collider.collideWith(collision,self);
//			}
//		}
//	}
	
	public override void _Input (InputEvent @event)
	{
		if(@event.IsActionReleased("player2_move_jump") && velocity.y < minJumpVelocity)
		{
			velocity.y = (float)minJumpVelocity;
		}

		if (@event.IsActionReleased("player1_move_jump") && velocity.y < minJumpVelocity)
		{
			velocity.y = (float)minJumpVelocity;
		}
	}
	

	public override void _PhysicsProcess(float delta)
	{
	 

		airXAccel = 0.0f;

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
			velocity.y += gravity * delta;
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
			currentWallJumpCoolDown = 0;
			wallJumpReady = true;
		}

		if(currentWallJumpCoolDown < wallJumpCoolDown)
		{
			currentWallJumpCoolDown += delta;
		}
		else
		{
			wallJumpReady = true;
			currentWallJumpCoolDown = 0.0f;
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
				else
				{
					airXAccel = walkSpeed * airDashMod * delta;
					if (velocity.x > -maxXVel)
					{
						velocity.x -= airXAccel;
					}
					//velocity.x = Math.Max(-maxXVel, velocity.x - airXAccel);
				}
				
			}
			else if (Input.IsActionPressed("player1_move_right"))
			{
				if (onFloor)
				{
					velocity.x = walkSpeed;
				}
				else
				{
					airXAccel = walkSpeed * airDashMod * delta;
					if (velocity.x < maxXVel)
					{
						velocity.x += airXAccel;
					}
					// velocity.x = Math.Min(maxXVel, velocity.x + airXAccel);
				}
			}
			else
			{
				if (onFloor)
				{
					velocity.x = 0;
				}
			}

			if (Input.IsActionJustPressed("player1_move_jump"))
			{

				if (onFloor)
				{
					make_jump_sound();
					velocity.y = -jumpStrength;
				}
				if (onWall && wallJumpReady)
				{
					velocity.y = -jumpStrength;
					wallJumpReady = false;

					if (GetSlideCount() > 0 )
					{
						KinematicCollision2D col = GetSlideCollision(0);
						velocity.x = col.Normal.x * jumpStrength *0.5f ;
						// true;
					}

				}
			}
			if (velocity.x < 0)
			{
				animations.FlipH = false;
			}
			else if (velocity.x > 0)
			{
				animations.FlipH = true;
			}

			//animation stuff
			if (onFloor && velocity.x != 0){
			
			//Restarts the animation
			animations.Play("run");
				
		}else if(!onFloor && velocity.y >= walkSpeed) {
			animations.Play("fall");
		}else if(!onFloor) {
			animations.Play("swing");
		}else{
			animations.Play("idle");
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
				else
				{
					airXAccel = walkSpeed * airDashMod * delta;
					if (velocity.x > -maxXVel)
					{
						velocity.x -= airXAccel;
					}
					// velocity.x = Math.Max(-maxXVel, velocity.x - airXAccel);
				}
			}
			else if (Input.IsActionPressed("player2_move_right"))
			{
				if (onFloor)
				{
					velocity.x = walkSpeed;
				}
				else
				{
					airXAccel = walkSpeed * airDashMod * delta;
					if (velocity.x < maxXVel)
					{
						velocity.x += airXAccel;
					}
					//velocity.x = Math.Min(maxXVel, velocity.x + airXAccel);
				}
			}
			else
			{
				if (onFloor)
				{
					velocity.x = 0;
				}
			}
			if (Input.IsActionJustPressed("player2_move_jump"))
			{
				
				if (onFloor)
				{
					make_jump_sound();
					velocity.y = -jumpStrength;
				}
				if (onWall && wallJumpReady)
				{
					velocity.y = -jumpStrength;
					wallJumpReady = false;

					if (GetSlideCount() > 0)
					{
						KinematicCollision2D col = GetSlideCollision(0);
						velocity.x = col.Normal.x * jumpStrength * 0.5f;
						// true;
					}
				}
			}

			if (velocity.x < 0)
			{
				animations.FlipH = false;
			}
			else if (velocity.x > 0)
			{
				animations.FlipH = true;
			}
			if (onFloor && velocity.x != 0){
			
			animations.Play("run2");
		}else if(!onFloor && velocity.y >= walkSpeed) {
			animations.Play("fall2");
		}else if(!onFloor) {
			animations.Play("swing2");
		}else{
			animations.Play("idle2");
		}
		
		}
	

	}

	public void make_fall_sound()
	{
		Random rnd = new Random();
		int x = rnd.Next(1, 10);

		switch (x)
		{
			case 1:
				hh1.Play();
				break;
			case 2:
				hh2.Play();
				break;
			case 3:
				hh3.Play();
				break;
			default:
				break;

		}
	}
	public void make_jump_sound()
	{
		Random rnd = new Random();
		int x = rnd.Next(1,5);

		switch (x)
		{
			case 1:
				jj1.Play();
				break;
			case 2:
				jj2.Play();
				break;
			case 3:
				jj3.Play();
				break;
			default:
				break;

		}
	}

	public void move()
	{
		MoveAndSlide(velocity, new Vector2(0, -1));

		onFloor = IsOnFloor();
		onWall = (IsOnWall() && wallJumpReady);
	}

	public bool hitNonWall()
	{

		if (GetSlideCount() > 0 && !onFloor)
		{
			KinematicCollision2D col = GetSlideCollision(0);
			return col.Normal.y > 0;
			// true;
		}
		return false;
	}
	public void applyForce(Vector2 force)
	{
		velocity += force;
	}
}
