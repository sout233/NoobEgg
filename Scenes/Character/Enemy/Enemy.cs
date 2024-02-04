using Godot;
using NoobEgg.Classes;
using NoobEgg.GameProps;
using NoobEgg.Gaming;

public partial class Enemy : Character
{
    [Export]
    public PackedScene AttackedParticles;

    [Export]
    public AudioStreamPlayer2D AttackedSoundPlayer;

    [Export]
    public AnimatedSprite2D AnimatedSprite;

    private Vector2 _knockback = Vector2.Zero;

    // FUNC DOWN

    public override void _EnterTree()
    {
        Health = MaxHealth;
    }

    public override void _PhysicsProcess(double delta)
    {
        _knockback = NoobHelper.LerpV2(_knockback, Vector2.Zero, 0.1f);
        Velocity = (SceneNodes.CurrentPlayer.Position - Position).Normalized() * Speed + _knockback - Vector2.One * Weight;
        MoveAndSlide();
        AnimatedSprite.FlipH = Velocity.X < 0;
    }

    public void Attacked(Attack attack)
    {
        _knockback = Vector2.Zero;
        var attackedParticles = AttackedParticles.Instantiate<CpuParticles2D>();
        attackedParticles.Gravity = attack.StartDirection * 100;
        attackedParticles.Emitting = true;
        attackedParticles.Position = Position;
        AddSibling(attackedParticles);

        AttackedSoundPlayer.Play();

        Health -= attack.Damage;

        _knockback = attack.StartDirection * attack.KnockBackForce;

        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GD.Print("die");
        AttackedSoundPlayer.Reparent(GetParent().GetParent());

        QueueFree();
    }
}