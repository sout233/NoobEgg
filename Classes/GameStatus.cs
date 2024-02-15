using Godot;
using NoobEgg.Classes.Gaming;

namespace NoobEgg.Classes;

public partial class GameStatus:Node
{
    private static int _currentScore;
    private static int _aimScore = 100;

    public static int CurrentScore
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            UiController.ScoreBar.Value = _currentScore;
        }
    }

    public static int AimScore
    {
        get => _aimScore;
        set
        {
            _aimScore = value;
            UiController.ScoreBar.MaxValue = _aimScore;
        }
    }
}