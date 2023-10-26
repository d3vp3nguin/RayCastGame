using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RaycastGame
{
    public class Player : Drawable
    {
        private Vector2f position;
        public Vector2f Position { get { return position; } }


        private Vector2i positionMap;
        public Vector2i PositionMap { get { return positionMap; } }


        private float angle;
        public float Angle { get { return angle; } }


        private Map map;
        private FloatRect rect;

        private CircleShape playerMapCircle;

        private bool isCollisionDetected = false;
        private bool isPlayerWalk = false;
        public bool IsCollisionDetected { get { return isCollisionDetected; } }
        public bool IsPlayerWalk { get { return isPlayerWalk; } }

        public Player(Map map)
        {
            position = map.PlayerStartPos;
            angle = map.PlayerStartRotation;
            rect = map.MapShapes[0, 0].GetGlobalBounds();
            this.map = map;

            CalculatePositionMapAndCenter();

            playerMapCircle = new CircleShape(Config.PlayerMapRadius);
            playerMapCircle.FillColor = Config.PlayerMapColor;
            playerMapCircle.Origin = new Vector2f(Config.PlayerMapRadius, Config.PlayerMapRadius);
        }

        public void Update(float deltaTime)
        {
            MovementAndRotation(deltaTime);
            CalculatePositionMapAndCenter();
        }

        private void CalculatePositionMapAndCenter()
        {
            positionMap = new Vector2i((int)Math.Floor((position.X - Config.MapOffset.X) / rect.Width), 
                                       (int)Math.Floor((position.Y - Config.MapOffset.Y) / rect.Height));
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
            {
                deltaPos = MyMath.Norm(deltaPos) * Config.PlayerSpeed * deltaTime;
                isPlayerWalk = true;
            }
            else
                isPlayerWalk = false;

            position += deltaPos;
            CheckForCollision();

            playerMapCircle.Position = position;

            float dx = Mouse.GetPosition().X - Config.GameResolution.X / 2;
            Mouse.SetPosition(new Vector2i(Config.GameResolution.X / 2, Config.GameResolution.Y / 2));
            angle += dx * Settings.MouseSensativity * deltaTime;

            if (angle < 0) angle += (float)Math.Tau;
            if (angle > Math.Tau) angle -= (float)Math.Tau;
        }

        private void CheckForCollision()
        {
            isCollisionDetected = false;
            for (int y = 0; y < map.MapWallBase.GetLength(0); y++)
                for (int x = 0; x < map.MapWallBase.GetLength(1); x++)
                    if (map.MapWallBase[y, x] != 0)
                        CheckForWallCollision(map.MapShapes[y, x].GetGlobalBounds());
        }

        private void CheckForWallCollision(FloatRect rect)
        {
            Vector2f nearWallPoint = new Vector2f(MyMath.Clamp(position.X, rect.Left - Config.WallOutlineThickness, rect.Left + rect.Width + Config.WallOutlineThickness), 
                                                  MyMath.Clamp(position.Y, rect.Top - Config.WallOutlineThickness, rect.Top + rect.Height + Config.WallOutlineThickness));

            Vector2f playerToPoint = position - nearWallPoint;
            if (MyMath.Length(playerToPoint) < Config.PlayerMapRadius)
                position = nearWallPoint + MyMath.Norm(playerToPoint) * Config.PlayerMapRadius;

            isCollisionDetected = true;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(playerMapCircle, states);
        }
    }
}
