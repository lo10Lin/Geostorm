using Renderer;
using System.Collections.Generic;

namespace Geostorm
{
    abstract class ISystem
    {
        public abstract void Update(in GameInputs inputs, GameData data, IList <GameEvent>events);
    }
}
