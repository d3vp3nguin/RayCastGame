using SFML.Graphics;
using SFML.System;

namespace RaycastGame
{
    public class ObjectRenderer : Drawable
    {
        private RectangleShape[] wallRects;
        private Texture[] wallTextures;

        private RayCasting rayCasting;
        public ObjectRenderer(RayCasting rayCasting)
        {
            this.rayCasting = rayCasting;
            wallRects = new RectangleShape[Settings.RaysCount];
            wallTextures = GetTextures();
            for (int i = 0; i < wallRects.Length; i++)
            {
                wallRects[i] = new RectangleShape();
                wallRects[i].FillColor = Color.White;
            }
        }

        public void Update()
        {
            for (int i = 0; i < wallRects.Length; i++)
            {
                wallRects[i].Size = new Vector2f(Settings.WallScale, rayCasting.ProjectionHeights[i]);
                wallRects[i].Position = new Vector2f(i * Settings.WallScale, Settings.GameResolution.Y / 2 - rayCasting.ProjectionHeights[i] / 2);
                wallRects[i].FillColor = rayCasting.ProjectionColor[i];
            }
        }

        private Texture[] GetTextures()
        {
            Texture[] textures = new Texture[Settings.WallTextureNumber];
            for (int i = 1; i <= Settings.WallTextureNumber; i++)
                textures[i] = new Texture(Settings.TexturePath + "Wall" + i.ToString());
            return textures;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            for (int i = 0; i < wallRects.Length; i++)
                target.Draw(wallRects[i], states);
        }
    }
}
