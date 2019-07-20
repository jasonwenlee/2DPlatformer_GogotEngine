using Godot;
using System;

public class Player : Godot.KinematicBody2D
{
	// Export properties to editor
	[Export] public int maxSpeed;[Export] public int acceleration;
	[Export] public int maxJumpSeed; [Export] public float minJumpPercent;
	[Export] public int gravity; [Export] public float groundFriction;
	[Export] public float airFriction;

	// Motion
	private Vector2 velocity = new Vector2();
	private bool jumping = false;

	// Normal vector pointing upwards to signify floor normal
	private Vector2 UP = new Vector2(0,-1);
	public void GetInput(){
		// Get AnimatedSprite node under player
		var charSprite = (AnimatedSprite) GetNode("Sprite");
		bool friction = false;

		// Detect up/down/left/right keystrokes and only move when pressed
		bool right = Input.IsActionPressed("ui_right");
		bool left = Input.IsActionPressed("ui_left");
		bool jump = Input.IsActionJustPressed("ui_up");
		bool jumpHold = Input.IsActionPressed("ui_up");
		// Move right
		if (right){
			velocity.x = Mathf.Min(velocity.x + acceleration, maxSpeed);
			charSprite.FlipH = false;
			if (IsOnFloor()) charSprite.Play("Run");
		}
		// Move left
		else if (left){
			velocity.x = Mathf.Max(velocity.x - acceleration, -maxSpeed);
			charSprite.FlipH = true;
			if (IsOnFloor()) charSprite.Play("Run");
		}
		// Idle
		else{
			charSprite.Play("Idle");
			friction = true;
		}

		if (IsOnFloor()){
			if (friction) velocity.x = Mathf.Lerp(velocity.x, 0, groundFriction);
			// Jump
			if (jump)
			{
				jumping = true; velocity.y = -maxJumpSeed;
			}
		} 
		else{
			if (velocity.y < 0)
			{
				charSprite.Play("Jump");
				// Check if jump button is held for high jump
				if (!jumpHold)
				{
					jumping = true; velocity.y = Mathf.Max(velocity.y, -maxJumpSeed * minJumpPercent);
				}
			}
			else charSprite.Play("Fall");
			velocity.x = Mathf.Lerp(velocity.x, 0, airFriction);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		GetInput();
		velocity.y += gravity * delta;
		if (jumping && IsOnFloor()) jumping = false;
		velocity = MoveAndSlide(velocity, UP);
	}
}
