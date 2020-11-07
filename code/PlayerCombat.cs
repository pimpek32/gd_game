using Godot;
using System;

public class PlayerCombat : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	private PlayerMovement parentObj;
	private Vector3 dir;
	private RayCast hit;

	[Export]
	public float CoolDown;
	private float cdown;
	public override void _Ready()
	{
		parentObj = GetParent<PlayerMovement>();
		GD.Print(parentObj);
		dir = parentObj._vel;
		hit = GetParent().GetNode<RayCast>("mesh/hit");
		cdown = CoolDown;
		
	}

		public override void _Process(float delta)
	{
		if(Input.IsActionJustReleased("combat_melee") && CoolDown < 0)
		{
		if(hit.IsColliding())
		GD.Print(hit.GetCollider() + " in position: " + hit.GetCollisionPoint() );
		CoolDown = cdown;
		}
		CoolDown-=delta;
	}
	public override void _PhysicsProcess(float delta)
	{
		//dir = parentObj._vel;
		//var spaceState = GetWorld().DirectSpaceState;
		//var result = spaceState.IntersectRay(Transform.origin, dir);
		
		
	}
}
