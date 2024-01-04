using System;
using System.Numerics;
using Renderer;

namespace Geostorm 
{
    class EnemySprint : EntityEnemy
    {
        Vector2 acceleration;
        public EnemySprint(Vector2 ScreenSize)
        {
            spawnTime = 1f;
            speed = 0.2f;
            Random rnd = new Random();
            pos.X = rnd.Next((int)(ScreenSize.X * 0.1f + 15), (int)(ScreenSize.X * 0.9f - 15));
            pos.Y = rnd.Next((int)(ScreenSize.Y * 0.1f + 15), (int)(ScreenSize.Y * 0.9f - 15));
            collisionRadius = 20 * 0.8f;
            acceleration = new Vector2(0, 0);
        }

        override protected void DoUpdate(GameInputs inputs, GameData data)
        {
            Vector2 direction = data.Player.pos - pos;
            direction = direction / direction.Length();

            Vector2 newAcceleration = acceleration + direction * speed;
            if (newAcceleration.Length() < direction.Length() * 10)
                acceleration += direction * speed;

            pos = pos + acceleration;


            if (pos.X < inputs.ScreenSize.X * 0.1 + 15 || pos.X > inputs.ScreenSize.X * 0.9 - 15)
            {
                acceleration.X = -acceleration.X;
            }
            if (pos.Y < inputs.ScreenSize.Y * 0.1 + 15 || pos.Y >  inputs.ScreenSize.Y * 0.9 - 15)
            {
                acceleration.Y = -acceleration.Y;
            }


        }
        override public void Draw(Graphics graphics)
        {
            graphics.DrawSprint(pos, rotation,  1 - spawnTime);
        }
    }
}
