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
            textFPS.Scale = new Vector2f(Settings.DevTextScale, Settings.DevTextScale);
            textFPS.FillColor = Settings.DevTextFillColor;
            textFPS.OutlineColor = Settings.DevTextOutlineColor;
            textFPS.OutlineThickness = Settings.DevTextThickness;
            textFPS.CharacterSize = Settings.DevTextCharacterSize;
            textFPS.Position = new Vector2f(Settings.DevTextFPSX, Settings.DevTextFPSY);
            textFPS.Font = new Font(Settings.DevTextFontPath);

            textPlayerPosition = new Text();
            textPlayerPosition.Scale = new Vector2f(Settings.DevTextScale, Settings.DevTextScale);
            textPlayerPosition.FillColor = Settings.DevTextFillColor;
            textPlayerPosition.OutlineColor = Settings.DevTextOutlineColor;
            textPlayerPosition.OutlineThickness = Settings.DevTextThickness;
            textPlayerPosition.CharacterSize = Settings.DevTextCharacterSize;
            textPlayerPosition.Position = new Vector2f(Settings.DevTextPositionX, Settings.DevTextPositionY);
            textPlayerPosition.Font = new Font(Settings.DevTextFontPath);

            textPlayerPositionMap = new Text();
            textPlayerPositionMap.Scale = new Vector2f(Settings.DevTextScale, Settings.DevTextScale);
            textPlayerPositionMap.FillColor = Settings.DevTextFillColor;
            textPlayerPositionMap.OutlineColor = Settings.DevTextOutlineColor;
            textPlayerPositionMap.OutlineThickness = Settings.DevTextThickness;
            textPlayerPositionMap.CharacterSize = Settings.DevTextCharacterSize;
            textPlayerPositionMap.Position = new Vector2f(Settings.DevTextPositionMapX, Settings.DevTextPositionMapY);
            textPlayerPositionMap.Font = new Font(Settings.DevTextFontPath);

            textPlayerRotation = new Text();
            textPlayerRotation.Scale = new Vector2f(Settings.DevTextScale, Settings.DevTextScale);
            textPlayerRotation.FillColor = Settings.DevTextFillColor;
            textPlayerRotation.OutlineColor = Settings.DevTextOutlineColor;
            textPlayerRotation.OutlineThickness = Settings.DevTextThickness;
            textPlayerRotation.CharacterSize = Settings.DevTextCharacterSize;
            textPlayerRotation.Position = new Vector2f(Settings.DevTextRotationX, Settings.DevTextRotationY);
            textPlayerRotation.Font = new Font(Settings.DevTextFontPath);
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