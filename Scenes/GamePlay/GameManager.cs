using System;
using System.Threading.Tasks;
using Godot;
using NoobEgg.Classes;
using NoobEgg.Classes.Configs;
using NoobEgg.Classes.Gaming;
using NoobEgg.Scenes.Character.Enemy;

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

    [Export] public Node EnemyStack;
    
    [Export] public PackedScene AttackedParticles;


    private int _day = 1;

    private bool _canGenEnemy = true;


    public override void _Ready()
    {
        UiController.HealthBar = HealthBar;
        UiController.DamageScreenAnimationPlayer = DamageScreenAnimationPlayer;
        UiController.AmmoLabel = AmmoLabel;
        UiController.ScoreBar = ScoreBar;
        UiController.MoneyLabel = MoneyLabel;
        
        GameStatus.CurrentScore = 0;
        GameStatus.AimScore = GameScoreConfig.GetAimScoreByDay(_day);

        ScoreBar.ValueChanged += OnValueChange;

        SceneNodes.CurrentPlayer = GetCurrentPlayer();
        SceneNodes.CurrentTileMap = GetParent().GetNode<TileMap>("TileMap");

        SpawnTimer.OneShot = true;
        SpawnTimer.Start();
    }


    private async void OnValueChange(double value)
    {
        if (!(Math.Abs(value - ScoreBar.MaxValue) < 1)) return;
        GameStatus.CurrentScore = 0;
        
        GD.Print("Day Passed");
        _day++;

        _canGenEnemy = false;
        QueueFreeAllEnemy();
        
        await Task.Delay(10000);
        
        GameStatus.AimScore = GameScoreConfig.GetAimScoreByDay(_day);
        _canGenEnemy = true;
        SpawnTimer.Start();
    }

    private void QueueFreeAllEnemy()
    {
        var children = EnemyStack.GetChildren();
        foreach (var child in children)
        {
            if (child is Enemy enemy)
            {
                var attackedParticles = AttackedParticles.Instantiate<CpuParticles2D>();
                attackedParticles.Gravity = new Vector2(1,1);
                attackedParticles.Emitting = true;
                attackedParticles.Position = enemy.Position;
                
                EnemyStack.AddSibling(attackedParticles);
            }
            child.QueueFree();
        }
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
        if (!_canGenEnemy) return;
        
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
        EnemyStack.AddChild(spawner);

        SpawnTimer.WaitTime = random.NextDouble() * GameScoreConfig.GetWaitTimeByDay(_day);
        SpawnTimer.Start();

    }
}