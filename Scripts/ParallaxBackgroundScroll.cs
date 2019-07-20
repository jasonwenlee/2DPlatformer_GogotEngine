using Godot;
using System;

public class ParallaxBackgroundScroll : ParallaxBackground
{
    private float speed = 5;
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        GD.Print(speed);
        speed ++;
        var background = (ParallaxLayer) GetNode("BackgroundScroll");
        background.SetMotionOffset(new Vector2(speed, 0));
    }
}
