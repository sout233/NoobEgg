using Godot;

public partial class KilledParticles : CpuParticles2D
{
    public void OnTimerTimeout()
    {
        QueueFree();
    }
}