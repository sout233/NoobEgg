using System;
using Godot;
using NoobEgg.Classes;

namespace NoobEgg.Scenes.GamePlay;

public partial class OptionButton : Button
{
    public Action Action { get; set; }

    public string ActionName
    {
        get => _actionName;
        set
        {
            _actionName = value;
            Text = _actionName;
        }
    }

    private string _actionName;

    public int Cost { get; set; }

    public override void _Ready()
    {
        Pressed += DoAction;
    }

    private void DoAction()
    {
        if (SceneNodes.CurrentPlayer.Money < Cost) return;

        GD.Print("调用Action：" + ActionName);
        Action?.Invoke();
        SceneNodes.CurrentPlayer.Money -= Cost;

        foreach (var node in GetParent().GetChildren())
        {
            if (node is OptionButton optionButton)
            {
                optionButton.Disabled = true;
                GetTree().CurrentScene.GetNode<GameManager>("GameManager").OptionJumpedValue = 10;
            }
        }
    }
}