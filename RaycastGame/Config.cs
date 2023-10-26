﻿using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace RaycastGame
{
    public static class Config
    {
        // BASE CONFIG
        private static Vector2i gameResolution = new Vector2i((int)VideoMode.DesktopMode.Width, (int)VideoMode.DesktopMode.Height);
        public static Vector2i GameResolution { get { return gameResolution; } }


        private static float resolutionRatio = gameResolution.X / gameResolution.Y;
        public static float ResolutionRatio { get { return resolutionRatio; } }


        private static Styles gameStyle = Styles.Fullscreen;
        public static Styles GameStyle { get { return gameStyle; } }


        private static string gameTitle = "Raycast Game [C# SFML]";
        public static string GameTitle { get { return gameTitle; } }


        // MAP CONFIG
        private static Color wallYesFillColor = Color.Black;
        private static Color wallYesOutlineColor = Color.White;
        private static Color wallNoFillColor = Color.Black;
        private static Color wallNoOutlineColor = Color.Black;
        public static Color WallYesFillColor { get { return wallYesFillColor; } }
        public static Color WallYesOutlineColor { get { return wallYesOutlineColor; } }
        public static Color WallNoFillColor { get { return wallNoFillColor; } }
        public static Color WallNoOutlineColor { get { return wallNoOutlineColor; } }


        private static float wallOutlineThickness = 1f;
        public static float WallOutlineThickness { get { return wallOutlineThickness; } }


        private static Vector2f mapSize = new Vector2f(480f, 270f);
        public static Vector2f MapSize { get { return mapSize; } }


        private static float mapRatio = mapSize.X / gameResolution.X;
        public static float MapRatio { get { return mapRatio; } }


        private static Vector2f mapOffset = new Vector2f(10f, 10f);
        public static Vector2f MapOffset { get { return mapOffset; } }


        // PLAYER CONFIG
        private static float playerSpeed = 300f * mapRatio;
        public static float PlayerSpeed { get { return playerSpeed; } }


        private static float playerRotationSpeed = 6f;
        public static float PlayerRotationSpeed { get { return playerRotationSpeed; } }


        private static float playerMapRadius = 20f * mapRatio;
        public static float PlayerMapRadius { get { return playerMapRadius; } }


        private static Color playerMapColor = Color.Green;
        public static Color PlayerMapColor { get { return playerMapColor; } }


        private static Color playerLineMapColor = Color.Yellow;
        public static Color PlayerLineMapColor { get { return playerLineMapColor; } }


        // RAYCAST CONFIG
        private static float fov = (float)Math.PI / 3f;
        private static float fovHalf = fov / 2;
        public static float FOV { get { return fov; } }
        public static float FOVHalf { get { return fovHalf; } }


        private static float deltaAngle = fov / Settings.RaysCount;
        public static float DeltaAngle { get { return deltaAngle; } set { deltaAngle = fov / Settings.RaysCount; } }


        private static int maxDepth = 20;
        public static int MaxDepth { get { return maxDepth; } }


        private static float deltaDetection = 0.001f;
        public static float DeltaDetection { get { return deltaDetection; } }


        private static float dopOffsetrayCast = 3f;
        public static float DopOffsetRayCast { get { return dopOffsetrayCast; } }


        private static float screenDistance = (gameResolution.X / 2) / (float)Math.Tan(fovHalf);
        public static float ScreenDistance { get { return screenDistance; } }


        private static float wallScale = gameResolution.X / Settings.RaysCount;
        public static float WallScale { get { return wallScale; } set { wallScale = gameResolution.X / Settings.RaysCount; } }


        // TEXTURING CONFIG
        private static Vector2i textureWallSize = new Vector2i(512, 512);
        private static Vector2i textureWallSizeHalf = textureWallSize / 2;
        public static Vector2i TextureWallSize { get { return textureWallSize; } }
        public static Vector2i TextureWallSizeHalf { get { return textureWallSizeHalf; } }


        private static Vector2i textureSkySize = new Vector2i(2048, 512);
        public static Vector2i TextureSkySize { get { return textureSkySize; } }


        private static string textureWallPath = "Assets\\Textures\\Walls\\Wall";
        public static string TextureWallPath { get { return textureWallPath; } }


        private static string textureSkyPath = "Assets\\Textures\\Sky\\Sky.png";
        public static string TextureSkyPath { get { return textureSkyPath; } }


        private static int wallTextureNumber = 3;
        public static int WallTextureNumber { get { return wallTextureNumber; } }


        private static Color floorBrightColor = new Color(33, 33, 33, 255);
        private static Color floorDarkColor = new Color(3, 3, 3, 255);
        public static Color FloorBrightColor { get { return floorBrightColor; } }
        public static Color FloorDarkColor { get { return floorDarkColor; } }


        private static string textureProbPath = "Assets\\Sprites\\Probs";
        public static string TextureProbPath { get { return textureProbPath; } }


        private static float probeScale = 500f;
        public static float ProbeScale { get { return probeScale; } }


        // AUDIO CONFIG
        private static string musicPath = "Assets\\Audio\\Music\\Music1.wav";
        private static string stepSoundPath = "Assets\\Audio\\Sound\\Step.wav";
        public static string MusicPath { get { return musicPath; } }
        public static string StepSoundPath { get { return stepSoundPath; } }


        private static float stepTime = 0.5f;
        public static float StepTime { get { return stepTime; } }


        // DEV INFO CONFIG
        private static float devTextScale = 1f;
        public static float DevTextScale { get { return devTextScale; } }


        private static Color devTextFillColor = Color.White;
        private static Color devTextOutlineColor = Color.Black;
        public static Color DevTextFillColor { get { return devTextFillColor; } }
        public static Color DevTextOutlineColor { get { return devTextOutlineColor; } }


        private static float devTextThickness = 2f;
        public static float DevTextThickness { get { return devTextThickness; } }


        private static UInt32 devTextCharacterSize = 32;
        public static UInt32 DevTextCharacterSize { get { return devTextCharacterSize; } }


        private static float devTextFPSX = 10f;
        private static float devTextFPSY = gameResolution.Y - 130f;
        public static float DevTextFPSX { get { return devTextFPSX; } }
        public static float DevTextFPSY { get { return devTextFPSY; } }


        private static float devTextPositionX = 10f;
        private static float devTextPositionY = gameResolution.Y - 100f;
        public static float DevTextPositionX { get { return devTextPositionX; } }
        public static float DevTextPositionY { get { return devTextPositionY; } }


        private static float devTextPositionMapX = 10f;
        private static float devTextPositionMapY = gameResolution.Y - 70f;
        public static float DevTextPositionMapX { get { return devTextPositionMapX; } }
        public static float DevTextPositionMapY { get { return devTextPositionMapY; } }


        private static float devTextRotationX = 10f;
        private static float devTextRotationY = gameResolution.Y - 40f;
        public static float DevTextRotationX { get { return devTextRotationX; } }
        public static float DevTextRotationY { get { return devTextRotationY; } }


        private static string devTextFontPath = "Assets\\Fonts\\2a031.ttf";
        public static string DevTextFontPath { get { return devTextFontPath; } }


        // UI CONFIG
        private static Vector2f menuSize = new Vector2f(gameResolution.X / 3, gameResolution.Y / 2);
        public static Vector2f MenuSize { get { return menuSize; } }


        private static Color menuBaseColor = new Color(33, 33, 33, 192);
        private static Color menuParamColor = new Color(22, 22, 22, 192);
        private static Color menuButtonOffColor = new Color(33, 33, 33, 192);
        private static Color menuButtonOnColor = new Color(55, 55, 55, 192);
        private static Color menuTransparentColor = new Color(0, 0, 0, 0);
        public static Color MenuBaseColor { get { return menuBaseColor; } }
        public static Color MenuParamColor { get { return menuParamColor; } }
        public static Color MenuButtonOffColor { get { return menuButtonOffColor; } }
        public static Color MenuButtonOnColor { get { return menuButtonOnColor; } }
        public static Color MenuTransparentColor { get { return menuTransparentColor; } }


        private static float menuTextScale = 1f;
        public static float MenuTextScale { get { return menuTextScale; } }


        private static Color menuTextFillColor = Color.White;
        private static Color menuTextOutlineColor = Color.Black;
        public static Color MenuTextFillColor { get { return menuTextFillColor; } }
        public static Color MenuTextOutlineColor { get { return menuTextOutlineColor; } }


        private static float menuTextThickness = 0f;
        public static float MenuTextThickness { get { return menuTextThickness; } }


        private static UInt32 menuTextCharacterSize = 32;
        public static UInt32 MenuTextCharacterSize { get { return menuTextCharacterSize; } }


        private static string menuTextFontPath = "Assets\\Fonts\\2a031.ttf";
        public static string MenuTextFontPath { get { return menuTextFontPath; } }


        private static string[] nameOfSettings = { "Rays Count", "Mouse Sensativity", "Walk Buttons", "Volume", "" };
        public static string[] NameOfSettings { get { return nameOfSettings; } }


        private static int numberOfSettings = nameOfSettings.Length;
        public static int NumberOfSettings { get { return numberOfSettings; } }


        private static string[][] nameOfStatesSettings = { new string[] { "X / 1", "X / 2", "X / 4", "X / 5" }, new string[] { "" }, new string[] { "WASD", "Arrows" }, new string[] { "" }, new string[] { "Quit" } };
        public static string[][] NameOfStatesSettings { get { return nameOfStatesSettings; } }


        private static int[] numberOfStatesSettings = { 4, 1, 2, 1, 1 };
        public static int[] NumberOfStatesSettings { get { return numberOfStatesSettings; } }


        private static bool[] isAllButtonSettings = { false, false, false, false, true };
        public static bool[] IsAllButtonSettings { get { return isAllButtonSettings; } }


        private static bool[] isButtonSettings = { true, false, true, false, true };
        public static bool[] IsButtonSettings { get { return isButtonSettings; } }


        private static bool[] isScrollerSettings = { false, true, false, true, false };
        public static bool[] IsScrollerSettings { get { return isScrollerSettings; } }


        private static float menuParamOffset = 20f;
        public static float MenuParamOffset { get { return menuParamOffset; } }


        private static int[] rayCountParam = { gameResolution.X / 1, gameResolution.X / 2, gameResolution.X / 4, gameResolution.X / 5, };
        public static int[] RayCountParam { get { return rayCountParam; } }


        private static Keyboard.Key[] keyForwardParam = { Keyboard.Key.W, Keyboard.Key.Up };
        public static Keyboard.Key[] KeyForwardParam { get { return keyForwardParam; } }


        private static float minMouseSensativity = 0.001f;
        private static float maxMouseSensativity = 5f;
        public static float MinMouseSensativity { get { return minMouseSensativity; } }
        public static float MaxMouseSensativity { get { return maxMouseSensativity; } }


        private static float minVolume = 0f;
        private static float maxVolume = 100f;
        public static float MinVolume { get { return minVolume; } }
        public static float MaxVolume { get { return maxVolume; } }


        private static float[] scrollerSettings = { 0f, Settings.MouseSensativity, 0f, Settings.Volume, 0f };
        public static float[] ScrollerSettings { get { return scrollerSettings; } }


        private static float[] scrollerMinSettings = { 0f, minMouseSensativity, 0f, minVolume, 0f };
        public static float[] ScrollerMinSettings { get { return scrollerMinSettings; } }


        private static float[] scrollerMaxSettings = { 0f, maxMouseSensativity, 0f, maxVolume, 0f };
        public static float[] ScrollerMaxSettings { get { return scrollerMaxSettings; } }
    }
}
