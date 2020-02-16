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


		doRopePhysics(p1, p2, delta);
		doRopePhysics(p2, p1, delta);

		p1.move();
		p2.move();


	}

	void doRopePhysics(playerMovment p1,playerMovment p2,float delta)
	{

		if (ropeLength <= p1.Position.DistanceTo(p2.Position))
		{
			Vector2 ropePullVector = (p2.Position - p1.Position).Normalized();
			// tug on other player
			if (ropePullVector.Dot(p1.velocity) < 0)
			{
				if (p2.isAnchored)
				{
					//otherPlayer.MoveAndSlide(velocity, new Vector2(0, -1));
					//velocity *= 0.9f;
				}
				else
				{
					p2.MoveAndSlide(p1.velocity * 0.5f, new Vector2(0, -1));
					p1.velocity *= 0.5f;
				}
			}

			if (ropeLength <= p1.Position.DistanceTo(p2.Position))
			{
				float strafeInfluence;
				if ((-ropePullVector).Angle() > Math.PI)
				{
					strafeInfluence = 0;
				}
				else if (p1.airXAccel < 0)
				{
					strafeInfluence = p1.airXAccel * (float)(Math.Cos((-ropePullVector).Angle()) + 1);
				}
				else
				{
					strafeInfluence = p1.airXAccel * (-1) * (float)(Math.Cos((-ropePullVector).Angle()) - 1);
				}
				Vector2 parallelPart = (p1.velocity.Dot(ropePullVector) / ropePullVector.Dot(ropePullVector)) * ropePullVector;
				if (parallelPart.Dot(ropePullVector) < 0)
				{
					p1.velocity -= parallelPart;
				}

				p1.velocity.x += strafeInfluence;
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
