using System;
using System.Collections.Generic;

using System.Numerics;
using Raylib_cs;
using Renderer;

using static Raylib_cs.Raylib;

namespace Geostorm
{
    
    class EntityPlayer : Entity
    {
        public int life;
        public float speed;
        public Vector2 dir;
        public float acceleration;

        public EntityPlayer()
        {
            life = 5;
            collisionRadius = 20 * 0.7f;
            speed = 5;
            acceleration = 0;
            dir = new Vector2(1, 0);
        }

        Weapon weapon = new Weapon();
        override public void Update(in GameInputs inputs, GameData data, IList<GameEvent> events)
        {
            if (inputs.TurnLeft)
            {
                rotation -= MathF.PI/20;
            }

            if (inputs.TurnRight)
            {
                rotation += MathF.PI/20;
            }
            
            dir.X = MathF.Cos(rotation);
            dir.Y = MathF.Sin(rotation);

            if (inputs.MoveForward)
            {

                if (acceleration < 1)
                    acceleration += 0.1f;
            }
            else if(acceleration!=0)
            {
                acceleration -= 0.1f;
                if (acceleration < 0)
                    acceleration = 0;
            }

            float newPosX = pos.X + dir.X * speed * acceleration;
            float newPosY = pos.Y + dir.Y * speed * acceleration;



            if (newPosX > inputs.ScreenSize.X * 0.1 + 15 && newPosX < inputs.ScreenSize.X * 0.9 - 15)
            {
                pos.X = newPosX;
            }
            if (newPosY > inputs.ScreenSize.Y * 0.1 + 15 && newPosY < inputs.ScreenSize.Y * 0.9 - 15)
            {
                pos.Y = newPosY;
            }

            weapon.Update(inputs, data, events);
        }

        override public void Draw(Graphics graphics)
        {

            graphics.DrawPlayer(pos, rotation);
        }

        class Weapon
        {
            int frequency = 15;
            int timer = 0;
            float speed = 10;

            public void Update(GameInputs inputs, GameData data, IList<GameEvent>events)
            {
                if (timer!=0)
                    timer -= 1;

                if (inputs.Shoot && timer == 0)
                {
                    timer = frequency;
                    Vector2 bulletDir = inputs.ShootTarget - data.Player.pos;
                    bulletDir = bulletDir / bulletDir.Length();

                    Vector2 bulletPos = data.Player.pos + bulletDir * 0.8f * 20;
                    float bulletRotation = MyMaths.FindVectorAngle(bulletDir);

                    EntityBullet b = new EntityBullet(bulletPos, bulletDir / bulletDir.Length(), bulletRotation, speed) ;
                    data.AddBulletDelayed(b);

                    events.Add(new BulletShootEvent(b));
                }
                

            }
        }
        
    }
}
