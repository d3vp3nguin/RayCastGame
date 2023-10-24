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


        private int[] numTexture;
        public int[] NumTexture { get { return numTexture; } }


        private float[] offset;
        public float[] Offset { get { return offset; } }


        private float[] depth;
        public float[] Depth { get { return depth; } }


        public RayCastResult()
        {
            projectionHeights = new float[Settings.RaysCount];
            projectionColor = new Color[Settings.RaysCount];
            numTexture = new int[Settings.RaysCount];
            offset = new float[Settings.RaysCount];
            depth = new float[Settings.RaysCount];
        }

        public RayCastResult(float[] projectionHeights, Color[] projectionColor,
                             int[] numTexture, float[] offset, float[] depth)
        {
            this.projectionHeights = projectionHeights;
            this.projectionColor = projectionColor;
            this.numTexture = numTexture;
            this.offset = offset;
            this.depth = depth;
        }
    }
}
