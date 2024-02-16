using Godot;
using NoobEgg.Classes.Gaming;
using NoobEgg.Gaming;

namespace NoobEgg.Scenes.Weapon;

public partial class Weapon : Node2D
{
    private readonly Attack _attack = new();

    [Export] public float Damage = 10;

    [Export] public float KnockBackForce = 100;

    [Export] public AudioStreamPlayer2D Audio;

    [Export] public PackedScene Bullet;

    private Character.Player.Player _player;
    private double _shootTimer;
    private double _shootRate = 0.1f;

    public override void _EnterTree()
    {
        _attack.Damage = Damage;
        _attack.KnockBackForce = KnockBackForce;
        _player = GetParent().GetParent<Character.Player.Player>();
    }

    public void Shoot(double delta)
    {
        _shootTimer += delta;

        if (Input.GetActionRawStrength("Shoot") > 0 && _shootTimer >= _shootRate && _player.Ammo > 0 && _player.Shootable)
        {
            if (Bullet.Instantiate<Area2D>() is Bullet bullet)
            {
                bullet.GlobalPosition = GetNode<Marker2D>("MuzzleMarker").GlobalPosition;
                bullet.AreaDirection = (GetGlobalMousePosition() - GlobalPosition).Normalized();
                bullet.Attack = _attack;
                bullet.Player = _player;

                _player.AddSibling(bullet);
            }

            _player.Ammo--;
            UiController.AmmoLabel.Text = _player.Ammo.ToString();

            _shootTimer = 0;
        }
        else if (Input.GetActionRawStrength("Shoot") > 0 && _shootTimer >= _shootRate && _player.Ammo <= 0 && _player.Shootable)
        {
            Audio.Play();
            _shootTimer = 0;
        }
    }
}