using Godot;
using NoobEgg.Classes.Gaming;
using NoobEgg.Gaming;

public partial class Weapon : Node2D
{
    public Attack Attack = new();

    [Export] public float Damage = 10;

    [Export] public float KnockBackForce = 100;

    [Export] public AudioStreamPlayer2D Audio;

    [Export] public PackedScene Bullet;

    private Player _player;
    private double _shootTimer = 0;
    private double _shootRate = 0.1f;

    public override void _EnterTree()
    {
        Attack.Damage = Damage;
        Attack.KnockBackForce = KnockBackForce;
        _player = GetParent().GetParent<Player>();
    }

    public void Shoot(double delta)
    {
        _shootTimer += delta;

        if (Input.GetActionRawStrength("Shoot") > 0 && _shootTimer >= _shootRate && _player.Ammor > 0)
        {
            var _bullet = Bullet.Instantiate<Area2D>() as Bullet;

            _bullet.GlobalPosition = GetNode<Marker2D>("MuzzleMarker").GlobalPosition;
            _bullet.AreaDirection = (GetGlobalMousePosition() - GlobalPosition).Normalized();
            _bullet.Attack = Attack;

            _player.AddSibling(_bullet);

            _player.Ammor--;
            UIController.AmmorLabel.Text = _player.Ammor.ToString();

            _shootTimer = 0;
        }
        else if (Input.GetActionRawStrength("Shoot") > 0 && _shootTimer >= _shootRate && _player.Ammor <= 0)
        {
            Audio.Play();
            _shootTimer = 0;
        }
    }
}