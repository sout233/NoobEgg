using Godot;
using NoobEgg.Scenes.Weapon;

namespace NoobEgg.Scenes.Character.Player;

public partial class Noob : Player
{
    private Weapon.Weapon _weapon;

    public override void _Ready()
    {
        Camera.Position = new Vector2(40, 0);

        Wp01 = WeaponStack.GetNode<Weapon.Weapon>("WP01");
        _weapon = Wp01;

        Speed = 700f;
        Ammo = 30;
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