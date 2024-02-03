using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobEgg.Classes.Configs
{
    public static class GameTimeConfig
    {
        /// <summary>
        /// 第一天的白昼时长，单位分钟
        /// </summary>
        public static int FirstDayTime { get; set; } = 2;

        /// <summary>
        /// 第一天的夜晚时长，单位分钟
        /// </summary>
        public static int FirstNightTime { get; set; } = 5;

        /// <summary>
        /// 时长递增，否则递减
        /// </summary>
        public static bool Increment { get; set; } = true;


        public static int GetDayTime(int day)
        {
            int time = FirstDayTime * day / 2;
            return time;
        }

        public static int GetNightTime(int day)
        {
            int time = FirstNightTime * day / 2;
            return time;
        }
    }
}
