using Raylib_cs;
using Renderer;
using System.Numerics;

namespace Geostorm
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            const int screenWidth = 1600;
            const int screenHeight = 900;

            // Initialization
            //--------------------------------------------------------------------------------------
            Raylib.SetTraceLogCallback(&Logging.LogConsole);
            Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE);
            Raylib.InitWindow(screenWidth, screenHeight, "ImGui demo");
            Raylib.SetTargetFPS(60);

            Raylib.InitAudioDevice();

            ImguiController controller = new ImguiController();

            EntityPlayer player = new EntityPlayer();
            player.pos.X = screenWidth / 2f;
            player.pos.Y = screenHeight / 2f;


            Sounds sounds = new Sounds();
            sounds.Load();
            EventAnimation animation = new EventAnimation();


            GameInputs inputs = new GameInputs(screenWidth, screenHeight);
            Graphics graphics = new Graphics(inputs.ScreenSize);
            Game game = new Game(inputs);

            game.AddEventListener(sounds);
            game.AddEventListener(animation);
            //EditorScreen editor = new EditorScreen();

            controller.Load(screenWidth, screenHeight);
            //--------------------------------------------------------------------------------------

            // Main game loop
            while (!Raylib.WindowShouldClose())
            {

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE))
                    Raylib.WindowShouldClose();

                inputs.DeltaTime = Raylib.GetFrameTime();
                inputs.ShootTarget = Raylib.GetMousePosition();
                inputs.Space = Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE);
                inputs.Shoot = Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT) ;
                inputs.MoveForward = Raylib.IsKeyDown(KeyboardKey.KEY_E);
                inputs.TurnLeft = Raylib.IsKeyDown(KeyboardKey.KEY_S);
                inputs.TurnRight = Raylib.IsKeyDown(KeyboardKey.KEY_F);
                inputs.EnemySpawnerTest = Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE);

                controller.Update(inputs.DeltaTime);

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);


                game.Update(inputs, graphics);
                game.Render(graphics, inputs.ScreenSize);



                Raylib.EndDrawing();

            }

            // De-Initialization
            //--------------------------------------------------------------------------------------
            controller.Dispose();
            Raylib.CloseAudioDevice();
            Raylib.CloseWindow();
            //--------------------------------------------------------------------------------------
        }
    }
}