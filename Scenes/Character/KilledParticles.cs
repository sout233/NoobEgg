using Godot;
using System;

public partial class KilledParticles : CpuParticles2D
{
    public void OnTimerTimeout()
    {
        QueueFree();
    }
}
