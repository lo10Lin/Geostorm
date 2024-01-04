using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm;
using Raylib_cs;

namespace Renderer
{
    class Sounds : IGameEventListener
    {
        Sound bulletShootSound;
        Sound enemyDeadSound;
        public void Load()
        {

        }
        public void Unload()
        {

        }

        public void HandleEvents(IEnumerable<GameEvent> events, GameData data)
        {
            foreach (GameEvent e in events)
            {
                switch (e)
                {
                    case BulletShootEvent:
                        Raylib.PlaySound(bulletShootSound);
                        break;
                }
            }
        }

        public override void DoHandleEvents(IEnumerable<GameEvent> events, GameData data, Graphics graphics)
        {
            HandleEvents(events, data);
        }
    }
}
