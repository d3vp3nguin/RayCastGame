using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RaycastGame
{
    public class Button : Label
    {
        private int idButton = -1;
        public int ID { get { return idButton; } }

        private bool isPressedLKM = false;

        public Button(Vector2f size, Vector2f position, Color fillColorBack, Color fillColorFront, string displayedString, int idButton) : base(size, position, fillColorBack, fillColorFront, displayedString)
        {
            this.idButton = idButton;
        }

        public bool IsPressed()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (isPressedLKM) return false;

                Vector2i mousePos = Mouse.GetPosition();
                FloatRect button = background.GetGlobalBounds();
                if (mousePos.X < button.Left || mousePos.X > button.Left + button.Width ||
                    mousePos.Y < button.Top || mousePos.Y > button.Top + button.Height) return false;

                isPressedLKM = true;
                return true;
            }
            else isPressedLKM = false;
            return false;
        }
    }
}
