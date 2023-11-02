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

        private static AudioManager audioManager;

        private static Menu menu;

        private static float deltaTime = 0;
        private static int fps = 0;

        static void Main(string[] args)
        {
            Init();
            MainCycle();
        }

        private static void Init()
        {
            gameWindow = new RenderWindow(new VideoMode((uint)Config.GameResolution.X, (uint)Config.GameResolution.Y), Config.GameTitle, Config.GameStyle);
            gameWindow.Closed += WindowClose;

            gameView = new View(new FloatRect(0, 0, Config.GameResolution.X, Config.GameResolution.Y));
            gameWindow.SetView(gameView);

            gameClock = new Clock();

            map = new Map();
            player = new Player(map);

            rayCasting = new RayCasting(player, map);

            objectRenderer = new ObjectRenderer(player, rayCasting, map);

            gameDevInfo = new DevInfo();

            audioManager = new AudioManager(player);
            audioManager.ChangeVolume();

            menu = new Menu(gameWindow, rayCasting, objectRenderer, audioManager);

            gameWindow.SetMouseCursorVisible(false);
            Mouse.SetPosition(new Vector2i(Config.GameResolution.X / 2, Config.GameResolution.Y / 2));
        }

        private static void MainCycle()
        {
            while (gameWindow.IsOpen)
            {
                deltaTime = gameClock.Restart().AsSeconds();

                gameWindow.DispatchEvents();
                gameWindow.Clear(Color.Black);

                menu.Update();
                if (gameWindow.HasFocus() && !menu.IsActive)
                {
                    player.Update(deltaTime);
                    audioManager.Update(deltaTime);
                }
                rayCasting.RayCast();
                objectRenderer.Update();

                gameWindow.Draw(objectRenderer);
                gameWindow.Draw(map);
                gameWindow.Draw(player);
                gameWindow.Draw(rayCasting);
                if (menu.IsActive) gameWindow.Draw(menu);

                gameDevInfo.UpdateInfo(fps, player.Position, player.PositionMap, player.Angle);
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