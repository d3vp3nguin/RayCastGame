﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RaycastGame
{
    public class Label : UIelement
    {
        public Label(Vector2f size, Vector2f position, Color fillColorBack, Color fillColorFront, string displayedString, int id) : base(size, position, fillColorBack, fillColorFront, displayedString, id)
        {
            
        }
    }
}
