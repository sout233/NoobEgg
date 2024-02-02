using Godot;
using System;

public partial class Noob : Player
{
    [Export] public Camera2D Camera;
    [Export] public Node2D CameraAnchor;
    [Export] public Node2D WeaponStack;
    [Export] public AnimatedSprite2D FighterBody;
    [Export] public Sprite2D FighterLeftHand;
    [Export] public Sprite2D FighterRightHand;
    [Export] public PackedScene Bullet;

    public Weapon Wp01;

    private double _shootTimer = 0;
    private double _shootRate = 0.1f;

    public override void _Ready()
    {
        Camera.Position = new Vector2(40, 0);

        Wp01 = WeaponStack.GetNode<Weapon>("WP01");

        Speed = 700f;
    }

    public override void _PhysicsProcess(double delta)
    {
        Move();
        PlayAnimation();
        CameraFollow();
        Flip();
        HandFollow();
        WeaponFollow();

        Shoot(delta);
    }

    void Move()
    {
        Vector2 velocity = Velocity;

        float dirX = Input.GetAxis("Left", "Right");
        float dirY = Input.GetAxis("Up", "Down");

        velocity.X = dirX * Speed;
        velocity.Y = dirY * Speed;

        Velocity = velocity;
        MoveAndSlide();
    }

    void PlayAnimation()
    {
        if (Velocity != Vector2.Zero)
        {
            FighterBody.Play("Idle");
        }
        else
        {
            FighterBody.Stop();
        }
    }

    void CameraFollow()
    {
        CameraAnchor.LookAt(GetGlobalMousePosition());
    }

    void Flip()
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

    void HandFollow()
    {
        FighterLeftHand.GlobalPosition = Wp01.GetNode<Marker2D>("LeftHandMarker").GlobalPosition;
        FighterRightHand.GlobalPosition = Wp01.GetNode<Marker2D>("RightHandMarker").GlobalPosition;
    }

    void WeaponFollow()
    {
        Wp01.LookAt(GetGlobalMousePosition());
    }

    void Shoot(double delta)
    {
        _shootTimer += delta;

        if (Input.GetActionRawStrength("Shoot") > 0 && _shootTimer >= _shootRate)
        {
            var _bullet = Bullet.Instantiate<Area2D>() as Bullet;

            _bullet.GlobalPosition = Wp01.GetNode<Marker2D>("MuzzleMarker").GlobalPosition;
            _bullet.AreaDirection = (GetGlobalMousePosition() - this.GlobalPosition).Normalized();
            _bullet.Attack = Wp01.Attack;

            AddSibling(_bullet);

            _shootTimer = 0;
        }
        else
        {
            Camera.Set("offset", new Vector2(0, 0));
        }
    }
}
