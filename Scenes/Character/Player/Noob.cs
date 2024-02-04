using Godot;

public partial class Noob : Player
{
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
}