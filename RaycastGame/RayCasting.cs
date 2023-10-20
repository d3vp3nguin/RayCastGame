using SFML.Graphics;
using SFML.System;

namespace RaycastGame
{
    public class RayCasting : Drawable
    {
        private List<Vertex[]> rays;

        private float[] projectionHeights;
        private Color[] projectionColor;
        private Vector2i[] posMap;
        private float[] offset;

        private RayCastResult rayCastRes;
        public RayCastResult RayCastRes { get { return rayCastRes; } }


        private Player player;
        private Map map;

        public RayCasting(Player player, Map map)
        {
            this.player = player;
            this.map = map;

            projectionHeights = new float[Settings.RaysCount];
            projectionColor = new Color[Settings.RaysCount];
            posMap = new Vector2i[Settings.RaysCount];
            offset = new float[Settings.RaysCount];

            rayCastRes = new RayCastResult();
            rays = new List<Vertex[]>(Settings.RaysCount);
            for (int i = 0; i < Settings.RaysCount; i++)
            {
                rays.Add(new Vertex[2]);
                rays[i][0].Color = Settings.PlayerLineMapColor;
                rays[i][1].Color = Settings.PlayerLineMapColor;
            }
        }

        public void RayCast()
        {
            FloatRect rect = map.MapShapes[0, 0].GetGlobalBounds();
            float rayAngle = player.Rotation - Settings.FOVHalf;

            for (int idxRay = 0; idxRay < Settings.RaysCount; idxRay++)
            {
                float sin_angle = (float)Math.Sin(rayAngle);
                float cos_angle = (float)Math.Cos(rayAngle);
                float tan_angle = sin_angle / cos_angle;

                Vector2i posMapCalc = player.PositionMap;
                Vector2i posMapCheck1 = new Vector2i(0, 0), posMapCheck2 = new Vector2i(0, 0);

                Vector2f dxy1 = new Vector2f();
                Vector2f dxy2 = new Vector2f();

                bool flagX = true, flagY = true;
                int modX = cos_angle >= 0 ? 1 : -1;
                int modY = sin_angle >= 0 ? 1 : -1;

                while (flagX)
                {
                    dxy1.X = cos_angle >= 0f ? (posMapCalc.X + 1) * rect.Width + Settings.DeltaDetection + Settings.MapOffset.X: posMapCalc.X * rect.Width - Settings.DeltaDetection + Settings.MapOffset.X;
                    dxy1.Y = tan_angle * dxy1.X + (player.PositionCenter.Y - tan_angle * player.PositionCenter.X);

                    posMapCheck1 = new Vector2i((int)Math.Floor((dxy1.X - Settings.MapOffset.X)/ (rect.Width)),
                                                (int)Math.Floor((dxy1.Y - Settings.MapOffset.Y) / (rect.Height)));

                    if (posMapCheck1.X < 0 || posMapCheck1.X >= map.MapBase.GetLength(1) ||
                    posMapCheck1.Y < 0 || posMapCheck1.Y >= map.MapBase.GetLength(0)) break;

                    if (map.MapBase[posMapCheck1.Y, posMapCheck1.X] != 0) flagX = false;

                    posMapCalc.X += modX;
                }

                while (flagY)
                {
                    dxy2.Y = sin_angle >= 0f ? (posMapCalc.Y + 1) * rect.Height + Settings.DeltaDetection + Settings.MapOffset.Y : posMapCalc.Y * rect.Height - Settings.DeltaDetection + Settings.MapOffset.Y;
                    dxy2.X = (dxy2.Y - (player.PositionCenter.Y - tan_angle * player.PositionCenter.X)) / tan_angle;

                    posMapCheck2 = new Vector2i((int)Math.Floor((dxy2.X - Settings.MapOffset.X) / (rect.Width)),
                                                (int)Math.Floor((dxy2.Y - Settings.MapOffset.Y) / (rect.Height)));

                    if (posMapCheck2.X < 0 || posMapCheck2.X >= map.MapBase.GetLength(1) ||
                    posMapCheck2.Y < 0 || posMapCheck2.Y >= map.MapBase.GetLength(0)) break;

                    if (map.MapBase[posMapCheck2.Y, posMapCheck2.X] != 0) flagY = false;

                    posMapCalc.Y += modY;
                }

                Vector2f dxy;
                float depth;
                float off;
                float len1 = MyMath.Length(dxy1 - player.PositionCenter);
                float len2 = MyMath.Length(dxy2 - player.PositionCenter);
                if (len1 <= len2)
                {
                    dxy = dxy1;
                    posMap[idxRay] = posMapCheck1;
                    off = (dxy.Y - Settings.MapOffset.Y) % rect.Height / rect.Height;
                    offset[idxRay] = cos_angle > 0 ? off : (1 - off);
                    depth = len1 * (float)Math.Cos(player.Rotation - rayAngle);
                }
                else
                {
                    dxy = dxy2;
                    posMap[idxRay] = posMapCheck2;
                    off = (dxy.X - Settings.MapOffset.X) % rect.Width / rect.Width;
                    offset[idxRay] = sin_angle > 0 ? (1 - off) : off;
                    depth = len2 * (float)Math.Cos(player.Rotation - rayAngle);
                }

                rays[idxRay][0].Position = player.PositionCenter + MyMath.Norm(dxy - player.PositionCenter) * (Settings.PlayerMapRadius + Settings.DopOffsetrayCast);
                rays[idxRay][1].Position = dxy;

                projectionHeights[idxRay] = Settings.ScreenDistance / (depth / (rect.Width / 2f));

                byte c = (byte)(255 * Math.Pow((Settings.MaxDepth - depth) / Settings.MaxDepth, 1.5));
                projectionColor[idxRay] = new Color(c, c, c, 255);

                rayAngle += Settings.DeltaAngle;
            }
            rayCastRes = new RayCastResult(projectionHeights, projectionColor, posMap, offset);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Vertex[] ray in rays)
                target.Draw(ray, PrimitiveType.LineStrip, states);
        }
    }
}
