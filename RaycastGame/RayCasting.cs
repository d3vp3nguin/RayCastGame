using SFML.Graphics;
using SFML.System;

namespace RaycastGame
{
    public class RayCasting : Drawable
    {
        private List<Vertex[]> rays;


        private List<float> projectionHeights;
        public List<float> ProjectionHeights { get { return projectionHeights; } }


        private List<Color> projectionColor;
        public List<Color> ProjectionColor { get { return projectionColor; } }


        private Player player;
        private Map map;

        public RayCasting(Player player, Map map)
        {
            this.player = player;
            this.map = map;
            rays = new List<Vertex[]>(Settings.RaysCount);
            projectionHeights = new List<float>(Settings.RaysCount);
            projectionColor = new List<Color>(Settings.RaysCount);
            for (int i = 0; i < Settings.RaysCount; i++)
            {
                rays.Add(new Vertex[2]);
                rays[i][0].Color = Settings.PlayerLineMapColor;
                rays[i][1].Color = Settings.PlayerLineMapColor;
            }
        }

        public void RayCast()
        {
            projectionHeights.Clear();
            projectionColor.Clear();

            FloatRect rect = map.MapShapes[0, 0].GetGlobalBounds();

            float rayAngle = player.Rotation - Settings.FOVHalf;

            for (int idxRay = 0; idxRay < Settings.RaysCount; idxRay++)
            {
                rays[idxRay][0].Position = player.PositionCenter;
                rays[idxRay][1].Position = player.PositionCenter;

                float sin_angle = (float)Math.Sin(rayAngle);
                float cos_angle = (float)Math.Cos(rayAngle);
                float tan_angle = sin_angle / cos_angle;
                float ctan_angle = cos_angle / sin_angle;

                Vector2i posMapCalc = player.PositionMap;
                Vector2i posMapCheck1 = new Vector2i(0, 0), posMapCheck2 = new Vector2i(0, 0);

                int depthX = 0, depthY = 0;
                float dx1 = 0f, dy1 = 0f;
                float dx2 = 0f, dy2 = 0f;

                bool flagX = true, flagY = true;
                int modX = cos_angle >= 0 ? 1 : -1;
                int modY = sin_angle >= 0 ? 1 : -1;

                while (flagX)
                {
                    depthX++;
                    dx1 = cos_angle >= 0f ? (posMapCalc.X + 1) * rect.Width + Settings.DeltaDetection + Settings.MapOffset.X: posMapCalc.X * rect.Width - Settings.DeltaDetection + Settings.MapOffset.X;
                    dy1 = tan_angle * dx1 + (player.PositionCenter.Y - tan_angle * player.PositionCenter.X);

                    posMapCheck1 = new Vector2i((int)Math.Floor((dx1 - Settings.MapOffset.X)/ (rect.Width)),
                                                (int)Math.Floor((dy1 - Settings.MapOffset.Y) / (rect.Height)));

                    if (posMapCheck1.X < 0 || posMapCheck1.X >= map.MapBase.GetLength(1) ||
                    posMapCheck1.Y < 0 || posMapCheck1.Y >= map.MapBase.GetLength(0)) break;

                    if (map.MapBase[posMapCheck1.Y, posMapCheck1.X] != 0) flagX = false;

                    posMapCalc.X += modX;
                }

                while (flagY)
                {
                    depthY++;
                    dy2 = sin_angle >= 0f ? (posMapCalc.Y + 1) * rect.Height + Settings.DeltaDetection + Settings.MapOffset.Y : posMapCalc.Y * rect.Height - Settings.DeltaDetection + Settings.MapOffset.Y;
                    dx2 = (dy2 - (player.PositionCenter.Y - tan_angle * player.PositionCenter.X)) * ctan_angle;

                    posMapCheck2 = new Vector2i((int)Math.Floor((dx2 - Settings.MapOffset.X) / (rect.Width)),
                                                (int)Math.Floor((dy2 - Settings.MapOffset.Y) / (rect.Height)));

                    if (posMapCheck2.X < 0 || posMapCheck2.X >= map.MapBase.GetLength(1) ||
                    posMapCheck2.Y < 0 || posMapCheck2.Y >= map.MapBase.GetLength(0)) break;

                    if (map.MapBase[posMapCheck2.Y, posMapCheck2.X] != 0) flagY = false;

                    posMapCalc.Y += modY;
                }

                float depth = 0f;
                float dx = 0f, dy = 0f;
                if (MyMath.Length(new Vector2f(dx1 - player.PositionCenter.X, dy1 - player.PositionCenter.Y)) <=
                MyMath.Length(new Vector2f(dx2 - player.PositionCenter.X, dy2 - player.PositionCenter.Y)))
                {
                    dx = dx1;
                    dy = dy1;
                }
                else
                {
                    dx = dx2;
                    dy = dy2;
                }
                depth = MyMath.Length(new Vector2f(dx - player.PositionCenter.X, dy - player.PositionCenter.Y)) * (float)Math.Cos(player.Rotation - rayAngle);
                rays[idxRay][0].Position = player.PositionCenter + MyMath.Norm(new Vector2f(dx, dy) - player.PositionCenter) * (Settings.PlayerMapRadius + Settings.DopOffsetrayCast);
                rays[idxRay][1].Position = new Vector2f(dx, dy);

                projectionHeights.Add(Settings.ScreenDistance / (depth / (rect.Width / 2f)));
                byte c = (byte)(255 * Math.Pow((Settings.MaxDepth - depth) / Settings.MaxDepth, 1.5));
                projectionColor.Add(new Color(c, c, c, 255));

                rayAngle += Settings.DeltaAngle;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Vertex[] ray in rays)
                target.Draw(ray, PrimitiveType.LineStrip, states);
        }
    }
}
