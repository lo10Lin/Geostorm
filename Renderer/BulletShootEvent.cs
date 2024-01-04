using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm;

namespace Renderer
{
    class BulletShootEvent : GameEvent
    {
        public EntityBullet Bullet;

        public BulletShootEvent(EntityBullet bullet)
        {
            Bullet = bullet;
        }
        public override void DoUpdate(Graphics graphics)
        {
            throw new NotImplementedException();
        }
    }
}
