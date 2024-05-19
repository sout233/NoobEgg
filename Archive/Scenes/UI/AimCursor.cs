using Godot;

namespace NoobEgg.Archive.Scenes.UI;

public partial class AimCursor : Node2D
{
    public bool IsReload;
    
    public override void _Process(double delta)
    {
        if (IsReload)
        {
            Modulate = new("ffffff7f");
            Rotation += 0.1f;
        }
        else
        {
            Modulate = Colors.White;
            Rotation = 0;
        }
    }
}