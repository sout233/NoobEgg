using Godot;
using System;

public partial class Spawner : Marker2D
{
    [Export]
    public PackedScene Enemy;

    [Export]
    public AnimationPlayer AnimationPlayer;

    [Export]
    public Timer Timer;

    public float MinInterval;

    public float MaxInterval;


    public override void _Ready()
    {
        Random random = new Random();

        Timer.WaitTime = random.Next(1, 5);
        Timer.Start();
    }

    public void Spawn()
    {
        Random random = new Random();
        AnimationPlayer.Play("spawn");

        var enemy = Enemy.Instantiate<Enemy>();
        enemy.GlobalPosition = GlobalPosition + new Vector2(random.Next(-2, 2), random.Next(-2, 2));
        GetTree().CurrentScene.AddChild(enemy);

        QueueFree();
    }
}
