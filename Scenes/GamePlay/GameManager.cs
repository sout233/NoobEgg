using Godot;
using System;
using NoobEgg.GameProps;

public partial class GameManager : Node
{
    [Export]
    public Timer SpawnTimer;

    [Export]
    public PackedScene Spawner;

    public override void _Ready()
    {
        SceneNodes.CurrentPlayer = GetCurrentPlayer();
        SceneNodes.CurrentTileMap = GetParent().GetNode<TileMap>("TileMap");

        SpawnTimer.OneShot = true;
        SpawnTimer.Start();
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

    public void EnemyGenerate()
    {
        Random random = new Random();
        var spawner = Spawner.Instantiate<Spawner>();

        var used = SceneNodes.CurrentTileMap.GetUsedRect();
        var tileSize = SceneNodes.CurrentTileMap.TileSet.TileSize * 6;
        var tileLeftX = used.Position.X * tileSize.X + tileSize.X;
        var tileRightX = used.End.X * tileSize.X - tileSize.X;
        var tileTopY = used.Position.Y * tileSize.Y + tileSize.Y;
        var tileBottomY = used.End.Y * tileSize.Y - tileSize.Y;

        spawner.Position = new Vector2(random.Next(tileLeftX, tileRightX), random.Next(tileTopY, tileBottomY));
        GetTree().CurrentScene.AddSibling(spawner);

        SpawnTimer.WaitTime = random.Next(1, 5);
        SpawnTimer.Start();
    }
}
