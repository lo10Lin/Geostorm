

using System;
using System.Numerics;

namespace Geostorm
{
    static class MyMaths
    {
        public static Vector2 RotationV2(Vector2 vec, float rota)
        {
            Vector2 res = new Vector2(0, 0);
            res.X = MathF.Cos(rota) * vec.X - MathF.Sin(rota) * vec.Y;
            res.Y = MathF.Sin(rota) * vec.X + MathF.Cos(rota) * vec.Y;
            return res;
        }


        public static float FindVectorAngle(Vector2 vec)
        {
            float angle = 0;
            if (vec.Length() != 0)
            {
                angle = MathF.Acos(vec.X / vec.Length());
                if (vec.Y < 0)
                    angle = -angle;
            }
            return angle;
        }
    }
}
