using SFML.Graphics;
using SFML.System;

namespace RaycastGame
{
    public class Map : Drawable
    {
        private Vector2i playerStartPosMap = new Vector2i(1, 4);
        public Vector2f PlayerStartPos { get { return new Vector2f(playerStartPosMap.X * shapeSize.X + shapeSize.X / 2f + Config.MapOffset.X + Config.PlayerMapRadius, playerStartPosMap.Y * shapeSize.Y + shapeSize.Y / 2f + Config.MapOffset.Y + Config.PlayerMapRadius); } }


        private float playerStartRotation = 0f;
        public float PlayerStartRotation { get { return playerStartRotation; } }


        private SpriteObject[] spriteObjects = { new SpriteObject(1, new Vector2f(46f, 46f)),
                                                 new SpriteObject(1, new Vector2f(46f, 244f)),
                                                 new SpriteObject(2, new Vector2f(184f, 160f)),
                                                 new SpriteObject(3, new Vector2f(136f, 244f)),
                                                 new SpriteObject(1, new Vector2f(166f, 90f)),
                                                 new SpriteObject(3, new Vector2f(220f, 124f)),
                                                 new SpriteObject(2, new Vector2f(304f, 46f)),
                                                 new SpriteObject(3, new Vector2f(286f, 180f)),
                                                 new SpriteObject(1, new Vector2f(364f, 244f)),
                                                 new SpriteObject(2, new Vector2f(454f, 46f)),
                                                 new SpriteObject(1, new Vector2f(454f, 244f))};
        public SpriteObject[] SpriteObjects { get { return spriteObjects; } }


        private RectangleShape[,] mapShapes;
        public RectangleShape[,] MapShapes { get { return mapShapes; } }


        private int[,] mapWallBase = { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                                       { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 1},
                                       { 1, 0, 0, 3, 3, 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 1},
                                       { 1, 0, 0, 3, 3, 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 1},
                                       { 1, 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 1},
                                       { 1, 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 1},
                                       { 1, 0, 3, 3, 0, 0, 2, 2, 2, 0, 0, 0, 3, 3, 0, 1},
                                       { 1, 0, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 0, 1},
                                       { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};
        public int[,] MapWallBase { get { return mapWallBase; } }


        private Vector2f shapeSize;

        public Map()
        {
            shapeSize = new Vector2f(Config.MapSize.X / mapWallBase.GetLength(1) - 2 * Config.WallOutlineThickness,
                                     Config.MapSize.Y / mapWallBase.GetLength(0) - 2 * Config.WallOutlineThickness);
            mapShapes = new RectangleShape[mapWallBase.GetLength(0), mapWallBase.GetLength(1)];

            for (int y = 0; y < mapWallBase.GetLength(0); y++)
            {
                for (int x = 0; x < mapWallBase.GetLength(1); x++)
                {
                    mapShapes[y, x] = new RectangleShape(shapeSize);
                    mapShapes[y, x].OutlineThickness = Config.WallOutlineThickness;
                    mapShapes[y, x].FillColor = mapWallBase[y, x] != 0 ? Config.WallYesFillColor : Config.WallNoFillColor;
                    mapShapes[y, x].OutlineColor = mapWallBase[y, x] != 0 ? Config.WallYesOutlineColor : Config.WallNoOutlineColor;
                    mapShapes[y, x].Position = new Vector2f(x * (shapeSize.X + 2 * Config.WallOutlineThickness) + Config.WallOutlineThickness, y * (shapeSize.Y + 2 * Config.WallOutlineThickness) + Config.WallOutlineThickness) + Config.MapOffset;
                }
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (RectangleShape mapShape in mapShapes)
                if (mapShape.OutlineColor == Config.WallNoOutlineColor)
                    target.Draw(mapShape, states);

            foreach (RectangleShape mapShape in mapShapes)
                if (mapShape.OutlineColor == Config.WallYesOutlineColor)
                    target.Draw(mapShape, states);
        }
    }
}
