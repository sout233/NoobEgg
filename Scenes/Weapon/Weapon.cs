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

    private NoobEgg.Scenes.Character.Player.Player _player;
    private double _shootTimer = 0;
    private double _shootRate = 0.1f;

    public override void _EnterTree()
    {
        _attack.Damage = Damage;
        _attack.KnockBackForce = KnockBackForce;
        _player = GetParent().GetParent<NoobEgg.Scenes.Character.Player.Player>();
    }

    public void Shoot(double delta)
    {
        _shootTimer += delta;

        if (Input.GetActionRawStrength("Shoot") > 0 && _shootTimer >= _shootRate && _player.Ammo > 0)
        {
            var bullet = Bullet.Instantiate<Area2D>() as NoobEgg.Scenes.Weapon.Bullet;

            bullet.GlobalPosition = GetNode<Marker2D>("MuzzleMarker").GlobalPosition;
            bullet.AreaDirection = (GetGlobalMousePosition() - GlobalPosition).Normalized();
            bullet.Attack = _attack;

            _player.AddSibling(bullet);

            _player.Ammo--;
            UiController.AmmoLabel.Text = _player.Ammo.ToString();

            _shootTimer = 0;
        }
        else if (Input.GetActionRawStrength("Shoot") > 0 && _shootTimer >= _shootRate && _player.Ammo <= 0)
        {
            Audio.Play();
            _shootTimer = 0;
        }
    }
}