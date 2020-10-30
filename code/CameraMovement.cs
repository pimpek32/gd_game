using Godot;
using System;

public class CameraMovement : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private Transform camTransform;
	[Export]
	private float camDistance =5f;
	[Export]
	private float camSpeed =5f;
	private Transform playerTransform;
	
	private	Spatial camNode, playerNode;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		camNode = GetNode<Spatial>("Camera");
		playerNode = GetNode<Spatial>("Player");
	}
	public override void _Process(float delta)
	{
		
		camTransform = camNode.GetTransform();
		playerTransform = playerNode.GetTransform();
		camTransform.origin = camTransform.origin.LinearInterpolate(new Vector3(playerTransform.origin.x, playerTransform.origin.y + camDistance, playerTransform.origin.z), camSpeed * delta);
		camNode.SetGlobalTransform(camTransform);
	}
}
