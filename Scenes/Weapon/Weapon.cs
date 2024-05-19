using Godot;
using NoobEgg.Classes.Gaming;
using NoobEgg.Gaming;

namespace NoobEgg.Scenes.Weapon;

public partial class Weapon : Node2D
{
    protected readonly Attack Attack = new();

    [Export] public float Damage = 10;

    [Export] public float KnockBackForce = 100;

    [Export] public AudioStreamPlayer2D Audio;

    [Export] public PackedScene Bullet;

    protected Character.Player.Player Player;

    public override void _EnterTree()
    {
        Attack.Damage = Damage;
        Attack.KnockBackForce = KnockBackForce;
        Player = GetParent().GetParent<Character.Player.Player>();
    }
}