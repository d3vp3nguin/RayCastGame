﻿using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace RaycastGame
{
    public static class Settings
    {
        // BASE SETTINGS
        private static Vector2i gameResolution = new Vector2i(1920, 1080);
        public static Vector2i GameResolution { get { return gameResolution; } }


        private static float resolutionRatio = gameResolution.X / gameResolution.Y;
        public static float ResolutionRatio { get { return resolutionRatio; } }


        private static Styles gameStyle = Styles.Fullscreen;
        public static Styles GameStyle { get { return gameStyle; } }


        private static string gameTitle = "RaycastGame";
        public static string GameTitle { get { return gameTitle; } }


        // MAP SETTINGS
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


        // PLAYER SETTINGS
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


        private static float playerMapLineLength = 200f;
        public static float PlayerMapLineLength { get { return playerMapLineLength; } }


        private static float mouseSensativity = 0.3f;
        public static float MouseSensativity { get { return mouseSensativity; } }


        // RAYCAST SETTINGS
        private static float fov = (float)Math.PI / 3f;
        private static float fovHalf = fov / 2;
        public static float FOV { get { return fov; } }
        public static float FOVHalf { get { return fovHalf; } }

        private static int raysCount = (int)gameResolution.X / 1;
        private static int raysCountHalf = raysCount / 2;
        public static int RaysCount { get { return raysCount; } }
        public static int RaysCountHalf { get { return raysCountHalf; } }


        private static float deltaAngle = fov / raysCount;
        public static float DeltaAngle { get { return deltaAngle; } }


        private static int maxDepth = 20;
        public static int MaxDepth { get { return maxDepth; } }


        private static float deltaDetection = 0.001f;
        public static float DeltaDetection { get { return deltaDetection; } }


        private static float dopOffsetrayCast = 3f;
        public static float DopOffsetRayCast { get { return dopOffsetrayCast; } }


        private static float screenDistance = (gameResolution.X / 2) / (float)Math.Tan(fovHalf);
        public static float ScreenDistance { get { return screenDistance; } }


        private static float wallScale = gameResolution.X / raysCount;
        public static float WallScale { get { return wallScale; } }


        // TEXTURING SETTINGS
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


        // INPUT KEYS SETTINGS
        private static Keyboard.Key forwardKey = Keyboard.Key.W;
        private static Keyboard.Key backwardKey = Keyboard.Key.S;
        private static Keyboard.Key leftKey = Keyboard.Key.A;
        private static Keyboard.Key rightKey = Keyboard.Key.D;
        public static Keyboard.Key ForwardKey { get { return forwardKey; } }
        public static Keyboard.Key BackwardKey { get { return backwardKey; } }
        public static Keyboard.Key LeftKey { get { return leftKey; } }
        public static Keyboard.Key RightKey { get { return rightKey; } }


        // DEV INFO SETTINGS
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
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       