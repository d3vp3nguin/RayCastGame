using SFML.Graphics;

namespace RaycastGame
{
    public class RectToRender
    {
        public RectangleShape rect;
        public float depth;

        public RectToRender()
        {
            rect = new RectangleShape();
            depth = 0f;
        }

        public RectToRender(RectangleShape rect, float depth)
        {
            this.rect = rect;
            this.depth = depth;
        }
    }
}
