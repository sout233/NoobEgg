using Godot;
using NoobEgg.Gaming;
using NoobEgg.Scenes.Character.Enemy;
using NoobEgg.Scenes.Character.Player;

namespace NoobEgg.Scenes.Weapon;

public partial class Bullet : Area2D
{
    [Export] public AudioStreamPlayer2D BulletSoundPlayer;

    public float Speed { get; set; } = 2000;

    public Attack Attack { get; set; }

    public Vector2 AreaDirection { get; set; } = new Vector2(0, 0);

    public Player Player;


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
        if (area is not HitBox hitbox) return;

        if (hitbox.GetParent() is Enemy enemy)
        {
            enemy.Player = Player;

            Attack.StartDirection = (GetGlobalMousePosition() - GlobalPosition).Normalized();

            enemy.Attacked(Attack);
        }

        QueueFree();
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