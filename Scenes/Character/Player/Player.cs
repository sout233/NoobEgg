using Godot;
using NoobEgg.Toolkit;
using System;

public partial class Player : CharacterBody2D
{
    private float _health = 100;
    private float _maxHealth = 100;
    private float _speed = 700;

    public float Health
    {
        get { return NoobAntiCheat.DeValue(_health); }
        set { _health = NoobAntiCheat.EnValue(value); }
    }

    public float MaxHealth
    {
        get { return NoobAntiCheat.DeValue(_maxHealth); }
        set { _maxHealth = NoobAntiCheat.EnValue(value); }
    }

    public float Speed
    {
        get { return NoobAntiCheat.DeValue(_speed); }
        set { _speed = NoobAntiCheat.EnValue(value); }
    }
}