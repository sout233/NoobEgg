using Godot;
using System;

public partial class BulletParticles : CpuParticles2D
{
    public void OnTimerTimeOut()
    {
        GD.Print("Clear");
        QueueFree();
    }
}