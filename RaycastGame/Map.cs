using SFML.Graphics;
using SFML.System;

namespace RaycastGame
{
    public class Map : Drawable
    {
        private int[,] mapBase = { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                                   { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                   { 1, 0, 2, 2, 2, 2, 2, 0, 0, 3, 0, 0, 3, 3, 0, 1},
                                   { 1, 0, 2, 0, 2, 0, 0, 0, 0, 3, 3, 3, 3, 0, 0, 1},
                                   { 1, 0, 0, 0, 0, 0, 2, 2, 0, 3, 0, 0, 3, 3, 0, 1},
                                   { 1, 0, 0, 3, 0, 0, 2, 0, 0, 3, 0, 0, 3, 3, 0, 1},
                                   { 1, 0, 3, 3, 0, 2, 2, 2, 0, 3, 0, 3, 3, 0, 0, 1},
                                   { 1, 0, 3, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                                   { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};
        public int[,] MapBase { get { return mapBase; } }

        private RectangleShape[,] mapShapes;
        public RectangleShape[,] MapShapes { get { return mapShapes; } }

        private Vector2f shapeSize;


        private Vector2i playerStartPosMap = new Vector2i(1, 1);
        public Vector2f PlayerStartPos { get { return new Vector2f(playerStartPosMap.X * shapeSize.X + shapeSize.X / 2f - Settings.PlayerMapRadius,
                                                                   playerStartPosMap.Y * shapeSize.Y + shapeSize.Y / 2f - Settings.PlayerMapRadius); } }


        private float playerStartRotation = 0f;
        public float PlayerStartRotation { get { return playerStartRotation; } }

        public Map()
        {
            shapeSize = new Vector2f(Settings.MapSize.X / mapBase.GetLength(1) - 2 * Settings.WallOutlineThickness, 
                                        Settings.MapSize.Y / mapBase.GetLength(0) - 2 * Settings.WallOutlineThickness);
            mapShapes = new RectangleShape[mapBase.GetLength(0), mapBase.GetLength(1)];

            for (int y = 0; y < mapBase.GetLength(0); y++)
            {
                for (int x = 0; x < mapBase.GetLength(1); x++)
                {
                    mapShapes[y, x] = new RectangleShape(shapeSize);
                    mapShapes[y, x].OutlineThickness = Settings.WallOutlineThickness;
                    mapShapes[y, x].FillColor = mapBase[y, x] == 1 ? Settings.WallYesFillColor : Settings.WallNoFillColor;
                    mapShapes[y, x].OutlineColor = mapBase[y, x] == 1 ? Settings.WallYesOutlineColor : Settings.WallNoOutlineColor;
                    mapShapes[y, x].Position = new Vector2f(x * (shapeSize.X + 2 * Settings.WallOutlineThickness) + Settings.WallOutlineThickness, y * (shapeSize.Y + 2 * Settings.WallOutlineThickness) + Settings.WallOutlineThickness) + Settings.MapOffset;
                }
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (RectangleShape mapShape in mapShapes)
                if (mapShape.OutlineColor == Settings.WallNoOutlineColor)
                    target.Draw(mapShape, states);

            foreach (RectangleShape mapShape in mapShapes)
                if (mapShape.OutlineColor == Settings.WallYesOutlineColor)
                    target.Draw(mapShape, states);
        }
    }
}
