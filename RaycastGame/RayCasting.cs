using SFML.Graphics;
using SFML.System;

namespace RaycastGame
{
    public class RayCasting : Drawable
    {
        private List<Vertex[]> rays;

        private float[] projectionHeights;
        private Color[] projectionColor;
        private int[] numTexture;
        private float[] offset;
        private float[] depth;

        private RayCastResult rayCastRes;
        public RayCastResult RayCastRes { get { return rayCastRes; } }


        private Player player;
        private Map map;

        public RayCasting(Player player, Map map)
        {
            this.player = player;
            this.map = map;

            Init();
        }


        public void Init()
        {
            projectionHeights = new float[Settings.RaysCount];
            projectionColor = new Color[Settings.RaysCount];
            numTexture = new int[Settings.RaysCount];
            offset = new float[Settings.RaysCount];
            depth = new float[Settings.RaysCount];

            rayCastRes = new RayCastResult();
            rays = new List<Vertex[]>(Settings.RaysCount);
            for (int i = 0; i < Settings.RaysCount; i++)
            {
                rays.Add(new Vertex[2]);
                rays[i][0].Color = Config.PlayerLineMapColor;
                rays[i][1].Color = Config.PlayerLineMapColor;
            }
        }


        public void RayCast()
        {
            FloatRect rect = map.MapShapes[0, 0].GetGlobalBounds();
            Vector2f posPlayer = player.Position - Config.MapOffset;

            float rayAngle = player.Angle - Config.FOVHalf;
            for (int idxRay = 0; idxRay < Settings.RaysCount; idxRay++)
            {
                float sin_angle = (float)Math.Sin(rayAngle);
                float cos_angle = (float)Math.Cos(rayAngle);

                // HORIZONTAL

                float yHor = sin_angle > 0 ? (player.PositionMap.Y + 1) * rect.Height : player.PositionMap.Y * rect.Height - Config.DeltaDetection;
                float dy = sin_angle > 0 ? rect.Height : -rect.Height;

                float depthHor = (yHor - posPlayer.Y) / sin_angle;
                float xHor = posPlayer.X + depthHor * cos_angle;

                float deltaDepth = dy / sin_angle;
                float dx = deltaDepth * cos_angle;

                int textureHor = 0;
                for (int i = 0; i < Config.MaxDepth; i++)
                {
                    Vector2i tileHor = new Vector2i((int)(xHor / rect.Width), (int)(yHor / rect.Height));
                    if (tileHor.X >= 0 && tileHor.X < map.MapWallBase.GetLength(1) &&
                        tileHor.Y >= 0 && tileHor.Y < map.MapWallBase.GetLength(0))
                        if (map.MapWallBase[tileHor.Y, tileHor.X] != 0)
                        {
                            textureHor = map.MapWallBase[tileHor.Y, tileHor.X] - 1;
                            break;
                        }
                    xHor += dx;
                    yHor += dy;
                    depthHor += deltaDepth;
                }

                // VERTICAL

                float xVert = cos_angle > 0 ? (player.PositionMap.X + 1) * rect.Width: player.PositionMap.X * rect.Width - Config.DeltaDetection;
                dx = cos_angle > 0 ? rect.Width : -rect.Width;

                float depthVert = (xVert - posPlayer.X) / cos_angle;
                float yVert = posPlayer.Y + depthVert * sin_angle;

                deltaDepth = dx / cos_angle;
                dy = deltaDepth * sin_angle;

                int textureVert = 0;
                for (int i = 0; i < Config.MaxDepth; i++)
                {
                    Vector2i tileVert = new Vector2i((int)(xVert / rect.Width), (int)(yVert / rect.Height));
                    if (tileVert.X >= 0 && tileVert.X < map.MapWallBase.GetLength(1) &&
                        tileVert.Y >= 0 && tileVert.Y < map.MapWallBase.GetLength(0))
                        if (map.MapWallBase[tileVert.Y, tileVert.X] != 0)
                        {
                            textureVert = map.MapWallBase[tileVert.Y, tileVert.X] - 1;
                            break;
                        }
                    xVert += dx;
                    yVert += dy;
                    depthVert += deltaDepth;
                }

                // COMPARISON

                if (depthVert < depthHor)
                {
                    depth[idxRay] = depthVert;
                    yVert = yVert % rect.Height / rect.Height;
                    offset[idxRay] = cos_angle > 0 ? yVert : (1 - yVert);
                    numTexture[idxRay] = textureVert;
                }
                else
                {
                    depth[idxRay] = depthHor;
                    xHor = xHor % rect.Width / rect.Width;
                    offset[idxRay] = sin_angle > 0 ? (1 - xHor) : xHor;
                    numTexture[idxRay] = textureHor;
                }

                rays[idxRay][0].Position = player.Position + new Vector2f((Config.PlayerMapRadius + Config.DopOffsetRayCast) * cos_angle, (Config.PlayerMapRadius + Config.DopOffsetRayCast) * sin_angle);
                rays[idxRay][1].Position = player.Position + new Vector2f(depth[idxRay] * cos_angle, depth[idxRay] * sin_angle);

                depth[idxRay] *= (float)Math.Cos(player.Angle - rayAngle);

                projectionHeights[idxRay] = Config.ScreenDistance / (depth[idxRay] / rect.Width + Config.DeltaDetection);
                byte c = (byte)(255 * Math.Pow((Config.MaxDepth * rect.Width - depth[idxRay]) / (Config.MaxDepth * rect.Width), 1.5));
                projectionColor[idxRay] = new Color(c, c, c, 255);

                rayAngle += Config.DeltaAngle;
            }
            rayCastRes = new RayCastResult(projectionHeights, projectionColor, numTexture, offset, depth);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Vertex[] ray in rays)
                target.Draw(ray, PrimitiveType.LineStrip, states);
        }
    }
}
