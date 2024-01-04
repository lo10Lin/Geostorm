using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer
{
    abstract class Animation
    {
        public bool isfinished;
        public float time;
        public abstract void Update();
    }
}
