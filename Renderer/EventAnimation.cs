using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm;

namespace Renderer
{
    class EventAnimation : IGameEventListener
    {
        public void HandleEvents(IEnumerable<GameEvent> events, GameData data, Graphics graphics)
        {
            foreach(GameEvent e in events)
            {
                switch (e)
                {
                    case EnemyKilledEvent:
                        e.DoUpdate(graphics);
                        break;
                    case BulletWallEvent:
                        e.DoUpdate(graphics);
                        break;
                }
            }
        }

        public override void DoHandleEvents(IEnumerable<GameEvent> events, GameData data, Graphics graphics)
        {
            HandleEvents(events, data, graphics);
        }
    }
}
