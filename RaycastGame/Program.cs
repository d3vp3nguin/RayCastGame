using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace RaycastGame
{
    class Program
    {
        private static RenderWindow gameWindow;
        private static View gameView;

        private static Clock gameClock;

        private static Map map;
        private static Player player;

        private static RayCasting rayCasting;
        private static ObjectRenderer objectRenderer;

        private static DevInfo gameDevInfo;
        private static float deltaTime = 0;
        private static int fps = 0;

        static void Main(string[] args)
        {
            Init();
            MainCycle();
        }

        private static void Init()
        {
            gameWindow = new RenderWindow(new VideoMode((uint)Settings.GameResolution.X, (uint)Settings.GameResolution.Y), Settings.GameTitle, Settings.GameStyle);
            gameWindow.Closed += WindowClose;

            gameView = new View(new FloatRect(0, 0, Settings.GameResolution.X, Settings.GameResolution.Y));
            gameWindow.SetView(gameView);

            gameClock = new Clock();

            map = new Map();
            player = new Player(map);

            rayCasting = new RayCasting(player, map);

            objectRenderer = new ObjectRenderer(rayCasting);

            gameDevInfo = new DevInfo();
        }

        private static void MainCycle()
        {
            while (gameWindow.IsOpen)
            {
                deltaTime = gameClock.Restart().AsSeconds();

                gameWindow.DispatchEvents();
                gameWindow.Clear(Color.Black);

                player.Update(deltaTime);
                rayCasting.RayCast();
                objectRenderer.Update();

                gameWindow.Draw(objectRenderer);
                gameWindow.Draw(map);
                gameWindow.Draw(player);
                gameWindow.Draw(rayCasting);

                gameDevInfo.UpdateInfo(fps, player.Position, player.PositionMap, player.Rotation);
                gameWindow.Draw(gameDevInfo);

                gameWindow.Display();

                fps = (int)(1f / Math.Abs(deltaTime));
            }
        }

        private static void WindowClose(object sender, EventArgs e)
        {
            gameWindow.Close();
        }
    }
}