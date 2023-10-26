using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RaycastGame
{
    public class Scroller : Label
    {
        private int idScroller = -1;
        public int ID { get { return idScroller; } }

        private float value = 0f;
        private float valueMin = 0f;
        private float valueMax = 0f;

        public Scroller(Vector2f size, Vector2f position, Color fillColorBack, Color fillColorFront, string displayedString, int idScroller, float value, float valueMin, float valueMax) : base(size, position, fillColorBack, fillColorFront, displayedString)
        {
            this.idScroller = idScroller;
            this.value = value;
            this.valueMin = valueMin;
            this.valueMax = valueMax;
        }

        private bool IsScrolled()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2i mousePos = Mouse.GetPosition();
                FloatRect scroller = background.GetGlobalBounds();
                if (mousePos.X < scroller.Left || mousePos.X > scroller.Left + scroller.Width ||
                    mousePos.Y < scroller.Top || mousePos.Y > scroller.Top + scroller.Height) return false;

                return true;
            }
            else return false;
        }

        public float GetScrollValue()
        {
            if (!IsScrolled()) 
            {
                RecalcForeground(value);
                return value; 
            }

            Vector2i mousePos = Mouse.GetPosition();
            FloatRect scroller = base.background.GetGlobalBounds();

            value = MyMath.Clamp((mousePos.X - scroller.Left) / scroller.Width, 0f, 1f);
            RecalcForeground(value);
            return value;
        }

        public float GetScrollFullValue() 
        {
            return GetScrollValue() * (valueMax - valueMin) + valueMin;
        }
    }
}
