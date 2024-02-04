using Godot;
using NoobEgg.Toolkit;

public partial class Character : CharacterBody2D
{
    private float _health;
    private float _damage = 10;
    private float _maxHealth = 10;
    private float _speed = 300;
    private float _weight = 50;

    public float Health
    {
        get { return NoobAntiCheat.DeValue(_health); }
        set
        {
            _health = value < 0 ? 0 : value;
            _health = value > MaxHealth ? MaxHealth : value;
            _health = NoobAntiCheat.EnValue(_health);
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