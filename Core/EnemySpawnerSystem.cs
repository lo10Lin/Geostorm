using System;
using System.Collections.Generic;
using Renderer;

namespace Geostorm
{
    class EnemySpawnerSystem : ISystem
    {
        float timer = 60;
        float frequency = 60;

        public override void Update(in GameInputs inputs, GameData data, IList<GameEvent> events)
        {
            //if (inputs.EnemySpawnerTest)
            //{
            //    EnemySpinner enemy = new EnemySpinner(inputs.ScreenSize);
            //    data.AddEnemyDelayed(enemy);
            //}
            if (data.Player.life <= 0)
                frequency = 60;

            if (timer <= 0)
            {
                Random rnd = new Random();
                if (rnd.Next(5) == 0)
                {
                    EnemySprint enemy = new EnemySprint(inputs.ScreenSize);
                    data.AddEnemyDelayed(enemy);
                }
                else
                {
                    EnemyGrunt enemy = new EnemyGrunt(inputs.ScreenSize);
                    data.AddEnemyDelayed(enemy);
                }
                if(rnd.Next(8)==0)
                {
                    EnemySpinner enemy = new EnemySpinner(inputs.ScreenSize);
                    data.AddEnemyDelayed(enemy);
                }
                timer = frequency;
            }
            if (timer > 0)
                timer -= 1;

            if (frequency > 1)
                frequency = 60 - (int)(data.Score / 50);

        }
    }
}
