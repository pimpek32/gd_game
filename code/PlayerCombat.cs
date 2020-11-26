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
	[Export]
	public float punchForce;
	private float cdown;
	private bool rand = true;
	
	private AnimationTree animationTree;
	private AnimationPlayer animator;
	private AnimationNodeStateMachinePlayback anim;
	public override void _Ready()
	{
		parentObj = GetParent<PlayerMovement>();
		GD.Print(parentObj);
		dir = parentObj._vel;
		hit = GetParent().GetNode<RayCast>("mesh/hit");
		animationTree = GetParent().GetNode<AnimationTree>("mesh/pim/anim");
		animator = GetParent().GetNode<AnimationPlayer>("mesh/pim/animator");
		anim = animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
		cdown = CoolDown;
		
	}

		public override void _Process(float delta)
	{
		if(Input.IsActionJustReleased("combat_melee") && CoolDown < 0)
		{
			
		if(hit.IsColliding())
		{

		GD.Print(hit.GetCollider() + " in position: " + hit.GetCollisionPoint() );
		}
		CoolDown = cdown;
		rand =! rand;

			if(rand)
				anim.Start("Punch");
			else
				anim.Start("Punch2");
		}
		CoolDown-=delta;
	}
	public override void _PhysicsProcess(float delta)
	{
		if(Input.IsActionJustReleased("combat_melee") && CoolDown < 0)
		parentObj.AddForwardForce(delta, punchForce);
		
		
	}
}
