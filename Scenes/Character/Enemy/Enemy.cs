using Godot;
using NoobEgg.Classes;
using NoobEgg.GameProps;
using NoobEgg.Gaming;
using System;

public partial class Enemy : CharacterBody2D
{
    private float _health;
    private float _damage = 10;
    private float _maxHealth = 10;
    private float _speed = 300;
    private float _weight = 50;


    public float Health
    {
        get { return _health; }
        set
        {
            _health = value < 0 ? 0 : value;
            _health = value > MaxHealth ? MaxHealth : value;
        }
    }

    [Export]
    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    [Export]
    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [Export]
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    [Export]
    public float Weight
    {
        get { return _weight; }
        set { _weight = value; }
    }


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
        _health = MaxHealth;
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
