using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Geostorm.MyMaths;
using System.IO;

namespace Renderer
{
    class Graphics
    {
        public Texture2D background;
        Vector2 screenSize;
        List<Vector2> playerPoints = new List<Vector2>();
        List<Vector2> bulletPoints = new List<Vector2>();
        List<Vector2> gruntPoints = new List<Vector2>();
        List<Vector2> sprintPoints = new List<Vector2>();
        List<Vector2> spinnerPoints = new List<Vector2>();
        List<Vector2> arrowPoints = new List<Vector2>();
        List<Vector2> gridPoints = new List<Vector2>();
        public List<Animation> animations = new List<Animation>(); 


        public Graphics(Vector2 Screen)
        {
            float preScale = 20.0f;
            screenSize = Screen;
            playerPoints.Add(new Vector2(-1.0f, 0.0f) * preScale);  //outercenter 
            playerPoints.Add(new Vector2(-0.4f, -0.8f) * preScale); //topOuterWing
            playerPoints.Add(new Vector2(0.6f, -0.3f) * preScale);  //topGun
            playerPoints.Add(new Vector2(-0.2f, -0.55f) * preScale);//topInnterWing
            playerPoints.Add(new Vector2(-0.5f, 0.0f) * preScale);  //innerCenter
            playerPoints.Add(new Vector2(playerPoints[3].X, -playerPoints[3].Y)); //bottomInnerWing
            playerPoints.Add(new Vector2(playerPoints[2].X, -playerPoints[2].Y)); //bottomGun
            playerPoints.Add(new Vector2(playerPoints[1].X, -playerPoints[1].Y)); //bottomOuterWing


            arrowPoints.Add(new Vector2(1.4f, -0.2f) * preScale);
            arrowPoints.Add(new Vector2(1.4f, 0.2f) * preScale);
            arrowPoints.Add(new Vector2(1.8f, 0.0f) * preScale);

            gruntPoints.Add(new Vector2(-1.0f, 0.0f) * preScale);
            gruntPoints.Add(new Vector2(-0.0f, -1.0f) * preScale);
            gruntPoints.Add(new Vector2(1.0f, 0.0f) * preScale);
            gruntPoints.Add(new Vector2(-0.0f, 1.0f) * preScale);

            sprintPoints.Add(new Vector2(-1.0f, 0.0f) * preScale);
            sprintPoints.Add(new Vector2(-0.0f, -1.0f) * preScale);
            sprintPoints.Add(new Vector2(1.0f, 0.0f) * preScale);
            sprintPoints.Add(new Vector2(-0.0f, 1.0f) * preScale);

            spinnerPoints.Add(new Vector2(-1.0f, -1.0f) * preScale);
            spinnerPoints.Add(new Vector2(0.0f, -1.0f) * preScale);
            spinnerPoints.Add(new Vector2(0.0f, 0.0f) * preScale);
            spinnerPoints.Add(new Vector2(1.0f, -1.0f) * preScale);
            spinnerPoints.Add(new Vector2(1.0f, 0.0f) * preScale);
            spinnerPoints.Add(new Vector2(0.0f, 0.0f) * preScale);
            spinnerPoints.Add(new Vector2(1.0f, 1.0f) * preScale);
            spinnerPoints.Add(new Vector2(0.0f, 1.0f) * preScale);
            spinnerPoints.Add(new Vector2(0.0f, 0.0f) * preScale);
            spinnerPoints.Add(new Vector2(-1.0f, 1.0f) * preScale);
            spinnerPoints.Add(new Vector2(-1.0f, 0.0f) * preScale);
            spinnerPoints.Add(new Vector2(0.0f, 0.0f) * preScale);

            preScale = 15.0f;
            bulletPoints.Add(new Vector2(-0.3f, 0.0f) * preScale);
            bulletPoints.Add(new Vector2(-0.1f, 0.2f) * preScale);
            bulletPoints.Add(new Vector2(0.8f, 0.0f) * preScale);  
            bulletPoints.Add(new Vector2(bulletPoints[1].X, -bulletPoints[1].Y));

            float x = screenSize.X * 0.1f + 20;
            while (x < screenSize.X * 0.9f) // vertical lines
            {
                gridPoints.Add(new Vector2(x, screenSize.Y * 0.1f));
                gridPoints.Add(new Vector2(x, screenSize.Y * 0.9f));
                x += 20;
            }

            float y = screenSize.Y * 0.1f + 20;
            while (y < screenSize.Y * 0.9f) // horizontal lines
            {
                gridPoints.Add(new Vector2(screenSize.X * 0.1f, y));
                gridPoints.Add(new Vector2(screenSize.X * 0.9f, y));
                y += 20;
            }
            background = LoadTexture("../../../assets/background.png");
        }
        ~Graphics()
        {
            UnloadTexture(background);
        }

        public void DrawPlayer(Vector2 pos, float rotation)
        {
            //draw player
            

            for (int i = 0; i < playerPoints.Count; i++)
            {
                DrawLineV(pos + RotationV2(playerPoints[i], rotation), 
                          pos + RotationV2(playerPoints[(i + 1) % playerPoints.Count], rotation), 
                          Color.WHITE);
            }
            //draw arrow

            Vector2 playerToMouse = (GetMousePosition() - pos);

            float rota = FindVectorAngle(playerToMouse);

            DrawTriangle(pos + RotationV2(arrowPoints[0], rota), pos + RotationV2(arrowPoints[1], rota), pos + RotationV2(arrowPoints[2], rota), Color.WHITE);

        }

        public void DrawGrunt(Vector2 pos, float rotation, float alpha)
        {
            for (int i = 0; i < gruntPoints.Count; i++)
            {
                DrawLineV(pos + gruntPoints[i], pos + gruntPoints[(i + 1) % gruntPoints.Count], ColorAlpha(Color.SKYBLUE, alpha));
            }
        }

        public void DrawExplosion(Vector2 pos, IList<Vector2> dir, float time, IList<Raylib_cs.Color> colors, IList<int> speed)
        {
            float radius = 1.5f;
            Random rnd = new Random();
            for (int i = 0; i < dir.Count; i++)
            {
                float newPosX = pos.X + dir[i].X * time * speed[i] / 100;
                float newPosY = pos.Y + dir[i].Y * time * speed[i] / 100;



                if (newPosX > screenSize.X * 0.1 + 15 && newPosX < screenSize.X * 0.9 - 15)
                {
                    pos.X = newPosX;
                }
                if (newPosY > screenSize.Y * 0.1 + 15 && newPosY < screenSize.Y * 0.9 - 15)
                {
                    pos.Y = newPosY;
                }
                DrawCircleV(pos + dir[i] * time * speed[i] / 100, radius, colors[i]);
            }
        }

        public void DrawSprint(Vector2 pos, float rotation, float alpha)
        {
            for (int i = 0; i < sprintPoints.Count; i++)
            {
                DrawLineV(pos + sprintPoints[i], pos + sprintPoints[(i + 1) % sprintPoints.Count], ColorAlpha(Color.PINK, alpha));
                if(i<2)
                    DrawLineV(pos + sprintPoints[i], pos + sprintPoints[(i + 2) ], ColorAlpha(Color.PINK, alpha));
            }
        }

        public void DrawSpinner(Vector2 pos, float rotation, float alpha)
        {
            for (int i = 0; i < spinnerPoints.Count; i += 3)
            {
                DrawLineV(pos + RotationV2(spinnerPoints[i], rotation), pos + RotationV2(spinnerPoints[i + 1], rotation), ColorAlpha(Color.RED, alpha));
                DrawLineV(pos + RotationV2(spinnerPoints[i + 1], rotation), pos + RotationV2(spinnerPoints[i + 2], rotation), ColorAlpha(Color.RED, alpha));
                DrawLineV(pos + RotationV2(spinnerPoints[i + 2], rotation), pos + RotationV2(spinnerPoints[i], rotation), ColorAlpha(Color.RED, alpha));
            }
        }

        public void DrawBullet(Vector2 pos, float rotation)
        {
            for (int i = 0; i < bulletPoints.Count; i++)
            {
                DrawLineV(pos + RotationV2(bulletPoints[i], rotation), pos + RotationV2( bulletPoints[(i + 1) % bulletPoints.Count], rotation), Color.BEIGE);
            }

        }

        public void DrawBackground()
        {
            Vector2 tiling = new Vector2(3,3);
            Vector2 offset = new Vector2(0,0);
            Rectangle rec = new Rectangle(0,0, screenSize.X, screenSize.Y);

            DrawTextureQuad(background, tiling, offset, rec, Color.GRAY);
            for (int i = 0; i < gridPoints.Count; i+=2)
            {
                DrawLineV(gridPoints[i], gridPoints[i+1], new Color(0, 0, 80, 200));
            }

        }
        public void DrawUI(int life, int score, int bestScore)
        {
            Rectangle borderLines = new Rectangle(screenSize.X * 0.1f, screenSize.Y * 0.1f, screenSize.X * 0.8f, screenSize.Y * 0.8f);
            DrawRectangleLinesEx(borderLines, 5, Color.WHITE);

            DrawText($"Score: {score}", (int)(screenSize.X * 0.1f), (int)(screenSize.Y * 0.04f), (int)(screenSize.X / 50), Color.YELLOW);
            DrawText($"Personal Best: {bestScore}", (int)(screenSize.X * 0.4f), (int)(screenSize.Y * 0.94f), (int)(screenSize.X / 50), Color.YELLOW);
            DrawText($"HP: {life}", (int)(screenSize.X * 0.85f), (int)(screenSize.Y * 0.04f), (int)(screenSize.X / 50), Color.YELLOW);

            DrawText($"MOVE: E,S,D", (int)(screenSize.X * 0.01f), (int)(screenSize.Y * 0.2f), (int)(screenSize.X / 90), Color.WHITE);
            DrawText($"PAUSE: SPACE", (int)(screenSize.X * 0.01f), (int)(screenSize.Y * 0.25f), (int)(screenSize.X / 90), Color.WHITE);
            DrawText($"QUIT: ESCAPE", (int)(screenSize.X * 0.01f), (int)(screenSize.Y * 0.3f), (int)(screenSize.X / 90), Color.WHITE);

        }

        public void DrawPause()
        {
           DrawText($"PAUSE", (int)(screenSize.X * 0.435f), (int)(screenSize.Y * 0.4f), (int)(screenSize.X / 25), Color.WHITE);
           DrawText($"(Press SPACE to resume)", (int)(screenSize.X * 0.415f), (int)(screenSize.Y * 0.5f), (int)(screenSize.X / 70), Color.WHITE);
        }

        public void DrawStart()
        {
            DrawText($"START", (int)(screenSize.X * 0.435f), (int)(screenSize.Y * 0.4f), (int)(screenSize.X / 25), Color.WHITE);
            DrawText($"(Press SPACE to start)", (int)(screenSize.X * 0.415f), (int)(screenSize.Y * 0.5f), (int)(screenSize.X / 70), Color.WHITE);
        }
    }
}
