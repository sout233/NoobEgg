using Godot;
using NoobEgg.Gaming;

public partial class SwampMan : Enemy
{
    public void Attack(Node2D body)
    {
        if (body is Player)
        {
            var player = body as Player;
            var attack = new Attack()
            {
                Damage = Damage,
                KnockBackForce = Velocity.Length() * Weight / 10,
                StartDirection = Velocity.Normalized()
            };
            player.Attacked(attack);
        }
    }
}