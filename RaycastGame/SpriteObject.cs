using SFML.Graphics;
using SFML.System;

namespace RaycastGame
{
    public class SpriteObject
    {
        private RectangleShape probRect;
        public RectangleShape ProbRect { get { return probRect; } }


        private bool isOnScreen = false;
        public bool IsOnScreen { get { return isOnScreen; } }


        private float depth = 0f;
        public float Depth { get { return depth; } }


        private int numberProb;
        private Texture probTexture;
        private float probTextureRatio;

        private Vector2f postion;

        public SpriteObject(int numberProb, Vector2f position)
        {
            this.numberProb = numberProb;
            this.postion = position;

            probTexture = GetProbTexture();
            probTextureRatio = probTexture.Size.X / probTexture.Size.Y;

            probRect = new RectangleShape(new Vector2f(probTexture.Size.X, probTexture.Size.Y));
            probRect.FillColor = Color.White;
            probRect.Texture = probTexture;
        }

        public void Update(Player player, Map map)
        {
            isOnScreen = false;

            float dx = postion.X - player.Position.X;
            float dy = postion.Y - player.Position.Y;
            float theta = (float)Math.Atan2(dy, dx);

            float delta = theta - player.Angle;
            if ((dx > 0 && player.Angle > Math.PI) || (dx < 0 && dy < 0)) delta += (float)Math.Tau;

            float deltaRays = delta / Settings.DeltaAngle;
            float screenX = (Settings.RaysCountHalf + deltaRays) * Settings.WallScale;

            float dist = MyMath.Hypot(dx, dy);
            depth = dist * (float)Math.Cos(delta);

            if ((-probRect.Size.X / 2 < screenX) && (screenX < (Settings.GameResolution.X + probRect.Size.X / 2)) &&
                depth > Settings.PlayerMapRadius)
                isOnScreen = true;

            if (!isOnScreen) return;

            FloatRect rect = map.MapShapes[0, 0].GetGlobalBounds();
            float proj = Settings.ScreenDistance / depth / rect.Width * Settings.ProbeScale;
            Vector2f projSize = new Vector2f(proj * probTextureRatio, proj);

            probRect.Size = projSize;
            probRect.Origin = new Vector2f(projSize.X / 2, projSize.Y);
            probRect.Position = new Vector2f(screenX, Settings.GameResolution.Y / 2 + projSize.Y);
        }

        private Texture GetProbTexture()
        {
            return new Texture(Settings.TextureProbPath + numberProb.ToString() + ".png");
        }
    }
}
