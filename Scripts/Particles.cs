using Godot;
using System;
using System.Collections.Generic;

public class Particles : Node2D
{
    RandomNumberGenerator randNum = new RandomNumberGenerator();
    private float height =  600;
    private float width = 1024;
    private float gravity = 0.3f;
    private int timer;
    public List<Particle> drops = new List<Particle>();

    public override void _Ready()
    {
        SetProcess(true);
    }
    public override void _Process(float delta)
    {
        Update();
    }

    public override void _Draw()
    {
        for (int i = 0; i < 5; i++)
        {
            if (drops.Count < 1000)
            {
                Particle drop = new Particle(width * 0.5f, height);
                // CHECK
                drop.Move(randNum.Randf()*4-2, randNum.Randf()*(-2)-15);
                drops.Insert(0,drop);
            }          
        }
        for (int j = 0; j < drops.Count; j++)
        {
            // GD.Print(j);
            drops[j].Move(0, gravity);
            drops[j].Integrate();
            // DrawCircle(drops[j].Bounce(), 3, Color.ColorN("red",1));
        }
        timer += 1;
        if (drops.Count == 1000 && timer > 400)
        {
            GD.Print(drops.Count + "Clear" + timer);
            drops.Clear();
            timer = 0;
        }
    }
}

public class Particle  
{
    public float damping = 0.999f;
    float width; float height;
    
    private Vector2 oldParticle;
    private Vector2 newParticle;

    public Particle(float width, float height)
    {
        oldParticle = new Vector2(width, height);
        newParticle = oldParticle;
        this.width = width; this.height = height;
    }

    public void Integrate(){
        Vector2 velocity = Velocity();
        oldParticle = newParticle;
        // CHECK + new Vector2(damping, damping)
        newParticle += velocity;
    }

    public Vector2 Velocity(){
        return (newParticle - oldParticle);
    }

    public void Move(float offset_x, float offset_y){
        newParticle += new Vector2(offset_x, offset_y);
    }

    public Vector2 Bounce(){
        if (newParticle.y > height)
        {
            var velocity = Velocity();
            oldParticle.y = height;
            // Check
            newParticle.y = oldParticle.y - velocity.y + 0.3f;
        }
        if (newParticle.x < 0 || newParticle.x > width)
        {
            newParticle.x = 0;
        }
        return newParticle;
    }
}


