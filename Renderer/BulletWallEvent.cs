using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm;
using System.Numerics;
using static Raylib_cs.Raylib;


namespace Renderer
{
    class BulletWallEvent : GameEvent
    {
        public Vector2 bullet;

        public BulletWallEvent(Vector2 b)
        {
            bullet = b;
        }
        public override void DoUpdate(Graphics graphics)
        {
            graphics.animations.Add(new DestroyBullet(bullet, graphics));
        }
    }
    class DestroyBullet : Animation
    {
        Vector2 pos;
        int opacity = 255;
        Graphics graphics;
        public List<Vector2> dir = new List<Vector2>();
        List<Raylib_cs.Color> colors = new List<Raylib_cs.Color>();
        List<int> speed = new List<int>();
        public DestroyBullet(Vector2 _pos, Graphics _graphics)
        {
            pos = _pos;
            graphics = _graphics;
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                int angle = (rnd.Next(0, 359));
                int g = (rnd.Next(100, 255));
                int s = (rnd.Next(1, 50));
                Vector2 vec = new Vector2(MathF.Cos(angle * DEG2RAD), MathF.Sin(angle * DEG2RAD));
                colors.Add(new Raylib_cs.Color(255, g, 0, opacity));
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
            opacity -= 255 / 90;
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
