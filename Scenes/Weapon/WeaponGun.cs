using System.Threading.Tasks;
using Godot;
using NoobEgg.Classes.Gaming;

namespace NoobEgg.Scenes.Weapon;

public partial class WeaponGun : Weapon
{
    [Export] public int MaxClipSpace = 10;
    [Export] public AudioStreamPlayer2D ReloadAudioPlayer;

    public int CaissonAmmo
    {
        get => _caissonAmmo;
        set
        {
            _caissonAmmo = value;
            UiController.CaissonAmmoLabel.Text = _caissonAmmo.ToString();
        }
    }

    public int ClipAmmo
    {
        get => _clipAmmo;
        set
        {
            _clipAmmo = value;
            UiController.ClipAmmoLabel.Text = _clipAmmo.ToString();
        }
    }

    private int _caissonAmmo = 100;
    private int _clipAmmo = 10;

    private double _shootTimer;
    private double _shootRate = 0.1f;

    private bool _isReloading;

    private bool _isShootable = true;

    public override void _Ready()
    {
        ClipAmmo = MaxClipSpace;
        CaissonAmmo -= ClipAmmo;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Reload"))
        {
            Reload();
        }
    }

    public void Shoot(double delta)
    {
        if (!_isShootable) return;

        _shootTimer += delta;

        if (Input.GetActionRawStrength("Shoot") > 0 && _shootTimer >= _shootRate && ClipAmmo > 0 && Player.Shootable)
        {
            if (Bullet.Instantiate<Area2D>() is Bullet bullet)
            {
                bullet.GlobalPosition = GetNode<Marker2D>("MuzzleMarker").GlobalPosition;
                bullet.AreaDirection = (GetGlobalMousePosition() - GlobalPosition).Normalized();
                bullet.Attack = Attack;
                bullet.Player = Player;

                Player.AddSibling(bullet);
            }

            // _player.Ammo--;
            ClipAmmo--;
            // UiController.ClipAmmoLabel.Text = Player.Ammo.ToString();

            _shootTimer = 0;
        }
        else if (Input.GetActionRawStrength("Shoot") > 0 && _shootTimer >= _shootRate && ClipAmmo <= 0 &&
                 Player.Shootable)
        {
            Audio.Play();
            _shootTimer = 0;
        }
    }

    private async void Reload()
    {
        if (_isReloading) return;
        
        _isReloading = true;
        _isShootable = false;
        UiController.AimCursor.IsReload = true;

        if (CaissonAmmo > 0 && CaissonAmmo >= CaissonAmmo - MaxClipSpace && ClipAmmo < MaxClipSpace)
        {
            var ammoToAdd = MaxClipSpace - ClipAmmo;
            GD.Print("reloading");
            
            ReloadAudioPlayer.Play();
            
            await Task.Delay(1500);
            ClipAmmo = MaxClipSpace;
            CaissonAmmo -= ammoToAdd;
        }

        _isReloading = false;
        _isShootable = true;
        UiController.AimCursor.IsReload = false;
    }
}