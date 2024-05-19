using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Godot;
using NoobEgg.Classes.Gaming;
using NoobEgg.Scenes.GamePlay;

namespace NoobEgg.Classes.Configs;

public abstract class GainOptions
{
    public static readonly List<ActionBundle> DefaultActions = new()
    {
        new()
        {
            Name = "恢复10点血量", Description = "增加10点血量", Cost = 70,
            Action = () => GainOptionActions.AddPlayerHealth(10)
        },
        new()
        {
            Name = "恢复20点血量", Description = "增加20点血量", Cost = 114,
            Action = () => GainOptionActions.AddPlayerHealth(20)
        },
        new()
        {
            Name = "恢复50点血量", Description = "增加50点血量", Cost = 233,
            Action = () => GainOptionActions.AddPlayerHealth(50)
        },
        new()
        {
            Name = "增加5点血量上限", Cost = 250,
            Action = () => GainOptionActions.AddPlayerMaxHealth(5)
        },
        new()
        {
            Name = "增加10点血量上限", Cost = 500,
            Action = () => GainOptionActions.AddPlayerMaxHealth(10)
        },
        new()
        {
            Name = "增加114点血量上限", Cost = 114514,
            Action = () => GainOptionActions.AddPlayerMaxHealth(114)
        },
        new()
        {
            Name = "弹药 +100", Cost = 50,
            Action = () => GainOptionActions.AddCaissonAmmo(100)
        },
        new()
        {
            Name = "弹药 +300", Cost = 100,
            Action = () => GainOptionActions.AddCaissonAmmo(300)
        },
        new()
        {
            Name = "弹夹容量上限 +5", Cost = 50,
            Action = () => GainOptionActions.AddMaxClipSpace(5)
        },
        new()
        {
            Name = "弹夹容量上限 +10", Cost = 100,
            Action = () => GainOptionActions.AddMaxClipSpace(10)
        },
        new()
        {
            Name = "弹夹容量上限 +20", Cost = 150,
            Action = () => GainOptionActions.AddMaxClipSpace(20)
        },
        new()
        {
            Name = "弹夹容量上限 +25", Cost = 200,
            Action = () => GainOptionActions.AddMaxClipSpace(25)
        },
    };

    public class ActionBundle
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public Action Action { get; set; }
    }
}

public static class GainOptionActions
{
    public static void AddPlayerHealth(int health)
    {
        var player = SceneNodes.CurrentPlayer;
        player.Health += health;
        UiController.HealthBar.Value = player.Health;
        UiController.HealthBar.GetNode<Label>("Label").Text = $"{player.Health} / {player.MaxHealth}";
    }

    public static void AddPlayerMaxHealth(int health)
    {
        var player = SceneNodes.CurrentPlayer;
        player.MaxHealth += health;
        UiController.HealthBar.MaxValue = player.MaxHealth;
        UiController.HealthBar.GetNode<Label>("Label").Text = $"{player.Health} / {player.MaxHealth}";
    }

    public static void AddCaissonAmmo(int ammo)
    {
        var player = SceneNodes.CurrentPlayer;
        var weapon = player.Wp01;
        weapon.CaissonAmmo += ammo;
    }

    public static void AddMaxClipSpace(int space)
    {
        var player = SceneNodes.CurrentPlayer;
        var weapon = player.Wp01;
        weapon.MaxClipSpace += space;
    }
}