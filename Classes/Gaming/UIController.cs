using Godot;
using NoobEgg.Archive.Scenes.UI;

namespace NoobEgg.Classes.Gaming
{
    public static class UiController
    {
        public static ProgressBar HealthBar { get; set; }

        public static AnimationPlayer DamageScreenAnimationPlayer { get; set; }

        public static Label ClipAmmoLabel { get; set; }

        public static ProgressBar ScoreBar { get; set; }

        public static Label MoneyLabel { get; set; }

        public static Label DayLabel { get; set; }

        public static Label CaissonAmmoLabel { get; set; }
        
        public static AimCursor AimCursor { get; set; }
    }
}