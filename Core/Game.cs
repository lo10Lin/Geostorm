using System.Numerics;
using System.Collections.Generic;
using Renderer;
using System;

namespace Geostorm
{
    class Game
    {
        CollisionSystem collisionSystem = new CollisionSystem();
        EnemySpawnerSystem enemySpawnerSystem = new EnemySpawnerSystem();
        List<ISystem> allSystems = new List<ISystem>();

        public Game(GameInputs inputs)
        {
            data.Player.pos.X = inputs.ScreenSize.X / 2;
            data.Player.pos.Y = inputs.ScreenSize.Y / 2;

            allSystems.Add(collisionSystem);
            allSystems.Add(enemySpawnerSystem);
        }
        GameData data = new GameData();
        List<IGameEventListener> eventListeners = new List <IGameEventListener>();


        public void AddEventListener(IGameEventListener listener)
        {
            eventListeners.Add(listener);
        }

        public void Update(GameInputs inputs, Graphics graphics)
        {
            
            if (inputs.Space && data.Start)
                data.Pause = !data.Pause;

            if (inputs.Space && !data.Start)
                data.Start = !data.Start;

            if(data.Start)
            {
                if (!data.Pause)
                {
                    List<GameEvent> events = new List<GameEvent>();
                    foreach (Entity e in data.Entities)
                    {
                        e.Update(inputs, data, events);
                    }

                    foreach (ISystem system in allSystems)
                    {
                        system.Update(inputs, data, events);
                    }

                    foreach (IGameEventListener e in eventListeners)
                    {
                        e.DoHandleEvents(events, data, graphics);
                    }

                    data.Synchronize();

                    if (data.Player.life <= 0)
                    {
                        data.Reset(inputs.ScreenSize);
                        graphics.animations.Clear();
                    }
                }
            }
            
        }

        public void Render(Graphics graphics, Vector2 ScreenSize)
        {
            graphics.DrawBackground();

            if(data.Start)
            {
                if(!data.Pause)
                {
                    foreach (Entity e in data.Entities)
                    {
                        e.Draw(graphics);
                    }

                    graphics.animations.RemoveAll(animations => animations.isfinished);
                    foreach (Animation a in graphics.animations)
                    {
                        a.Update();
                    }
                }
                else
                {
                    graphics.DrawPause();
                }

                
            }
            else
            {
                graphics.DrawStart();
            }
            

            graphics.DrawUI(data.Player.life, data.Score, data.BestScore );
            
            
        }
    }
}
