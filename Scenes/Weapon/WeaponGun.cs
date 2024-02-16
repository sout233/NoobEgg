using Godot;

namespace NoobEgg.Scenes.Weapon;

public partial class WeaponGun : Weapon
{
    [Export] public int TotalAmmo = 100;
    [Export] public int MaxAmmo = 10;
    
    private int _magAmmo;

    public override void _Ready()
    {
        _magAmmo = MaxAmmo;
        TotalAmmo -= MaxAmmo;
    }
}