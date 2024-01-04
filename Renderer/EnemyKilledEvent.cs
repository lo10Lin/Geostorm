using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Geostorm;
using static Raylib_cs.Raylib;

namespace Renderer
{
    class EnemyKilledEvent : GameEvent
    {
        public EntityEnemy enemy;

        public EnemyKilledEvent(EntityEnemy e)
        {
            enemy = e;
        }
        
        public override void DoUpdate(Graphics graphics)
        {
            graphics.animations.Add(new Explosion(enemy.pos, graphics));
        }

        
    }
    class Explosion : Animation
    {
        public Vector2 pos = new Vector2();
        public  List<Vector2> dir = new List<Vector2>();
        List<Raylib_cs.Color> colors = new List<Raylib_cs.Color>();
        Graphics graphics;
        List<int> speed = new List<int>();
        int opacity = 255;
        public Explosion(Vector2 _pos, Graphics _graphics)
        {
            pos = _pos;
            graphics = _graphics;
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                int angle = (rnd.Next(0, 359));
                int r= (rnd.Next(100, 255));
                int g= (rnd.Next(100, 255));
                int b= (rnd.Next(100, 255)); 
                int s = (rnd.Next(1, 50));
                Vector2 vec = new Vector2(MathF.Cos(angle * DEG2RAD), MathF.Sin(angle * DEG2RAD));
                colors.Add(new Raylib_cs.Color(r, g, b, opacity));
                dir.Add(vec);
                speed.Add(s);
            }
        }
        public void UpdateDataExplosion()
        {
            if (time > 90)
            {
                isfinished = true;
            }
            for (int i = 0; i < colors.Count; i++)
            {
                colors[i] = new Raylib_cs.Color(colors[i].r, colors[i].g, colors[i].b, opacity);
            }
            opacity -= 255/90;
            time++;
        }
        public void DrawEvent(Graphics graphics)
        {
            graphics.DrawExplosion(pos, dir, time, colors, speed);
        }

        public override void Update()
        {
            UpdateDataExplosion();
            DrawEvent(graphics);

        }
    }
}

