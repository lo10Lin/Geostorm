using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm;

namespace Renderer
{
    abstract class IGameEventListener
    {
        public abstract void DoHandleEvents(IEnumerable<GameEvent> events, GameData data, Graphics graphics);
    }
}
