﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RaycastGame
{
    public class Player : Drawable
    {
        private Vector2f position;
        public Vector2f Position { get { return position; } }


        private Vector2f positionCenter;
        public Vector2f PositionCenter { get { return positionCenter; } }


        private Vector2i positionMap;
        public Vector2i PositionMap { get { return positionMap; } }


        private float angle;
        public float Angle { get { return angle; } }


        private Map map;

        private CircleShape playerMapCircle;

        private bool isCollisionDetected = false;

        private int dxMouse;

        public Player(Map map)
        {
            position = map.PlayerStartPos + new Vector2f(Settings.PlayerMapRadius, Settings.PlayerMapRadius) + Settings.MapOffset;
            angle = map.PlayerStartRotation;
            this.map = map;

            dxMouse = Mouse.GetPosition().X;

            CalculatePositionMapAndCenter();

            playerMapCircle = new CircleShape(Settings.PlayerMapRadius);
            playerMapCircle.FillColor = Settings.PlayerMapColor;
        }

        public void Update(float deltaTime)
        {
            MovementAndRotation(deltaTime);
            CalculatePositionMapAndCenter();
        }

        private void CalculatePositionMapAndCenter()
        {
            FloatRect rect = map.MapShapes[0, 0].GetGlobalBounds();
            positionMap = new Vector2i((int)Math.Floor((position.X + Settings.PlayerMapRadius - Settings.MapOffset.X) / rect.Width), 
                                       (int)Math.Floor((position.Y + Settings.PlayerMapRadius - Settings.MapOffset.Y) / rect.Height));
            positionCenter = position + new Vector2f(Settings.PlayerMapRadius, Settings.PlayerMapRadius);
        }

        private void MovementAndRotation(float deltaTime)
        {
            float sin_a = (float)Math.Sin(angle);
            float cos_a = (float)Math.Cos(angle);

            Vector2f deltaPos = new Vector2f(0f, 0f);
            if (Keyboard.IsKeyPressed(Settings.ForwardKey)) deltaPos += new Vector2f(cos_a, sin_a);
            if (Keyboard.IsKeyPressed(Settings.BackwardKey)) deltaPos += new Vector2f(-cos_a, -sin_a);
            if (Keyboard.IsKeyPressed(Settings.LeftKey)) deltaPos += new Vector2f(sin_a, -cos_a);
            if (Keyboard.IsKeyPressed(Settings.RightKey)) deltaPos += new Vector2f(-sin_a, cos_a);
            
            if (MyMath.Length(deltaPos) != 0)
                deltaPos = MyMath.Norm(deltaPos) * Settings.PlayerSpeed * deltaTime;

            position += deltaPos;
            CheckForCollision();

            playerMapCircle.Position = position;

            Vector2i mousePos = Mouse.GetPosition();
            if (mousePos.X < Settings.MouseLeftBorder || mousePos.X > Settings.MouseRightBorder)
                Mouse.SetPosition(new Vector2i(Settings.GameResolution.X / 2, Settings.GameResolution.Y / 2));
            float rel = mousePos.X - dxMouse;
            rel = MyMath.Clamp(rel, -Settings.MouseMaxRel, Settings.MouseMaxRel);
            angle += rel * Settings.MouseSensativity * deltaTime;
            dxMouse = mousePos.X;

            if (angle < 0) angle += (float)Math.Tau;
            else if (angle > Math.Tau) angle -= (float)Math.Tau;
        }

        private void CheckForCollision()
        {
            isCollisionDetected = false;
            for (int y = 0; y < map.MapBase.GetLength(0); y++)
                for (int x = 0; x < map.MapBase.GetLength(1); x++)
                    if (map.MapBase[y, x] != 0)
                        CheckForWallCollision(map.MapShapes[y, x].GetGlobalBounds());
        }

        private void CheckForWallCollision(FloatRect rect)
        {
            Vector2f posPlayer = position + new Vector2f(Settings.PlayerMapRadius, Settings.PlayerMapRadius);

            Vector2f nearWallPoint = new Vector2f(MyMath.Clamp(posPlayer.X, rect.Left - Settings.WallOutlineThickness, rect.Left + rect.Width + Settings.WallOutlineThickness), 
                                                  MyMath.Clamp(posPlayer.Y, rect.Top - Settings.WallOutlineThickness, rect.Top + rect.Height + Settings.WallOutlineThickness));

            Vector2f playerToPoint = posPlayer - nearWallPoint;
            if (MyMath.Length(playerToPoint) < Settings.PlayerMapRadius)
                position = nearWallPoint + MyMath.Norm(playerToPoint) * Settings.PlayerMapRadius - new Vector2f(Settings.PlayerMapRadius, Settings.PlayerMapRadius);

            isCollisionDetected = true;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(playerMapCircle, states);
        }
    }
}
