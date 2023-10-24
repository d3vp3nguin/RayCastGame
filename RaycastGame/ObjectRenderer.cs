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

            wallRects = new RectangleShape[Settings.RaysCount];
            for (int i = 0; i < wallRects.Length; i++)
            {
                wallRects[i] = new RectangleShape();
                wallRects[i].FillColor = Color.White;
            }
            wallTextures = GetWallTextures();

            skyRect = new RectangleShape();
            skyRect.Position = new Vector2f(0, 0);
            skyRect.Size = new Vector2f(Settings.GameResolution.X, Settings.GameResolution.Y / 2);
            skyRect.FillColor = Color.White;
            skyTexture = GetSkyTexture();
            skyTexture.Repeated = true;
            skyRect.Texture = skyTexture;

            floorRect = new Vertex[4];
            floorRect[0] = new Vertex(new Vector2f(0, Settings.GameResolution.Y / 2), Settings.FloorDarkColor);
            floorRect[1] = new Vertex(new Vector2f(Settings.GameResolution.X, Settings.GameResolution.Y / 2), Settings.FloorDarkColor);
            floorRect[2] = new Vertex(new Vector2f(Settings.GameResolution.X, Settings.GameResolution.Y), Settings.FloorBrightColor);
            floorRect[3] = new Vertex(new Vector2f(0, Settings.GameResolution.Y), Settings.FloorBrightColor);

            probRects = new RectangleShape[map.SpriteObjects.Length];
            for (int i = 0; i < probRects.Length; i++) 
                probRects[i] = map.SpriteObjects[i].ProbRect;

            rects = new List<RectToRender>();
        }

        public void Update()
        {
            rects.Clear();

            skyRect.TextureRect = new IntRect((int)(player.Angle / (float)Math.Tau * Settings.TextureSkySize.X) - Settings.TextureSkySize.Y / 2, 0, Settings.TextureSkySize.Y, Settings.TextureSkySize.Y);

            for (int i = 0; i < Settings.RaysCount; i++)
            {
                wallRects[i].FillColor = rayCasting.RayCastRes.ProjectionColor[i];
                wallRects[i].Texture = wallTextures[rayCasting.RayCastRes.NumTexture[i]];
                wallRects[i].Position = new Vector2f(i * Settings.WallScale, Settings.GameResolution.Y / 2 - rayCasting.RayCastRes.ProjectionHeights[i] / 2);
                wallRects[i].Size = new Vector2f(Settings.WallScale, rayCasting.RayCastRes.ProjectionHeights[i]);
                wallRects[i].TextureRect = new IntRect((int)Math.Round(rayCasting.RayCastRes.Offset[i] * (Settings.TextureWallSize.X - Settings.WallScale)), 0, (int)Settings.WallScale, Settings.TextureWallSize.X);
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
            Texture[] textures = new Texture[Settings.WallTextureNumber];
            for (int i = 1; i <= Settings.WallTextureNumber; i++)
                textures[i - 1] = new Texture(Settings.TextureWallPath + i.ToString() + ".png");
            return textures;
        }

        private Texture GetSkyTexture()
        {
            Texture texture = new Texture(Settings.TextureSkyPath);
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
