using System.Collections.Generic;
using System.Linq;
using Godot;
using NoobEgg.Scenes.Character.Player;
using NoobEgg.Scenes.Items;

namespace NoobEgg.Scenes.UI;

public partial class ShopMenu : Control
{
    [Export] public GridContainer ShopGrid;

    [Export] public Label ItemNameLabel;

    [Export] public Label PriceLabel;

    [Export] public Button BuyButton;

    public Player Player;

    private List<ShopItem> _shopItems;

    public override void _Ready()
    {
        // GetTree().Paused = true;
        Input.MouseMode = Input.MouseModeEnum.Visible;
        GetTree().CurrentScene.GetNode<Node2D>("TopLayer/AimCursor").Hide();
        Player.Shootable = false;

        _shopItems = new()
        {
            new() { ItemName = "Ammo", ItemPrice = 10, ItemIcon = "res://Archive/Resource/images/wp01.png" },
        };

        foreach (var item in _shopItems)
        {
            var button = new ShopItem();
            button.ItemName = item.ItemName;
            button.ItemIcon = item.ItemIcon;
            button.ItemPrice = item.ItemPrice;
            button.ItemNameLabel = ItemNameLabel;
            button.PriceLabel = PriceLabel;

            ShopGrid.AddChild(button);
        }

        BuyButton.Pressed += BuyItem;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            GetTree().Paused = false;
            Input.MouseMode = Input.MouseModeEnum.Hidden;
            GetTree().CurrentScene.GetNode<Node2D>("TopLayer/AimCursor").Show();
            Player.Shootable = true;

            QueueFree();
        }
    }

    private void BuyItem()
    {
        if (ItemNameLabel.Text == "SHOP") return;
        
        var name = ItemNameLabel.Text;
        var price = int.Parse(PriceLabel.Text.Replace("Price: ", ""));
        
        if (Player.Money < price)
        {
            GD.Print("money is not enough");
            return;
        }

        Player.BuyItem(name, price);
    }
}