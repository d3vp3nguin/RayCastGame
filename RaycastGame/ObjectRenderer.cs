using SFML.Graphics;
using SFML.System;

namespace RaycastGame
{
    public class ObjectRenderer : Drawable
    {
        private RectangleShape[] wallRects;
        private Texture[] wallTextures;

        private RayCasting rayCasting;
        private Map map;

        public ObjectRenderer(RayCasting rayCasting, Map map)
        {
            this.rayCasting = rayCasting;
            this.map = map;

            wallRects = new RectangleShape[Settings.RaysCount];
            wallTextures = GetWallTextures();

            for (int i = 0; i < wallRects.Length; i++)
            {
                wallRects[i] = new RectangleShape();
                wallRects[i].FillColor = Color.White;
            }
        }

        public void Update()
        {
            for (int i = 0; i < Settings.RaysCount; i++)
            {
                wallRects[i].FillColor = rayCasting.RayCastRes.ProjectionColor[i];
                wallRects[i].Texture = wallTextures[rayCasting.RayCastRes.NumTexture[i]];
                wallRects[i].Position = new Vector2f(i * Settings.WallScale, Settings.GameResolution.Y / 2 - rayCasting.RayCastRes.ProjectionHeights[i] / 2);
                wallRects[i].Size = new Vector2f(Settings.WallScale, rayCasting.RayCastRes.ProjectionHeights[i]);
                wallRects[i].TextureRect = new IntRect((int)Math.Round(rayCasting.RayCastRes.Offset[i] * (Settings.TextureSize - Settings.WallScale)), 0, (int)Settings.WallScale, Settings.TextureSize);
            }
        }

        private Texture[] GetWallTextures()
        {
            Texture[] textures = new Texture[Settings.WallTextureNumber];
            for (int i = 1; i <= Settings.WallTextureNumber; i++)
                textures[i - 1] = new Texture(Settings.TexturePath + "Wall" + i.ToString() + ".png");
            return textures;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            for (int i = 0; i < wallRects.Length; i++)
                target.Draw(wallRects[i], states);
        }
    }
}
