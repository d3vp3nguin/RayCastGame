using SFML.Graphics;
using SFML.System;

namespace RaycastGame
{
    public class DevInfo : Drawable
    {
        private Text textFPS;
        private Text textPlayerPosition;
        private Text textPlayerPositionMap;
        private Text textPlayerRotation;

        public DevInfo()
        {
            textFPS = new Text();
            textFPS.Scale = new Vector2f(Config.DevTextScale, Config.DevTextScale);
            textFPS.FillColor = Config.DevTextFillColor;
            textFPS.OutlineColor = Config.DevTextOutlineColor;
            textFPS.OutlineThickness = Config.DevTextThickness;
            textFPS.CharacterSize = Config.DevTextCharacterSize;
            textFPS.Position = new Vector2f(Config.DevTextFPSX, Config.DevTextFPSY);
            textFPS.Font = new Font(Config.DevTextFontPath);

            textPlayerPosition = new Text();
            textPlayerPosition.Scale = new Vector2f(Config.DevTextScale, Config.DevTextScale);
            textPlayerPosition.FillColor = Config.DevTextFillColor;
            textPlayerPosition.OutlineColor = Config.DevTextOutlineColor;
            textPlayerPosition.OutlineThickness = Config.DevTextThickness;
            textPlayerPosition.CharacterSize = Config.DevTextCharacterSize;
            textPlayerPosition.Position = new Vector2f(Config.DevTextPositionX, Config.DevTextPositionY);
            textPlayerPosition.Font = new Font(Config.DevTextFontPath);

            textPlayerPositionMap = new Text();
            textPlayerPositionMap.Scale = new Vector2f(Config.DevTextScale, Config.DevTextScale);
            textPlayerPositionMap.FillColor = Config.DevTextFillColor;
            textPlayerPositionMap.OutlineColor = Config.DevTextOutlineColor;
            textPlayerPositionMap.OutlineThickness = Config.DevTextThickness;
            textPlayerPositionMap.CharacterSize = Config.DevTextCharacterSize;
            textPlayerPositionMap.Position = new Vector2f(Config.DevTextPositionMapX, Config.DevTextPositionMapY);
            textPlayerPositionMap.Font = new Font(Config.DevTextFontPath);

            textPlayerRotation = new Text();
            textPlayerRotation.Scale = new Vector2f(Config.DevTextScale, Config.DevTextScale);
            textPlayerRotation.FillColor = Config.DevTextFillColor;
            textPlayerRotation.OutlineColor = Config.DevTextOutlineColor;
            textPlayerRotation.OutlineThickness = Config.DevTextThickness;
            textPlayerRotation.CharacterSize = Config.DevTextCharacterSize;
            textPlayerRotation.Position = new Vector2f(Config.DevTextRotationX, Config.DevTextRotationY);
            textPlayerRotation.Font = new Font(Config.DevTextFontPath);
        }

        public void UpdateInfo(int fps, Vector2f pos, Vector2i posMap, float rot)
        {
            textFPS.DisplayedString = $"FPS: {fps}";
            textPlayerPosition.DisplayedString = $"POS: ({pos.X.ToString("0.00")}, {pos.Y.ToString("0.00")})";
            textPlayerPositionMap.DisplayedString = $"MAP: ({posMap.X}, {posMap.Y})";
            textPlayerRotation.DisplayedString = $"ROT: {rot}";
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(textFPS, states);
            target.Draw(textPlayerPosition, states);
            target.Draw(textPlayerPositionMap, states);
            target.Draw(textPlayerRotation, states);
        }
    }
}