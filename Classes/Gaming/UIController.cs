using Godot;

namespace NoobEgg.Classes.Gaming
{
    public static class UiController
    {
        public static ProgressBar HealthBar { get; set; }

        public static AnimationPlayer DamageScreenAnimationPlayer { get; set; }

        public static Label AmmoLabel { get; set; }

        public static ProgressBar ScoreBar { get; set; }

        public static Label MoneyLabel { get; set; }

        public static Label DayLabel { get; set; }
    }
}