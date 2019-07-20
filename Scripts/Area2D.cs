using Godot;
using System;
using System.Text;

public class Area2D : Godot.Area2D
{
    [Export] public string sceneLocation;

    public override void _PhysicsProcess(float delta)
    {
        var bodies = GetOverlappingBodies();
        foreach (var body in bodies)
        {
            if (body.GetType().Name == "Player")
            {
                GD.Print("Player found");
                GetTree().ChangeScene(sceneLocation);
            }
        }        
    }
}
