using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RaycastGame
{
    public class Menu : Drawable
    {
        private bool isActive = false;
        public bool IsActive { get { return isActive; } }


        private RectangleShape menuBase;

        private RectangleShape[] settingBases;
        private Text[] settingTexts;

        private RectangleShape[][] settingButtons;
        private Text[][] settingButtonTexts;

        private RectangleShape[] scrollerBases;

        private float maxTextWidth = 0f;

        private bool isPressedEsc = false;
        private bool isPressedLKM = false;
        private int idxPressedScroll = -1;

        private Window gameWindow;
        private RayCasting rayCasting;
        private ObjectRenderer objectRenderer;

        public Menu(Window gameWindow, RayCasting rayCasting, ObjectRenderer objectRenderer) 
        {
            this.gameWindow = gameWindow;
            this.gameWindow.SetMouseCursorVisible(false);

            this.rayCasting = rayCasting;
            this.objectRenderer = objectRenderer;

            menuBase = new RectangleShape(Config.MenuSize);
            menuBase.Position = new Vector2f(Config.GameResolution.X / 2 - Config.MenuSize.X / 2, Config.GameResolution.Y / 2 - Config.MenuSize.Y / 2);
            menuBase.FillColor = Config.MenuBaseColor;

            settingBases = new RectangleShape[Config.NumberOfSettings];
            settingTexts = new Text[Config.NumberOfSettings];

            Vector2f settingsSize = new Vector2f(Config.MenuSize.X - 2 * Config.MenuParamOffset, (Config.MenuSize.Y - (Config.NumberOfSettings + 1) * Config.MenuParamOffset) / Config.NumberOfSettings);
            float maxTextHeight = 0f;

            for (int i = 0; i < Config.NumberOfSettings; i++)
            {
                settingBases[i] = new RectangleShape(settingsSize);
                settingBases[i].Position = new Vector2f(Config.GameResolution.X / 2 - menuBase.Size.X / 2 + Config.MenuParamOffset, menuBase.Position.Y + (i + 1) * Config.MenuParamOffset + i * settingBases[i].Size.Y);
                settingBases[i].FillColor = Config.MenuParamColor;

                settingTexts[i] = new Text(Config.NameOfSettings[i], new Font(Config.MenuTextFontPath), Config.MenuTextCharacterSize);
                settingTexts[i].FillColor = Config.MenuTextFillColor;
                settingTexts[i].OutlineColor = Config.MenuTextOutlineColor;
                settingTexts[i].OutlineThickness = Config.MenuTextThickness;

                if (settingTexts[i].GetGlobalBounds().Height > maxTextHeight) maxTextHeight = settingTexts[i].GetGlobalBounds().Height;
                if (settingTexts[i].GetGlobalBounds().Width > maxTextWidth) maxTextWidth = settingTexts[i].GetGlobalBounds().Width;

                settingTexts[i].Position = new Vector2f(settingBases[i].Position.X + Config.MenuParamOffset / 2, settingBases[i].Position.Y + settingsSize.Y / 2 - maxTextHeight);
            }

            settingButtons = new RectangleShape[Config.NumberOfSettings][];
            settingButtonTexts = new Text[Config.NumberOfSettings][];
            scrollerBases = new RectangleShape[Config.NumberOfSettings];

            for (int i = 0; i < Config.NumberOfSettings; i++)
            {
                settingButtons[i] = new RectangleShape[Config.NumberOfStatesSettings[i]];
                settingButtonTexts[i] = new Text[Config.NumberOfStatesSettings[i]];

                Vector2f settingButtonSize = new Vector2f((settingsSize.X - maxTextWidth - Config.MenuParamOffset - Config.NumberOfStatesSettings[i] * Config.MenuParamOffset / 2) / Config.NumberOfStatesSettings[i], settingsSize.Y - Config.MenuParamOffset);
                for (int j = 0; j < Config.NumberOfStatesSettings[i]; j++)
                {
                    if (!Config.IsAllButtonSettings[i])
                    {
                        settingButtons[i][j] = new RectangleShape(settingButtonSize);
                        settingButtons[i][j].Position = new Vector2f(settingBases[i].Position.X + maxTextWidth + Config.MenuParamOffset + j * (settingButtonSize.X + Config.MenuParamOffset / 2), settingBases[i].Position.Y + Config.MenuParamOffset / 2);
                        settingButtons[i][j].FillColor = Config.MenuButtonOffColor;
                    }
                    else
                    {
                        settingButtons[i][j] = new RectangleShape(settingsSize);
                        settingButtons[i][j].Position = settingBases[i].Position;
                        settingButtons[i][j].FillColor = Config.MenuTransparentColor;
                    }

                    settingButtonTexts[i][j] = new Text(Config.NameOfStatesSettings[i][j], new Font(Config.MenuTextFontPath), Config.MenuTextCharacterSize);
                    settingButtonTexts[i][j].FillColor = Config.MenuTextFillColor;
                    settingButtonTexts[i][j].OutlineColor = Config.MenuTextOutlineColor;
                    settingButtonTexts[i][j].OutlineThickness = Config.MenuTextThickness;
                    settingButtonTexts[i][j].Origin = new Vector2f(settingButtonTexts[i][j].GetGlobalBounds().Width / 2, maxTextHeight);
                    settingButtonTexts[i][j].Position = new Vector2f(settingButtons[i][j].Position.X + settingButtons[i][j].Size.X / 2, settingButtons[i][j].Position.Y + settingButtons[i][j].Size.Y / 2);
                }

                if (Config.IsScrollerSettings[i])
                {
                    scrollerBases[i] = new RectangleShape();
                    scrollerBases[i].Position = settingButtons[i][0].Position;
                    scrollerBases[i].Size = new Vector2f(settingButtons[i][0].Size.X * CalcPartOfScroller(Config.ScrollerSettings[i], Config.ScrollerMinSettings[i], Config.ScrollerMaxSettings[i]), settingButtons[i][0].Size.Y);
                    scrollerBases[i].FillColor = Config.MenuButtonOnColor;

                    settingButtonTexts[i][0].DisplayedString = Config.ScrollerSettings[i].ToString("0.00");
                    settingButtonTexts[i][0].Origin = new Vector2f(settingButtonTexts[i][0].GetGlobalBounds().Width / 2, maxTextHeight);
                }
            }
            settingButtons[0][Array.IndexOf(Config.RayCountParam, Settings.RaysCount)].FillColor = Config.MenuButtonOnColor;
            settingButtons[2][Array.IndexOf(Config.KeyForwardParam, Settings.ForwardKey)].FillColor = Config.MenuButtonOnColor;
        }

        public void Update()
        {
            InputMenu();
            CheckClick();
            CheckScroll();
        }

        private void InputMenu()
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

        private float CalcPartOfScroller(float a, float min, float max)
        {
            return (a - min) / (max - min);
        }

        private void CheckClick()
        {
            if (!isActive) return;

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (!isPressedLKM)
                {
                    Vector2i mousePos = Mouse.GetPosition();

                    for (int i = 0; i < Config.NumberOfSettings; i++)
                    {
                        if (!Config.IsButtonSettings[i]) continue;

                        for (int j = 0; j < Config.NumberOfStatesSettings[i]; j++)
                        {
                            if (mousePos.X > settingButtons[i][j].Position.X && mousePos.X < settingButtons[i][j].Position.X + settingButtons[i][j].Size.X &&
                                mousePos.Y > settingButtons[i][j].Position.Y && mousePos.Y < settingButtons[i][j].Position.Y + settingButtons[i][j].Size.Y)
                            {
                                if (i == 0 && j == 0)
                                {
                                    ChangeRaysCount(0);
                                    break;
                                }
                                if (i == 0 && j == 1)
                                {
                                    ChangeRaysCount(1);
                                    break;
                                }
                                if (i == 0 && j == 2)
                                {
                                    ChangeRaysCount(2);
                                    break;
                                }
                                if (i == 0 && j == 3)
                                {
                                    ChangeRaysCount(3);
                                    break;
                                }

                                if (i == 2 && j == 0)
                                {
                                    Settings.ForwardKey = Keyboard.Key.W;
                                    Settings.BackwardKey = Keyboard.Key.S;
                                    Settings.LeftKey = Keyboard.Key.A;
                                    Settings.RightKey = Keyboard.Key.D;
                                    reserColorForSetting(2);
                                    settingButtons[2][0].FillColor = Config.MenuButtonOnColor;
                                    break;
                                }
                                if (i == 2 && j == 1)
                                {
                                    Settings.ForwardKey = Keyboard.Key.Up;
                                    Settings.BackwardKey = Keyboard.Key.Down;
                                    Settings.LeftKey = Keyboard.Key.Left;
                                    Settings.RightKey = Keyboard.Key.Right;
                                    reserColorForSetting(2);
                                    settingButtons[2][1].FillColor = Config.MenuButtonOnColor;
                                    break;
                                }

                                if (i == 4 && j == 0)
                                {
                                    gameWindow.Close();
                                    break;
                                }
                            }
                        }
                    }
                }
                isPressedLKM = true;
            }
            else
                isPressedLKM = false;
        }

        private void CheckScroll()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (idxPressedScroll == -1)
                {
                    Vector2i mousePos = Mouse.GetPosition();

                    for (int i = 0; i < Config.NumberOfSettings; i++)
                    {
                        if (!Config.IsScrollerSettings[i]) continue;

                        if (mousePos.X > settingButtons[i][0].Position.X && mousePos.X < settingButtons[i][0].Position.X + settingButtons[i][0].Size.X &&
                            mousePos.Y > settingButtons[i][0].Position.Y && mousePos.Y < settingButtons[i][0].Position.Y + settingButtons[i][0].Size.Y) idxPressedScroll = i;
                    }
                }
                else
                {
                    float part = MyMath.Clamp((Mouse.GetPosition().X - settingButtons[idxPressedScroll][0].Position.X) / settingButtons[idxPressedScroll][0].Size.X, 0f, 1f);
                    float value = MyMath.Clamp(part * (Config.ScrollerMaxSettings[idxPressedScroll] - Config.ScrollerMinSettings[idxPressedScroll]) + Config.ScrollerMinSettings[idxPressedScroll], Config.ScrollerMinSettings[idxPressedScroll], Config.ScrollerMaxSettings[idxPressedScroll]);
                    scrollerBases[idxPressedScroll].Size = new Vector2f(settingButtons[idxPressedScroll][0].Size.X * part, settingButtons[idxPressedScroll][0].Size.Y);

                    if (idxPressedScroll == 1) Settings.MouseSensativity = value;
                    if (idxPressedScroll == 3) Settings.Volume = value;

                    settingButtonTexts[idxPressedScroll][0].DisplayedString = value.ToString("0.00");
                    settingButtonTexts[idxPressedScroll][0].Origin = new Vector2f(settingButtonTexts[idxPressedScroll][0].GetGlobalBounds().Width / 2, settingButtonTexts[idxPressedScroll][0].Origin.Y);
                }
            }
            else
                idxPressedScroll = -1;
        }

        private void reserColorForSetting(int n)
        {
            for (int i = 0; i < Config.NumberOfStatesSettings[n]; i++)
                settingButtons[n][i].FillColor = Config.MenuButtonOffColor;
        }

        private void ChangeRaysCount(int k)
        {
            Settings.RaysCount = Config.RayCountParam[k];
            reserColorForSetting(0);
            settingButtons[0][k].FillColor = Config.MenuButtonOnColor;
            rayCasting.Init();
            objectRenderer.Init();
            Config.WallScale = Config.GameResolution.X / Settings.RaysCount;
            Config.DeltaAngle = Config.FOV / Settings.RaysCount;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(menuBase, states);
            foreach (RectangleShape setting in settingBases)
                target.Draw(setting, states);
            foreach (Text settingText in settingTexts)
                target.Draw(settingText, states);
            for (int i = 0; i < Config.NumberOfSettings; i++)
                for (int j = 0; j < Config.NumberOfStatesSettings[i]; j++)
                {
                    target.Draw(settingButtons[i][j], states);
                    target.Draw(settingButtonTexts[i][j], states);
                }
            for (int i = 0; i < Config.NumberOfSettings; i++)
                if (Config.IsScrollerSettings[i])
                {
                    target.Draw(scrollerBases[i], states);
                    target.Draw(settingButtonTexts[i][0], states);
                }
        }
    }
}
