using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Geostorm
{
    class GameInputs
    {
        public GameInputs(int screenWidth, int screenHeight)
        {
            ScreenSize.X = screenWidth;
            ScreenSize.Y = screenHeight;
        }
        public Vector2 ScreenSize;
        public float DeltaTime;
        public Vector2 MoveAxis;

        public Vector2 ShootAxis;// controle pour game pad 

        public Vector2 ShootTarget; // controle souris

        public bool Space = false;

        public bool Shoot = false;
        public bool MoveForward = false;
        public bool TurnLeft = false;
        public bool TurnRight = false;
        public bool EnemySpawnerTest = false;



        //public Type operation1()
        // ...
    }

}
