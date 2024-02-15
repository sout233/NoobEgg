using System;
using Godot;

namespace NoobEgg.Classes
{
    public static class NoobAntiCheat
    {
        private static int antiKey = 114;
        private static Random random = new();

        public static int UpdateAntiKey()
        {
            antiKey = random.Next(114);
            GD.Print($"设置加密密钥：{antiKey}");
            return antiKey;
        }

        public static float EnValue(float value)
        {
            float _newValue = -value * antiKey;
            //GD.Print($"已对变量加密：{_newValue}, 源值：{value}");
            return _newValue;
        }

        public static float DeValue(float value)
        {
            float _newValue = -value / antiKey;
            //GD.Print($"DeValue：{value}=>{_newValue}");
            return _newValue;
        }

        public static int EnValue(int value)
        {
            int _newValue = -value * antiKey;
            //GD.Print($"已对变量加密：{_newValue}, 源值：{value}");
            return _newValue;
        }

        public static int DeValue(int value)
        {
            int _newValue = -value / antiKey;
            //GD.Print($"DeValue：{value}=>{_newValue}");
            return _newValue;
        }
    }
}