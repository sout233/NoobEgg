using System;
using System.Threading.Tasks;
using Godot;
using NoobEgg.Archive.Scenes.UI;
using NoobEgg.Classes;
using NoobEgg.Classes.Configs;
using NoobEgg.Classes.Gaming;
using NoobEgg.Scenes.Character.Enemy;
using NoobEgg.Scenes.Character.Player;

namespace NoobEgg.Scenes.GamePlay;

public partial class GameManager : Node
{
    [Export] public Timer SpawnTimer;

    [Export] public PackedScene Spawner;

    [Export] public ProgressBar HealthBar;

    [Export] public AnimationPlayer DamageScreenAnimationPlayer;

    [Export] public Label ClipAmmoLabel;

    [Export] public Label CaissonAmmoLabel;

    [Export] public ProgressBar ScoreBar;

    [Export] public Label MoneyLabel;

    [Export] public Node EnemyStack;

    [Export] public PackedScene AttackedParticles;

    [Export] public Label DayLabel;

    [Export] public Control PauseMenu;

    [Export] public Control GameOverMenu;

    [Export] public Control OptionPanel;

    [Export] public Node2D AimCursor;

    [Export] public Label ScoreLabel;

    [Export] public AudioStreamPlayer2D BgmAudioStreamPlayer;

    public int CurrentScore
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            ScoreLabel.Text = CurrentScore.ToString();
        }
    }

    private int _day = 1;

    private bool _canGenEnemy = true;

    public int OptionJumpedValue = 0;
    private int _currentScore;

    public override void _Ready()
    {
        UiController.HealthBar = HealthBar;
        UiController.DamageScreenAnimationPlayer = DamageScreenAnimationPlayer;
        UiController.ClipAmmoLabel = ClipAmmoLabel;
        UiController.ScoreBar = ScoreBar;
        UiController.MoneyLabel = MoneyLabel;
        UiController.DayLabel = DayLabel;
        UiController.CaissonAmmoLabel = CaissonAmmoLabel;
        UiController.AimCursor = AimCursor as AimCursor;

        GameStatus.CurrentScore = 0;
        GameStatus.AimScore = GameScoreConfig.GetAimScoreByDay(_day);

        ScoreBar.ValueChanged += OnValueChange;

        SceneNodes.CurrentPlayer = GetCurrentPlayer();
        SceneNodes.CurrentTileMap = GetParent().GetNode<TileMap>("TileMap");

        SpawnTimer.OneShot = true;
        SpawnTimer.Start();
    }

    public override void _Process(double delta)
    {
        if (!Input.IsActionJustPressed("ui_cancel")) return;

        if (GetTree().CurrentScene.HasNode("GameHUD/ShopMenu")) return;

        PauseGame();
    }

    private void PauseGame()
    {
        PauseMenu.Show();

        GetTree().Paused = true;
    }

    public void GameOver()
    {
        GameOverMenu.Show();

        GameOverMenu.GetNode<Label>("FinallyScoreLabel").Text = "最终成绩: " + CurrentScore;

        GetTree().Paused = true;
    }

    private void RestartGame()
    {
        GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
    }

    private void ShowOptionPanel()
    {
        OptionPanel.Show();

        // 随机选择一个ActionBundle
        var random = new Random();

        for (var i = 1; i <= 3; i++)
        {
            var optionButton = OptionPanel.GetNode<OptionButton>("OptionButton" + i);
            optionButton.Disabled = false;

            var randomIndex = random.Next(GainOptions.DefaultActions.Count);
            var randomActionBundle = GainOptions.DefaultActions[randomIndex];

            optionButton.ActionName = $"{randomActionBundle.Name}(${randomActionBundle.Cost})";
            optionButton.Action = randomActionBundle.Action;
            optionButton.Cost = randomActionBundle.Cost;
        }

        var tween = GetTree().CreateTween().BindNode(this).SetTrans(Tween.TransitionType.Elastic);
        tween.TweenProperty(OptionPanel, "position", new Vector2(700, 126), 1.0f);
    }

    private void HideOptionPanel()
    {
        var tween = GetTree().CreateTween().BindNode(this).SetTrans(Tween.TransitionType.Elastic);
        tween.TweenProperty(OptionPanel, "position", new Vector2(1200, 126), 1.0f);

        tween.TweenCallback(Callable.From(OptionPanel.Hide));
    }

    public void ContinueGame()
    {
        GetTree().Paused = false;
        PauseMenu.Hide();
    }

    public void Back2Menu()
    {
        GD.Print("Back2Menu");
        Input.MouseMode = Input.MouseModeEnum.Visible;
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile("res://Scenes/GamePlay/main_menu.tscn");
    }

    private async void OnValueChange(double value)
    {
        if (!(Math.Abs(value - ScoreBar.MaxValue) < 1)) return;
        GameStatus.CurrentScore = 0;

        GD.Print("Day Passed");
        _day++;

        _canGenEnemy = false;
        QueueFreeAllEnemy();

        ShowOptionPanel();

        for (var i = 10; i >= OptionJumpedValue; i--)
        {
            await Task.Delay(1000);
            OptionPanel.GetNode<Panel>("Panel").GetNode<Label>("RemainingLabel").Text = $"Remaining: {i}s";
            OptionPanel.GetNode<ProgressBar>("ProgressBar").Value = i * 10;
            GD.Print(i / 10 * 100);
        }

        OptionJumpedValue = 0;

        HideOptionPanel();

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
                attackedParticles.Gravity = new Vector2(1, 1);
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