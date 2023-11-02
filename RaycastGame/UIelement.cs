using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RaycastGame
{
    public class UIelement : Drawable
    {
        private RectangleShape _background;
        private RectangleShape _foreground;
        private Text _text;
        public RectangleShape background { get { return _background; } set { _background = value; } }
        public RectangleShape foreground { get { return _foreground; } set { _foreground = value; } }
        public Text text { get { return _text; } set { _text = value; } }

        private int id = -1;
        public int ID { get { return id; } }

        private float _width = 0f;

        public UIelement(Vector2f size, Vector2f position, Color fillColorBack, Color fillColorFront, string displayedString, int id)
        {
            _background = new RectangleShape();
            _background.Size = size;
            _background.Origin = size / 2;
            _background.Position = position;
            _background.FillColor = fillColorBack;

            _foreground = new RectangleShape();
            _foreground.Size = _background.Size;
            _foreground.Position = position - _background.Origin;
            _foreground.FillColor = fillColorFront;

            _text = new Text();
            _text.DisplayedString = displayedString;
            _text.Font = new Font(Config.MenuTextFontPath);
            _text.CharacterSize = Config.MenuTextCharacterSize;
            _text.FillColor = Config.MenuTextFillColor;
            _text.OutlineColor = Config.MenuTextOutlineColor;
            _text.OutlineThickness = Config.MenuTextThickness;
            _text.Origin = new Vector2f(text.GetGlobalBounds().Width / 2, text.GetGlobalBounds().Height);
            _text.Position = position;

            this.id = id;

            _width = size.X;
        }

        public void RecalcForeground(float k)
        {
            _foreground.Size = new Vector2f(_width * k, _foreground.Size.Y);
        }

        public void ChangeForegroundColor(Color color)
        {
            _foreground.FillColor = color;
        }

        public void UpdateText(string displayedString)
        {
            text.DisplayedString = displayedString;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_background, states);
            target.Draw(_foreground, states);
            target.Draw(text, states);
        }
    }
}
