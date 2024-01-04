using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm;

namespace Renderer
{
    abstract class GameEvent
    {
        public abstract void DoUpdate(Graphics graphics);
    }
}
