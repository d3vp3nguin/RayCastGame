using SFML.Graphics;
using SFML.System;

namespace RaycastGame
{
    public class RayCastResult
    {
        private float[] projectionHeights;
        public float[] ProjectionHeights { get { return projectionHeights; } }


        private Color[] projectionColor;
        public Color[] ProjectionColor { get { return projectionColor; } }


        private Vector2i[] posMap;
        public Vector2i[] PosMap { get { return posMap; } }


        private float[] offset;
        public float[] Offset { get { return offset; } }


        public RayCastResult()
        {
            projectionHeights = new float[Settings.RaysCount];
            projectionColor = new Color[Settings.RaysCount];
            posMap = new Vector2i[Settings.RaysCount];
            offset = new float[Settings.RaysCount];
        }

        public RayCastResult(float[] projectionHeights, Color[] projectionColor,
                             Vector2i[] posMap, float[] offset)
        {
            this.projectionHeights = projectionHeights;
            this.projectionColor = projectionColor;
            this.posMap = posMap;
            this.offset = offset;
        }
    }
}
