using Godot;

namespace NoobEgg.Scenes.Items;

public partial class ShopItem : Button
{
    public Label ItemNameLabel;

    public Label PriceLabel;

    public string ItemName { get; set; }

    public int ItemPrice { get; set; }

    public string ItemIcon { get; set; }

    public override void _Ready()
    {
        Icon = GD.Load<CompressedTexture2D>(ItemIcon);
        TooltipText = ItemName;
        IconAlignment = HorizontalAlignment.Center;
        CustomMinimumSize = Vector2.One * 50;

        if (GetThemeStylebox("normal").Duplicate() is not StyleBoxFlat style) return;
        style.BgColor = new Color("d3850064");
        style.BorderColor = new Color("ffbf65");
        style.BorderWidthTop = 2;
        style.BorderWidthLeft = 2;
        style.BorderWidthRight = 2;
        style.BorderWidthBottom = 2;

        AddThemeStyleboxOverride("normal", style);

        Pressed += OnPressed;
    }

    private void OnPressed()
    {
        ItemNameLabel.Text = ItemName;
        PriceLabel.Text = "Price: " + ItemPrice;
    }
}