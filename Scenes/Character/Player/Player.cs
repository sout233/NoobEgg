using Godot;
using NoobEgg.Classes;
using NoobEgg.Classes.Gaming;
using NoobEgg.Gaming;
using NoobEgg.Toolkit;
using System.Security.AccessControl;

public partial class Player : Character
{
    [Export] public Camera2D Camera;
    [Export] public Node2D CameraAnchor;
    [Export] public Node2D WeaponStack;
    [Export] public AnimatedSprite2D FighterBody;
    [Export] public Sprite2D FighterLeftHand;
    [Export] public Sprite2D FighterRightHand;
    [Export] public PackedScene Bullet;
    [Export] public PackedScene AttackedParticles;
    [Export] public AudioStreamPlayer2D AttackedSoundPlayer;

    public Weapon Wp01;

    private int _ammor;
    private Vector2 _knockback = Vector2.Zero;


    public int Ammor
    {
        get { return NoobAntiCheat.DeValue(_ammor); }
        set => _ammor = value > 0 ? NoobAntiCheat.EnValue(value) : 0;
    }

    public override void _EnterTree()
    {
        UIController.AmmorLabel.Text = Ammor.ToString();
        UIController.HealthBar.Value = Health;
    }


    #region 常规角色方法

    protected void Move()
    {
        Vector2 velocity = Velocity;
        _knockback = NoobHelper.LerpV2(_knockback, Vector2.Zero, 0.1f);

        float dirX = Input.GetAxis("Left", "Right");
        float dirY = Input.GetAxis("Up", "Down");

        velocity.X = dirX * Speed;
        velocity.Y = dirY * Speed;

        Velocity = velocity + _knockback;
        MoveAndSlide();
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
        UIController.HealthBar.Value = Health;

        _knockback = attack.StartDirection * attack.KnockBackForce;

        if (Health <= 0)
        {
            //Die();
        }
    }

    protected void PlayAnimation()
    {
        if (Velocity - _knockback != Vector2.Zero)
        {
            FighterBody.Play("Idle");
        }
        else
        {
            FighterBody.Stop();
        }
    }

    protected void CameraFollow()
    {
        CameraAnchor.LookAt(GetGlobalMousePosition());
    }

    protected void Flip()
    {
        if (GetGlobalMousePosition().X < Position.X)
        {
            FighterBody.FlipH = true;
            // 构式写法
            Wp01.Scale = Wp01.Scale with { Y = -1 };
            WeaponStack.Position = WeaponStack.Position with { X = -6 };
        }
        else
        {
            FighterBody.FlipH = false;
            Wp01.Scale = Wp01.Scale with { Y = 1 };
            WeaponStack.Position = WeaponStack.Position with { X = 6 };
        }
    }

    protected void HandFollow()
    {
        FighterLeftHand.GlobalPosition = Wp01.GetNode<Marker2D>("LeftHandMarker").GlobalPosition;
        FighterRightHand.GlobalPosition = Wp01.GetNode<Marker2D>("RightHandMarker").GlobalPosition;
    }

    protected void WeaponFollow()
    {
        Wp01.LookAt(GetGlobalMousePosition());
    }

    #endregion 常规角色方法
}