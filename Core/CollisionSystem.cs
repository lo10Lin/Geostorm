using System.Numerics;
using System.Collections.Generic;
using Renderer;

namespace Geostorm
{
    class CollisionSystem : ISystem
    {
        public override void Update(in GameInputs inputs, GameData data, IList<GameEvent> events )
        {
            foreach (EntityBullet b in data.Bullets)
            {
                foreach (EntityEnemy e in data.Enemies)
                {
                    if (e.spawnTime > 0)
                        continue;
                    Vector2 BulletEnemy = e.pos - b.pos;
                    if (BulletEnemy.Length() < (e.collisionRadius + b.collisionRadius))
                    {
                        e.isDead = true;
                        b.isDead = true;

                        data.Score += 10;
                        events.Add(new EnemyKilledEvent(e));
                    }
                }
            }

            foreach (EntityEnemy e in data.Enemies)
            {
                if (e.spawnTime > 0)
                    continue;
                Vector2 PlayerEnemy = e.pos - data.Player.pos;
                if (PlayerEnemy.Length() < (e.collisionRadius + data.Player.collisionRadius))
                {
                    e.isDead = true;

                    data.Player.life -= 1;
                }
            }
        }
    }
}
