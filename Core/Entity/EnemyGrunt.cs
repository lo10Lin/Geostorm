using System;
using Renderer;
using System.Numerics;

namespace Geostorm 
{
    class EnemyGrunt : EntityEnemy
    {
        public EnemyGrunt(Vector2 ScreenSize)
        {
            spawnTime = 1f;
            speed = 3;
            Random rnd = new Random();
            pos.X = rnd.Next((int)(ScreenSize.X * 0.1f + 15), (int)(ScreenSize.X * 0.9f - 15));
            pos.Y = rnd.Next((int)(ScreenSize.Y * 0.1f + 15), (int)(ScreenSize.Y * 0.9f - 15));
            collisionRadius = 20 * 0.8f;
        }
        override protected void DoUpdate(GameInputs inputs, GameData data)
        {
            Vector2 direction = data.Player.pos - pos;
            direction = direction / direction.Length();
            pos += direction * speed;
        }
        override public void Draw(Graphics graphics)
        {
            graphics.DrawGrunt(pos, rotation, 1 - spawnTime);
        }
    }
}
