using System;
using Godot;

namespace NoobEgg.Classes.Configs;

public static class GameScoreConfig
{
    public static int GetAimScoreByDay(int day)
    {
        var score = (int)(Math.Log(day + 1) * 114 / 5.14 + day * 5);
        GD.Print($"第{day}天, AimScore: {score}");
        return score;
    }

    public static double GetWaitTimeByDay(int day)
    {
        var time = Math.Log(20 / (day + 1.14));
        if (time < 0)
        {
            time = 0.1;
        }
        GD.Print($"wait time:{time}");
        return time;
    }
}