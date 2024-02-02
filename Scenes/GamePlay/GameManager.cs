using Godot;
using System;
using NoobEgg.GameProps;

public partial class GameManager : Node
{
    public override void _Ready()
    {
        SceneNodes.CurrentPlayer = GetCurrentPlayer();
        SceneNodes.CurrentTileMap = GetParent().GetNode<TileMap>("TileMap");
    }

    public Player GetCurrentPlayer()
    {
        Godot.Collections.Array<Node> children = GetTree().CurrentScene.GetChildren();
        Player player = null;

        foreach (Node child in children)
        {
            if (child is Player)
            {
                player = (Player)child;
            }
        }
        return player;
    }

    public void EnemyGenerator()
    {

    }
}
