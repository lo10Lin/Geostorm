
using System.Numerics;
using System.Collections.Generic;
using Renderer;


namespace Geostorm
{
    abstract class EntityEnemy : Entity
    {
        public float spawnTime;
        public float speed;
        override sealed public void Update(in GameInputs inputs, GameData data, IList<GameEvent> events)
        {
            if (spawnTime <= 0)
            {
                DoUpdate(inputs, data);
            }
            else
            {
                spawnTime -= 1 / 40f;
            }
                
        }
        protected abstract void DoUpdate(GameInputs inputs, GameData data);
        override public void Draw(Graphics graphics)
        {
        }
    }
}
