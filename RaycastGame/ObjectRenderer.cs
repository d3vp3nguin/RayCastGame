using System.Linq;
using SFML.Graphics;
using SFML.System;

namespace RaycastGame
{
    public class ObjectRenderer : Drawable
    {
        private RectangleShape[] wallRects;
        private Texture[] wallTextures;

        private RectangleShape skyRect;
        private Texture skyTexture;

        private RectangleShape[] probRects;

        private List<RectToRender> rects;

        private Vertex[] floorRect;

        private Player player;
        private RayCasting rayCasting;
        private Map map;

        public ObjectRenderer(Player player, RayCasting rayCasting, Map map)
        {
            this.player = player;
            this.rayCasting = rayCasting;
            this.map = map;

            Init();
        }

        public void Init()
        {
            wallRects = new RectangleShape[Settings.RaysCount];
            for (int i = 0; i < wallRects.Length; i++)
            {
                wallRects[i] = new RectangleShape();
                wallRects[i].FillColor = Color.White;
            }
            wallTextures = GetWallTextures();

            skyRect = new RectangleShape();
            skyRect.Position = new Vector2f(0, 0);
            skyRect.Size = new Vector2f(Config.GameResolution.X, Config.GameResolution.Y / 2);
            skyRect.FillColor = Color.White;
            skyTexture = GetSkyTexture();
            skyTexture.Repeated = true;
            skyRect.Texture = skyTexture;

            floorRect = new Vertex[4];
            floorRect[0] = new Vertex(new Vector2f(0, Config.GameResolution.Y / 2), Config.FloorDarkColor);
            floorRect[1] = new Vertex(new Vector2f(Config.GameResolution.X, Config.GameResolution.Y / 2), Config.FloorDarkColor);
            floorRect[2] = new Vertex(new Vector2f(Config.GameResolution.X, Config.GameResolution.Y), Config.FloorBrightColor);
            floorRect[3] = new Vertex(new Vector2f(0, Config.GameResolution.Y), Config.FloorBrightColor);

            probRects = new RectangleShape[map.SpriteObjects.Length];
            for (int i = 0; i < probRects.Length; i++)
                probRects[i] = map.SpriteObjects[i].ProbRect;

            rects = new List<RectToRender>();
        }

        public void Update()
        {
            rects.Clear();

            skyRect.TextureRect = new IntRect((int)(player.Angle / (float)Math.Tau * Config.TextureSkySize.X) - Config.TextureSkySize.Y / 2, 0, Config.TextureSkySize.Y, Config.TextureSkySize.Y);

            for (int i = 0; i < Settings.RaysCount; i++)
            {
                wallRects[i].FillColor = rayCasting.RayCastRes.ProjectionColor[i];
                wallRects[i].Texture = wallTextures[rayCasting.RayCastRes.NumTexture[i]];
                wallRects[i].Position = new Vector2f(i * Config.WallScale, Config.GameResolution.Y / 2 - rayCasting.RayCastRes.ProjectionHeights[i] / 2);
                wallRects[i].Size = new Vector2f(Config.WallScale, rayCasting.RayCastRes.ProjectionHeights[i]);
                wallRects[i].TextureRect = new IntRect((int)Math.Round(rayCasting.RayCastRes.Offset[i] * (Config.TextureWallSize.X - Config.WallScale)), 0, (int)Config.WallScale, Config.TextureWallSize.X);
                rects.Add(new RectToRender(wallRects[i], rayCasting.RayCastRes.Depth[i]));
            }
            for (int i = 0; i < probRects.Length; i++)
            {
                map.SpriteObjects[i].Update(player, map);
                probRects[i] = map.SpriteObjects[i].ProbRect;
                if (map.SpriteObjects[i].IsOnScreen)
                    rects.Add(new RectToRender(probRects[i], map.SpriteObjects[i].Depth));
            }

            rects = rects.OrderByDescending(i => i.depth).ToList();
        }

        private Texture[] GetWallTextures()
        {
            Texture[] textures = new Texture[Config.WallTextureNumber];
            for (int i = 1; i <= Config.WallTextureNumber; i++)
                textures[i - 1] = new Texture(Config.TextureWallPath + i.ToString() + ".png");
            return textures;
        }

        private Texture GetSkyTexture()
        {
            Texture texture = new Texture(Config.TextureSkyPath);
            return texture;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(skyRect, states);
            target.Draw(floorRect, PrimitiveType.Quads, states);
            for (int i = 0; i < rects.Count; i++)
                target.Draw(rects[i].rect, states);
        }
    }
}
