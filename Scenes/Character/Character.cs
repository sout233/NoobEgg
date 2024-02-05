using Godot;
using NoobEgg.Classes;
using NoobEgg.Toolkit;

public partial class Character : CharacterBody2D
{
    private float _health;
    private float _damage;
    private float _maxHealth;
    private float _speed;
    private float _weight;


    public float Health
    {
        get { return NoobAntiCheat.DeValue(_health); }
        set
        {
            var _preHealth = NoobHelper.Clamp(value, 0, MaxHealth);
            _health = NoobAntiCheat.EnValue(_preHealth);
        }
    }

    [Export]
    public float Damage
    {
        get { return NoobAntiCheat.DeValue(_damage); }
        set { _damage = NoobAntiCheat.EnValue(value); }
    }

    [Export]
    public float MaxHealth
    {
        get { return NoobAntiCheat.DeValue(_maxHealth); }
        set { _maxHealth = NoobAntiCheat.EnValue(value); }
    }

    [Export]
    public float Speed
    {
        get { return NoobAntiCheat.DeValue(_speed); }
        set { _speed = NoobAntiCheat.EnValue(value); }
    }

    [Export]
    public float Weight
    {
        get { return NoobAntiCheat.DeValue(_weight); }
        set { _weight = NoobAntiCheat.EnValue(value); }
    }
}