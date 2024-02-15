using Godot;

namespace NoobEgg.Scenes.Weapon;

public partial class BulletParticles : CpuParticles2D
{
    public void OnTimerTimeOut()
    {
        GD.Print("Clear");
        QueueFree();
    }
}