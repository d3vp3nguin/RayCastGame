using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RaycastGame
{
    public class Scroller : UIelement
    {
        private float value = 0f;
        private float valueMin = 0f;
        private float valueMax = 0f;

        public float Value { get { return value; } }


        public Scroller(Vector2f size, Vector2f position, Color fillColorBack, Color fillColorFront, string displayedString, float value, float valueMin, float valueMax, int id) : base(size, position, fillColorBack, fillColorFront, displayedString, id)
        {
            this.value = value;
            this.valueMin = valueMin;
            this.valueMax = valueMax;
        }

        public float GetScrollFullValue() 
        {
            return GetScrollValue() * (valueMax - valueMin) + valueMin;
        }

        public float GetScrollValue()
        {
            RecalcForeground(value);

            if (!IsScrolled()) return value; 

            Vector2i mousePos = Mouse.GetPosition();
            FloatRect scroller = base.background.GetGlobalBounds();
            value = MyMath.Clamp((mousePos.X - scroller.Left) / scroller.Width, 0f, 1f);
            return value;
        }

        public bool IsScrolled()
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
    }
}
