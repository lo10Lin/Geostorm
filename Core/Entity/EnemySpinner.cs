using System;
using System.Numerics;
using Renderer;

namespace Geostorm 
{
    class EnemySpinner : EntityEnemy
    {
        float acceleration;
        int timer;
        Vector2 direction;
        public EnemySpinner(Vector2 ScreenSize)
        {
            spawnTime = 1f;
            speed = 15f;
            Random rnd = new Random();
            pos.X = rnd.Next((int)(ScreenSize.X * 0.1f + 15), (int)(ScreenSize.X * 0.9f - 15));
            pos.Y = rnd.Next((int)(ScreenSize.Y * 0.1f + 15), (int)(ScreenSize.Y * 0.9f - 15));
            collisionRadius = 20 * 0.8f;
            acceleration = 0;
            timer = 0;
        }

        override protected void DoUpdate(GameInputs inputs, GameData data)
        {
            rotation += timer / 100f;
            if (acceleration == 0)
            {
                timer++;
                if(timer>=60)
                {
                    acceleration = 1;
                    direction = data.Player.pos - pos;
                    direction = direction / direction.Length();
                }
            }
            else if (acceleration > 0 && acceleration < 1)
            {
                acceleration -= 1 / 30f;
                if (acceleration < 0)
                    acceleration = 0;
            }


            pos += direction * acceleration * speed;


            if (pos.X < inputs.ScreenSize.X * 0.1 + 15 || pos.X > inputs.ScreenSize.X * 0.9 - 15)
            {
                timer = 0;
                direction.X = -direction.X;
                acceleration -= 1 / 30f;
                pos += direction * acceleration * speed;
            }
            if (pos.Y < inputs.ScreenSize.Y * 0.1 + 15 || pos.Y >  inputs.ScreenSize.Y * 0.9 - 15)
            {
                timer = 0;
                direction.Y = -direction.Y;
                acceleration -= 1 / 30f;
                pos += direction * acceleration * speed;
            }


        }
        override public void Draw(Graphics graphics)
        {
            graphics.DrawSpinner(pos, rotation, 1 - spawnTime);
        }
    }
}
