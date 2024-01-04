using System.Numerics;
using System.Collections.Generic;
using Renderer;

namespace Geostorm
{
    class EntityBullet : Entity
    {
        Vector2 direction;
        float speed;
        public EntityBullet(Vector2 position, Vector2 dir, float rota, float velocity)
        {
            pos = position;
            direction = dir;
            rotation = rota;
            speed = velocity;
            collisionRadius = 0.7f * 15;
        }
        override public void Update(in GameInputs inputs, GameData data, IList<GameEvent> events)
        {
            pos += direction * speed;
            if (pos.X < inputs.ScreenSize.X * 0.1 + 15 || pos.X > inputs.ScreenSize.X * 0.9 - 15 || pos.Y < inputs.ScreenSize.Y * 0.1 + 15 || pos.Y > inputs.ScreenSize.Y * 0.9 - 15)
            {
                isDead = true;
                events.Add(new BulletWallEvent(pos));
            }
        }
        override public void Draw(Graphics graphics)
        {
            graphics.DrawBullet(pos, rotation);
        }
    }
}
