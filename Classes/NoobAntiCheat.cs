using Godot;
using System;

namespace NoobEgg.Toolkit
{
    public static class NoobAntiCheat
    {
        private static int antiKey = 114;
        private static Random random;

        public static int UpdateAntiKey()
        {
            antiKey = random.Next(114);
            GD.Print($"设置加密密钥：{antiKey}");
            return antiKey;
        }

        public static float EnValue(float value)
        {
            float _newValue = -value * antiKey;
            GD.Print($"已对变量加密：{_newValue}, 源值：{value}");
            return _newValue;
        }

        public static float DeValue(float value)
        {
            float _newValue = -value / antiKey;
            return _newValue;
        }
    }
}