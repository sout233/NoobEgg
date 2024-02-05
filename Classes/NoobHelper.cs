using Godot;

namespace NoobEgg.Classes
{
    public static class NoobHelper
    {
        public static float LerpF(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }

        public static Vector2 LerpV2(Vector2 firstVector, Vector2 secondVector, float by)
        {
            float retX = LerpF(firstVector.X, secondVector.X, by);
            float retY = LerpF(firstVector.Y, secondVector.Y, by);
            return new Vector2(retX, retY);
        }
        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
        public static float Clamp(float value, float min, float max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
    }
}