using Godot;
using NoobEgg.Gaming;

public partial class Weapon : Node2D
{
    public Attack Attack = new();

    [Export] public float Damage = 10;

    [Export] public float KnockBackForce = 100;

    public override void _EnterTree()
    {
        Attack.Damage = Damage;
        Attack.KnockBackForce = KnockBackForce;
    }
}