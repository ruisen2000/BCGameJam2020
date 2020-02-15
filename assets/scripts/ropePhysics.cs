using Godot;
using System;

public class ropePhysics : DampedSpringJoint2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	//playerMovment p1;
	//playerMovment p2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//p1 = (playerMovment)GetNode(NodeA);
		//p2 = (playerMovment)GetNode(NodeB);
	}
	[Export]
	float tugForce = 10;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		//if (Length <= p1.Position.DistanceTo(p2.Position))
		//{
			//// Rope is taut; apply force to players
			//Vector2 relativeDirection = p1.Position - p2.Position;
			//relativeDirection = relativeDirection.Normalized();

			////p1.Position = (-relativeDirection) * p1.Position.DistanceTo(p2.Position);
			////p2.Position = (relativeDirection) * p2.Position.DistanceTo(p1.Position);

			//Vector2 p1NewVelocity = p1.velocity - ((p1.velocity.Dot(relativeDirection) / relativeDirection.Dot(relativeDirection)) * relativeDirection);
			//Vector2 p2NewVelocity = p2.velocity - ((p2.velocity.Dot(relativeDirection) / relativeDirection.Dot(relativeDirection)) * relativeDirection);

			////float p1NewY = p1.velocity.y / (float)Math.Sin((-relativeDirection).AngleTo(new Vector2(1, 0)));
			////float p1NewX = p1.velocity.x / (float)Math.Cos((-relativeDirection).AngleTo(new Vector2(1, 0)));

			////float p2NewY = p2.velocity.y / (float)Math.Sin(relativeDirection.AngleTo(new Vector2(1, 0)));
			////float p2NewX = p2.velocity.x / (float)Math.Cos(relativeDirection.AngleTo(new Vector2(1, 0)));

			////p1.velocity = new Vector2(p1NewX, p1NewY);
			////p2.velocity += new Vector2(p2NewX, p2NewY);
			//p2.velocity = p2NewVelocity;

			////Vector2 p1Force = tugForce * -relativeDirection;
			////Vector2 p2Force = tugForce * relativeDirection;
			////p1.applyForce(p1Force);
			////p2.applyForce(p2Force);
		//}
	}
	//public void calculateRope(NodePath movingPlayer)
	//{
	//	NodePath otherPlayer = null;
	//	if (movingPlayer == NodeA)
	//	{
	//		otherPlayer = NodeB;
	//	}
	//	else if (movingPlayer == NodeB)
	//	{
	//		otherPlayer = NodeA;
	//	}

	//	playerMovment moveP = (playerMovment)GetNode(movingPlayer);
	//	playerMovment otherP = (playerMovment)GetNode(otherPlayer);

	//	Vector2 ropePullVector = (otherP.Position - moveP.Position).Normalized();

	//	Vector2 newVelocity = moveP.velocity - ((moveP.velocity.Dot(ropePullVector) / ropePullVector.Dot(ropePullVector)) * ropePullVector);
		
	//	moveP.velocity = newVelocity;
	//}
}
