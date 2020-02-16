using Godot;
using System;

public class ropePhysics : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	[Export]
	NodePath player1;
	
	[Export]
	NodePath player2;

	[Export]
	float ropeLength;

	[Export]
	float ropeMaxLength = 600;

	[Export]
	float ropeMinLength = 50;

	[Export]
	float ropePullSpeed = 10.0f;

	[Export]
	float ropePullForce = 10.0f;

	playerMovment p1;
	playerMovment p2;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		p1 = (playerMovment)(KinematicBody2D)GetNode(player1);
		p2 = (playerMovment)(KinematicBody2D)GetNode(player2);
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		if (Input.IsActionPressed("player1_move_up") || Input.IsActionPressed("player2_move_up"))
		{
			extendRope(delta);
		//	GD.Print("Rope length now : " + ropeLength);
		}

		if (Input.IsActionPressed("player1_move_down") || Input.IsActionPressed("player2_move_down"))
		{
			shrinkRope(delta);
		//	GD.Print("Rope length now : " + ropeLength);
		}


		doRopePhysics(p1, p2, delta);
		doRopePhysics(p2, p1, delta);

		//Only pull them together if they are farther apart then rope is allowed
		if(ropeLength < p2.Position.DistanceTo(p1.Position))
		{
			Vector2 ropePullVector = (p2.Position - p1.Position).Normalized();

			if (!p1.isAnchored && !p2.isAnchored)
			{ //Both moving or in air, make it damped
				p1.velocity += ropePullVector * ropePullForce * 0.1f;
				p2.velocity -= ropePullVector * ropePullForce * 0.1f;
			}
			else
			{//One is anchored pull full force
				p1.velocity += ropePullVector * ropePullForce;
				p2.velocity -= ropePullVector * ropePullForce;
			}
		}
		

		p1.move();
		p2.move();


	}

	void shrinkRope(float delta)
	{
	
		if(ropeLength <= ropeMinLength)
		{
			ropeLength = ropeMinLength;
			return;
		}

		ropeLength -= delta* ropePullSpeed;
	

		
	}

	void extendRope(float delta)
	{
		if (ropeLength >= ropeMaxLength)
		{
			ropeLength = ropeMaxLength;
			return;
		}

		ropeLength += delta* ropePullSpeed;

	}

	void doRopePhysics(playerMovment p1,playerMovment p2,float delta)
	{
		if (ropeLength <= p1.Position.DistanceTo(p2.Position))
		{
			Vector2 ropePullVector = (p2.Position - p1.Position).Normalized();
			// tug on other player
			if (ropePullVector.Dot(p1.velocity) < 0)
			{
				if (p1.isAnchored && !p2.isAnchored)
				{
					p2.velocity += p1.velocity * 0.5f;
					// p2.MoveAndSlide(p1.velocity * 0.5f, new Vector2(0, -1));
					p1.velocity *= 0.5f;
				}
			}

			if (ropeLength <= p1.Position.DistanceTo(p2.Position))
			{
				Vector2 parallelPart = (p1.velocity.Dot(ropePullVector) / ropePullVector.Dot(ropePullVector)) * ropePullVector;
				if (parallelPart.Dot(ropePullVector) < 0)
				{
					p1.velocity -= parallelPart;
				}
			}
		}



		if (p1.hitNonWall())
		{
			p1.velocity = new Vector2(0, 0);
			p1.velocity.y += delta * p1.gravity;
			p1.velocity.y = Math.Min(p1.velocity.y, p1.maxYVel);
		}
	}

}
