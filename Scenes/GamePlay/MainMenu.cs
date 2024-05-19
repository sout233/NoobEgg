using System.Threading.Tasks;
using Godot;

namespace NoobEgg.Scenes.GamePlay;

public partial class MainMenu : Node2D
{
    [Export] public PackedScene GamePlayScene;
    [Export] public Label TitleLabel;
    [Export] public Button StartButton;
    
    public async void StartGame()
    {
        StartButton.Disabled = true;
        TitleLabel.Text = "Loading";
        await Task.Delay(1000);
        GetTree().ChangeSceneToPacked(GamePlayScene);
    }
}