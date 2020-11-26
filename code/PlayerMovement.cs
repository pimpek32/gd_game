using Godot;
using System;

public class PlayerMovement : RigidBody
{
	public Vector3 _vel = new Vector3();
	private Vector2 inputMovementVector = new Vector2();
	private Spatial mesh;
	private AnimationTree animationTree;
	private AnimationNodeStateMachinePlayback anim;

	
	[Export]
	float Acceleration = 500; 
	
	[Export]
	float rotSpeed = 500;
	
	[Export]
	float maxSpeed = 500;
	[Export]

	public float Deacceleration = 11.0f;
	private Vector3 nvel;

	public override void _Ready()
	{
		mesh = GetNode<Spatial>("mesh");
		animationTree = GetNode<AnimationTree>("mesh/pim/anim");
		anim = animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
	}

	public override void _Process(float delta)
	{
		if(Input.IsActionPressed("movement_forward") || Input.IsActionPressed("movement_backward")
		 || Input.IsActionPressed("movement_left") || Input.IsActionPressed("movement_right"))
		{
			anim.Travel("move");
		}
		else
		{
			anim.Travel("idle");
		}
		Vector3 lerpVec = new Vector3(-_vel.x + Transform.origin.x, Transform.origin.y, -_vel.z + Transform.origin.z);
		if(lerpVec.DistanceTo(Transform.origin) > 0.1f)
		mesh.LookAt(lerpVec, new Vector3(0,1,0));
		
		inputMovementVector = inputMovementVector.Normalized();
		if (Input.IsActionPressed("movement_forward"))
			inputMovementVector.y -= 1;
		if (Input.IsActionPressed("movement_backward"))
			inputMovementVector.y += 1;
		if (Input.IsActionPressed("movement_left"))
			inputMovementVector.x -= 1;
		if (Input.IsActionPressed("movement_right"))
			inputMovementVector.x += 1;
		GD.Print(LinearVelocity);
		/*mesh.LookAt(new Vector3(Mathf.Lerp(lerpVec.x, -inputMovementVector.x + Transform.origin.x, delta * rotSpeed),
		Transform.origin.y,
		Mathf.Lerp(lerpVec.z, -inputMovementVector.y + Transform.origin.z, delta * rotSpeed)), new Vector3(0,1,0));*/
	}
public override void _PhysicsProcess(float delta)
{
		
		nvel.x = inputMovementVector.x * delta * Acceleration;
		nvel.z = inputMovementVector.y * delta * Acceleration;
		_vel += nvel;
		
		_vel.x = Mathf.Clamp(_vel.x, -maxSpeed, maxSpeed);
		_vel.z = Mathf.Clamp(_vel.z, -maxSpeed, maxSpeed);
		
		_vel.z = Mathf.Clamp(_vel.z, -maxSpeed, maxSpeed);
		
		_vel.x -= _vel.x  / Deacceleration;
		_vel.z -= _vel.z  / Deacceleration;
		
		LinearVelocity = _vel;
		
		inputMovementVector = new Vector2(0,0);
}

	public void AddForwardForce(float delta, float scale)
	{
	_vel.x += inputMovementVector.x * delta*scale;
	_vel.z  += inputMovementVector.y * delta*scale;
	}
}
