using SFML.System;

namespace RaycastGame
{
    static class MyMath
    {
        public static int Clamp(int value, int min, int max) { return Math.Max(Math.Min(value, max), min); }
        public static float Clamp(float value, float min, float max) { return Math.Max(Math.Min(value, max), min); }
        public static float Length(Vector2f v) { return (float)Math.Sqrt(v.X * v.X + v.Y * v.Y); }
        public static float Length(Vector3f v) { return (float)Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z); }
        public static Vector2f Norm(Vector2f v) { return v / Length(v); }
        public static Vector3f Norm(Vector3f v) { return v / Length(v); }
        public static float Dot(Vector2f v1, Vector2f v2) { return v1.X * v2.X + v1.Y * v2.Y; }
        public static float Dot(Vector3f v1, Vector3f v2) { return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z; }
    }
}
