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
    public PackedScene AttackedParticles;

    [Export]
    public AudioStreamPlayer2D AttackedSoundPlayer;

    private Vector2 _knockback = Vector2.Zero;

    // FUNC DOWN

    public override void _EnterTree()
    {
        _health = MaxHealth;
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = (SceneNodes.CurrentPlayer.Position - Position).Normalized() * Speed + _knockback;
        MoveAndSlide();
        _knockback = NoobHelper.LerpV2(_knockback, Vector2.Zero, 0.1f);
    }

    public void Attacked(Attack attack)
    {
        var attackedParticles = AttackedParticles.Instantiate<CpuParticles2D>();
        attackedParticles.Gravity = attack.StartDirection * 100;
        attackedParticles.Emitting = true;
        attackedParticles.Position = Position;
        AddSibling(attackedParticles);

        AttackedSoundPlayer.Play();

        Health -= attack.Damage;

        _knockback = attack.StartDirection * attack.KnockBackForce;

        GD.Print(_knockback);

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
