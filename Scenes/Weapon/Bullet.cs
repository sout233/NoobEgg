using Godot;
using NoobEgg.Gaming;

namespace NoobEgg.Scenes.Weapon;

public partial class Bullet : Area2D
{
    [Export] public AudioStreamPlayer2D BulletSoundPlayer;

    public float Speed { get; set; } = 2000;

    public Attack Attack { get; set; }

    public Vector2 AreaDirection { get; set; } = new Vector2(0, 0);


    public override void _Ready()
    {
        GetNode<CpuParticles2D>("BulletParticles").Gravity = AreaDirection;
        BulletSoundPlayer.Play();
    }

    public override void _Process(double delta)
    {
        Translate(AreaDirection * (float)(Speed * delta));
    }

    public void OnAreaEnterd(Area2D area)
    {
        if (area is NoobEgg.Scenes.Character.Enemy.HitBox)
        {
            var hitbox = area as NoobEgg.Scenes.Character.Enemy.HitBox;
            var enemy = hitbox.GetParent() as NoobEgg.Scenes.Character.Enemy.Enemy;

            Attack.StartDirection = (GetGlobalMousePosition() - GlobalPosition).Normalized();

            enemy.Attacked(Attack);

            QueueFree();
        }
    }

    public void OnTimerTimeout()
    {
        QueueFree();
    }

    public void OnAreaEnterd(Node2D body)
    {
        QueueFree();
    }
}