using System;
using Godot;
using NoobEgg.Classes;
using NoobEgg.Classes.Configs;
using NoobEgg.Classes.Gaming;

namespace NoobEgg.Scenes.GamePlay;

public partial class GameManager : Node
{
    [Export]
    public Timer SpawnTimer;

    [Export]
    public Timer DayTimer;

    [Export]
    public PackedScene Spawner;

    [Export]
    public ProgressBar HealthBar;

    [Export]
    public AnimationPlayer DamageScreenAnimationPlayer;

    [Export]
    public Label AmmorLabel;

    private int _day = 1;

    public override void _Ready()
    {
        UiController.HealthBar = HealthBar;
        UiController.DamageScreenAnimationPlayer = DamageScreenAnimationPlayer;
        UiController.AmmoLabel = AmmorLabel;

        SceneNodes.CurrentPlayer = GetCurrentPlayer();
        SceneNodes.CurrentTileMap = GetParent().GetNode<TileMap>("TileMap");

        SpawnTimer.OneShot = true;
        SpawnTimer.Start();
    }

    public Character.Player.Player GetCurrentPlayer()
    {
        Godot.Collections.Array<Node> children = GetTree().CurrentScene.GetChildren();
        Character.Player.Player player = null;

        foreach (Node child in children)
        {
            if (child is Character.Player.Player)
            {
                player = (Character.Player.Player)child;
            }
        }
        return player;
    }

    public void EnemyGenerate()
    {
        Random random = new Random();
        var spawner = Spawner.Instantiate<Items.Spawner>();

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

    public void TimeController()
    {
        DayTimer.WaitTime = GameTimeConfig.GetDayTime(_day);
        GD.Print($"第{_day}天, Timer:{GameTimeConfig.GetDayTime(_day)}");
        _day++;
    }
}