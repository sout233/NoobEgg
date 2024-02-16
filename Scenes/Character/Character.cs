using Godot;
using NoobEgg.Classes;

namespace NoobEgg.Scenes.Character;

public partial class Character : CharacterBody2D
{
	private float _health;
	private float _damage;
	private float _maxHealth;
	private float _speed;
	private float _weight;


	public float Health
	{
		get => NoobAntiCheat.DeValue(_health);
		set
		{
			var preHealth = NoobHelper.Clamp(value, 0, MaxHealth);
			_health = NoobAntiCheat.EnValue(preHealth);
		}
	}

	[Export]
	public float Damage
	{
		get => NoobAntiCheat.DeValue(_damage);
		set => _damage = NoobAntiCheat.EnValue(value);
	}

	[Export]
	public float MaxHealth
	{
		get => NoobAntiCheat.DeValue(_maxHealth);
		set => _maxHealth = NoobAntiCheat.EnValue(value);
	}

	[Export]
	public float Speed
	{
		get => NoobAntiCheat.DeValue(_speed);
		set => _speed = NoobAntiCheat.EnValue(value);
	}

	[Export]
	public float Weight
	{
		get => NoobAntiCheat.DeValue(_weight);
		set => _weight = NoobAntiCheat.EnValue(value);
	}
}
