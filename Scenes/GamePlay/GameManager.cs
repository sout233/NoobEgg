using System;
using Godot;
using NoobEgg.Classes;
using NoobEgg.Classes.Configs;
using NoobEgg.Classes.Gaming;

namespace NoobEgg.Scenes.GamePlay;

public partial class GameManager : Node
{
    [Export] public Timer SpawnTimer;

    [Export] public PackedScene Spawner;

    [Export] public ProgressBar HealthBar;

    [Export] public AnimationPlayer DamageScreenAnimationPlayer;

    [Export] public Label AmmoLabel;

    [Export] public ProgressBar ScoreBar;

    [Export] public Label MoneyLabel;


    private int _day = 1;


    public override void _Ready()
    {
        UiController.HealthBar = HealthBar;
        UiController.DamageScreenAnimationPlayer = DamageScreenAnimationPlayer;
        UiController.AmmoLabel = AmmoLabel;
        UiController.ScoreBar = ScoreBar;
        UiController.MoneyLabel = MoneyLabel;

        ScoreBar.ValueChanged += OnValueChange;

        SceneNodes.CurrentPlayer = GetCurrentPlayer();
        SceneNodes.CurrentTileMap = GetParent().GetNode<TileMap>("TileMap");

        SpawnTimer.OneShot = true;
        SpawnTimer.Start();

        GameStatus.AimScore = GameScoreConfig.GetAimScoreByDay(_day);
    }
    
    
    private void OnValueChange(double value)
    {
        if (!(Math.Abs(value - ScoreBar.MaxValue) < 1)) return;
        GD.Print("Day Passed");
        _day++;
        GameStatus.CurrentScore = 0;
        GameStatus.AimScore = GameScoreConfig.GetAimScoreByDay(_day);
    }


    private Character.Player.Player GetCurrentPlayer()
    {
        var children = GetTree().CurrentScene.GetChildren();
        Character.Player.Player player = null;

        foreach (var child in children)
        {
            if (child is Character.Player.Player child1)
            {
                player = child1;
            }
        }

        return player;
    }

    private void EnemyGenerate()
    {
        var random = new Random();
        var spawner = Spawner.Instantiate<Items.Spawner>();

        // var used = SceneNodes.CurrentTileMap.GetUsedRect();
        var used = new Rect2I(new(-1, -1), new(27, 22));
        var tileSize = SceneNodes.CurrentTileMap.TileSet.TileSize * 6;
        var tileLeftX = used.Position.X * tileSize.X + tileSize.X;
        var tileRightX = used.End.X * tileSize.X - tileSize.X;
        var tileTopY = used.Position.Y * tileSize.Y + tileSize.Y;
        var tileBottomY = used.End.Y * tileSize.Y - tileSize.Y;

        spawner.Position = new Vector2(random.Next(tileLeftX, tileRightX), random.Next(tileTopY, tileBottomY));
        GetTree().CurrentScene.AddSibling(spawner);

        SpawnTimer.WaitTime = random.NextDouble() * GameScoreConfig.GetWaitTimeByDay(_day);
        SpawnTimer.Start();
    }
}