using Godot;
using System;

public class GameUI : Node
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private Control canvas;
	private Spatial gameScene;
	private bool isPaused = false;

    private Button exitButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		canvas = GetTree().GetCurrentScene().GetNode<Control>("ui");
		gameScene  = GetTree().GetCurrentScene().GetNode<Spatial>("Game");
        exitButton = canvas.GetNode<Button>("container/exit");
		GD.Print(canvas);
		canvas.Visible = isPaused;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if(Input.IsActionJustReleased("pause"))
		{
			isPaused = !isPaused;
			canvas.Visible = isPaused;
			gameScene.GetTree().Paused = isPaused;
		}

        if(exitButton.Pressed)
        {
            GetTree().Quit();
        }
	}
}
