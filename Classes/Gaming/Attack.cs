using System;
using Godot;

namespace NoobEgg.Gaming
{
    public class Attack
    {
        public float Damage { get; set; }

        public float KnockBackForce { get; set; }

        public Vector2 StartDirection { get; set; } = new();
    }
}
