using Godot;
using System;

public partial class WeaponGun : Weapon
{
    [Export] public int TotalAmmor = 100;
    [Export] public int MaxAmmor = 10;
    private int _magAmmor;

    public override void _Ready()
    {
        _magAmmor = MaxAmmor;
        TotalAmmor -= MaxAmmor;
    }
}
