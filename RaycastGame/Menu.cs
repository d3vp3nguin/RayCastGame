using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RaycastGame
{
    public class Menu : Drawable
    {
        private bool isActive = false;
        public bool IsActive { get { return isActive; } }

        private bool isPressedEsc = false;

        private RectangleShape background;
        private Label[] labels;
        private Button[] buttons;
        private Scroller[] scrollers;

        private int idActive = -1;

        private Window gameWindow;
        private RayCasting rayCasting;
        private ObjectRenderer objectRenderer;
        private AudioManager audioManager;

        public Menu(Window gameWindow, RayCasting rayCasting, ObjectRenderer objectRenderer, AudioManager audioManager) 
        {
            this.gameWindow = gameWindow;
            this.rayCasting = rayCasting;
            this.objectRenderer = objectRenderer;
            this.audioManager = audioManager;

            background = new RectangleShape(Config.MenuSize);
            background.Origin = Config.MenuSize / 2;
            background.Position = new Vector2f(Config.GameResolution.X / 2, Config.GameResolution.Y / 2);
            background.FillColor = Config.MenuBackgroundColor;

            Vector2f size = new Vector2f((Config.MenuSize.X - 3 * Config.MenuOffset.X) / 2, (Config.MenuSize.Y - 6 * Config.MenuOffset.Y) / 5);
            float yStart = Config.GameResolution.Y / 2 - Config.MenuSize.Y / 2 + Config.MenuOffset.Y;

            labels = new Label[4];
            labels[0] = new Label(size, new Vector2f(Config.GameResolution.X / 2 - size.X / 2 - Config.MenuOffset.X / 2, yStart + 1 * size.Y / 2 + 0 * Config.MenuOffset.Y), Config.MenuButtonOffColor, Color.Transparent, "Rays Count", 0);
            labels[1] = new Label(size, new Vector2f(Config.GameResolution.X / 2 - size.X / 2 - Config.MenuOffset.X / 2, yStart + 3 * size.Y / 2 + 1 * Config.MenuOffset.Y), Config.MenuButtonOffColor, Color.Transparent, "Mouse Sensativity", 1);
            labels[2] = new Label(size, new Vector2f(Config.GameResolution.X / 2 - size.X / 2 - Config.MenuOffset.X / 2, yStart + 5 * size.Y / 2 + 2 * Config.MenuOffset.Y), Config.MenuButtonOffColor, Color.Transparent, "Player Input", 2);
            labels[3] = new Label(size, new Vector2f(Config.GameResolution.X / 2 - size.X / 2 - Config.MenuOffset.X / 2, yStart + 7 * size.Y / 2 + 3 * Config.MenuOffset.Y), Config.MenuButtonOffColor, Color.Transparent, "Volume", 3);
            
            float x1 = (size.X - 1.5f * Config.MenuOffset.X) / 4;
            buttons = new Button[7];
            buttons[0] = new Button(new Vector2f(x1, size.Y), new Vector2f(Config.GameResolution.X / 2 + 1 * Config.MenuOffset.X / 2 + 1 * x1 / 2, yStart + 1 * size.Y / 2 + 0 * Config.MenuOffset.Y), Config.MenuButtonOffColor, Color.Transparent, "X / 1", 4);
            buttons[1] = new Button(new Vector2f(x1, size.Y), new Vector2f(Config.GameResolution.X / 2 + 2 * Config.MenuOffset.X / 2 + 3 * x1 / 2, yStart + 1 * size.Y / 2 + 0 * Config.MenuOffset.Y), Config.MenuButtonOffColor, Color.Transparent, "X / 2", 5);
            buttons[2] = new Button(new Vector2f(x1, size.Y), new Vector2f(Config.GameResolution.X / 2 + 3 * Config.MenuOffset.X / 2 + 5 * x1 / 2, yStart + 1 * size.Y / 2 + 0 * Config.MenuOffset.Y), Config.MenuButtonOffColor, Color.Transparent, "X / 4", 6);
            buttons[3] = new Button(new Vector2f(x1, size.Y), new Vector2f(Config.GameResolution.X / 2 + 4 * Config.MenuOffset.X / 2 + 7 * x1 / 2, yStart + 1 * size.Y / 2 + 0 * Config.MenuOffset.Y), Config.MenuButtonOffColor, Color.Transparent, "X / 5", 7);

            float x2 = (size.X - 0.5f * Config.MenuOffset.X) / 2;
            buttons[4] = new Button(new Vector2f(x2, size.Y), new Vector2f(Config.GameResolution.X / 2 + 1 * Config.MenuOffset.X / 2 + 1 * x2 / 2, yStart + 5 * size.Y / 2 + 2 * Config.MenuOffset.Y), Config.MenuButtonOffColor, Color.Transparent, "WASD", 8);
            buttons[5] = new Button(new Vector2f(x2, size.Y), new Vector2f(Config.GameResolution.X / 2 + 2 * Config.MenuOffset.X / 2 + 3 * x2 / 2, yStart + 5 * size.Y / 2 + 2 * Config.MenuOffset.Y), Config.MenuButtonOffColor, Color.Transparent, "Arrows", 9);

            float x3 = size.X * 2 + Config.MenuOffset.X;
            buttons[6] = new Button(new Vector2f(x3, size.Y), new Vector2f(Config.GameResolution.X / 2, yStart + 9 * size.Y / 2 + 4 * Config.MenuOffset.Y), Config.MenuButtonOffColor, Color.Transparent, "Quit", 10);

            scrollers = new Scroller[2];
            scrollers[0] = new Scroller(size, new Vector2f(Config.GameResolution.X / 2 + size.X / 2 + Config.MenuOffset.X / 2, yStart + 3 * size.Y / 2 + 1 * Config.MenuOffset.Y), Config.MenuButtonOffColor, Config.MenuButtonOnColor, Settings.MouseSensativity.ToString(), (Settings.MouseSensativity - Config.MouseMinSensativity) / Config.MouseMaxSensativity, 
                                        Config.MouseMinSensativity, Config.MouseMaxSensativity, 11);
            scrollers[1] = new Scroller(size, new Vector2f(Config.GameResolution.X / 2 + size.X / 2 + Config.MenuOffset.X / 2, yStart + 7 * size.Y / 2 + 3 * Config.MenuOffset.Y), Config.MenuButtonOffColor, Config.MenuButtonOnColor, Settings.Volume.ToString(), (Settings.Volume - Config.MinVolume) / Config.MaxVolume,
                                        Config.MinVolume, Config.MaxVolume, 12);

            UpdateButtonColors();
            scrollers[0].RecalcForeground(scrollers[0].Value);
            scrollers[1].RecalcForeground(scrollers[0].Value);
        }

        public void Update()
        {
            EscListening();
            MenuInput();
        }

        private void EscListening()
        {
            if (Keyboard.IsKeyPressed(Settings.MenuKey))
            {
                if (!isPressedEsc)
                {
                    isActive = !isActive;
                    gameWindow.SetMouseCursorVisible(isActive);

                    Mouse.SetPosition(new Vector2i(Config.GameResolution.X / 2, Config.GameResolution.Y / 2));
                }
                isPressedEsc = true;
            }
            else
                isPressedEsc = false;
        }

        private void MenuInput()
        {
            if (!isActive) return;
            int checkId = idActive;

            if (buttons[0].IsPressed() && IsActiveId(buttons[0].ID)) ChangeRaysCount(Config.GameResolution.X / 1, buttons[0].ID);
            if (buttons[1].IsPressed() && IsActiveId(buttons[1].ID)) ChangeRaysCount(Config.GameResolution.X / 2, buttons[1].ID);
            if (buttons[2].IsPressed() && IsActiveId(buttons[2].ID)) ChangeRaysCount(Config.GameResolution.X / 4, buttons[2].ID);
            if (buttons[3].IsPressed() && IsActiveId(buttons[3].ID)) ChangeRaysCount(Config.GameResolution.X / 5, buttons[3].ID);

            if (buttons[4].IsPressed() && IsActiveId(buttons[4].ID)) SetPlayerInputButtons(Keyboard.Key.W, Keyboard.Key.S, Keyboard.Key.A, Keyboard.Key.D, buttons[4].ID);
            if (buttons[5].IsPressed() && IsActiveId(buttons[5].ID)) SetPlayerInputButtons(Keyboard.Key.Up, Keyboard.Key.Down, Keyboard.Key.Left, Keyboard.Key.Right, buttons[5].ID);

            if (buttons[6].IsPressed() && IsActiveId(buttons[6].ID)) gameWindow.Close();

            UpdateButtonColors();

            if (scrollers[0].IsScrolled() && IsActiveId(scrollers[0].ID))
            {
                Settings.MouseSensativity = scrollers[0].GetScrollFullValue();
                scrollers[0].UpdateText(Settings.MouseSensativity.ToString("0.00"));
                ChangeActiveId(scrollers[0].ID);
            }

            if (scrollers[1].IsScrolled() && IsActiveId(scrollers[1].ID))
            {
                Settings.Volume = scrollers[1].GetScrollFullValue();
                scrollers[1].UpdateText(Settings.Volume.ToString("0.00"));
                ChangeActiveId(scrollers[1].ID);
                audioManager.ChangeVolume();
            }

            if (checkId == idActive && !Mouse.IsButtonPressed(Mouse.Button.Left)) ChangeActiveId(-1);
        }

        private void ChangeRaysCount(int value, int idActive)
        {
            Settings.RaysCount = value;
            Config.WallScale = Config.GameResolution.X / Settings.RaysCount;
            Config.DeltaAngle = Config.FOV / Settings.RaysCount;
            rayCasting.Init();
            objectRenderer.Init();
            ChangeActiveId(idActive);
        }

        private void SetPlayerInputButtons(Keyboard.Key w, Keyboard.Key s, Keyboard.Key a, Keyboard.Key d, int idActive)
        {
            Settings.ForwardKey = w;
            Settings.BackwardKey = s;
            Settings.LeftKey = a;
            Settings.RightKey = d;
            ChangeActiveId(idActive);
        }

        private void ChangeActiveId(int idActive)
        {
            this.idActive = idActive;
        }

        private bool IsActiveId(int idActive)
        {
            if (this.idActive == idActive || this.idActive == -1) return true;
            return false;
        }

        private void UpdateButtonColors()
        {
            foreach (Button button in buttons)
                button.ChangeForegroundColor(Config.MenuButtonOffColor);

            if (Settings.RaysCount == Config.GameResolution.X / 1) buttons[0].ChangeForegroundColor(Config.MenuButtonOnColor);
            if (Settings.RaysCount == Config.GameResolution.X / 2) buttons[1].ChangeForegroundColor(Config.MenuButtonOnColor);
            if (Settings.RaysCount == Config.GameResolution.X / 4) buttons[2].ChangeForegroundColor(Config.MenuButtonOnColor);
            if (Settings.RaysCount == Config.GameResolution.X / 5) buttons[3].ChangeForegroundColor(Config.MenuButtonOnColor);

            if (Settings.ForwardKey == Keyboard.Key.W) buttons[4].ChangeForegroundColor(Config.MenuButtonOnColor);
            if (Settings.ForwardKey == Keyboard.Key.Up) buttons[5].ChangeForegroundColor(Config.MenuButtonOnColor);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(background, states);
            foreach (UIelement uIelement in labels)
                target.Draw(uIelement, states);
            foreach (UIelement uIelement in buttons)
                target.Draw(uIelement, states);
            foreach (UIelement uIelement in scrollers)
                target.Draw(uIelement, states);
        }
    }
}
