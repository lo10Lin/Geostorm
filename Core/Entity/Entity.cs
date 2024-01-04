using System.Numerics;
using System.Collections.Generic;
using Renderer;

namespace Geostorm
{
    abstract class Entity
    {
        public bool isDead;
        public Vector2 pos;
        public float rotation = 0;
        public float collisionRadius;
        public abstract void Update(in GameInputs inputs, GameData data, IList<GameEvent> events);
        public abstract void Draw(Graphics graphics);

    }
}
    