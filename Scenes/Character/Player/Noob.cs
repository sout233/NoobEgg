using Godot;
using NoobEgg.Classes.Gaming;
using NoobEgg.Classes;

public partial class Noob : Player
{
    private Weapon _weapon;

    public override void _Ready()
    {
        Camera.Position = new Vector2(40, 0);

        Wp01 = WeaponStack.GetNode<Weapon>("WP01");
        _weapon = Wp01;

        Speed = 700f;
        Ammor = 30;
        Health = MaxHealth;
    }

    public override void _PhysicsProcess(double delta)
    {
        Move();
        PlayAnimation();
        CameraFollow();
        Flip();
        HandFollow();
        WeaponFollow();

        _weapon.Shoot(delta);
    }
}