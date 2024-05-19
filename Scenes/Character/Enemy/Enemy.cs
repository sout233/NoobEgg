using Godot;
using NoobEgg.Classes;
using NoobEgg.Gaming;
using NoobEgg.Scenes.GamePlay;

namespace NoobEgg.Scenes.Character.Enemy;

public partial class Enemy : global::NoobEgg.Scenes.Character.Character
{
    [Export]
    public PackedScene AttackedParticles;

    [Export]
    public AudioStreamPlayer2D AttackedSoundPlayer;

    [Export]
    public AnimatedSprite2D AnimatedSprite;

    public Player.Player Player;

    private Vector2 _knockback = Vector2.Zero;

    // FUNC DOWN
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
        GetParent().AddSibling(attackedParticles);

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
        AttackedSoundPlayer.Reparent(GetParent().GetParent());
        GameStatus.CurrentScore += 2;
        Player.Money += 15;
        GetTree().CurrentScene.GetNode<GameManager>("GameManager").CurrentScore += 114;
        
        QueueFree();
    }
}