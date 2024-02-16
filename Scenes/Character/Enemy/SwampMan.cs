using Godot;
using NoobEgg.Gaming;

namespace NoobEgg.Scenes.Character.Enemy;

public partial class SwampMan : NoobEgg.Scenes.Character.Enemy.Enemy
{
	public void Attack(Node2D body)
	{
		if (body is global::NoobEgg.Scenes.Character.Player.Player)
		{
			var player = body as global::NoobEgg.Scenes.Character.Player.Player;
			var attack = new Attack()
			{
				Damage = Damage,
				KnockBackForce = Velocity.Length() * Weight / 10,
				StartDirection = Velocity.Normalized()
			};
			player.Attacked(attack);
		}
	}
}
