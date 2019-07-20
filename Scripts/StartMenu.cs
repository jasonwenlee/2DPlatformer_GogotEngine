using Godot;
using System;

public class StartMenu : Control
{
    private void StartButton()
    {
        GetTree().ChangeScene("res://Worlds/World1.tscn");
    }
    
    private void QuitButton()
    {
        GetTree().Quit();
    }
}





