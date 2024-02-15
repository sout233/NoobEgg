using Godot;

namespace NoobEgg.Scenes.Character;

public partial class KilledParticles : CpuParticles2D
{
    public void OnTimerTimeout()
    {
        QueueFree();
    }
}