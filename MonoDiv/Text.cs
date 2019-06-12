// MIT License - Copyright (C) Shawn Rakowski
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoDiv
{
    internal class Text : Div
    {
        public Text(string value)
        {
            Value = value;
        }

        public string Value { get; }

        internal override void UpdateLayout(Point position, SpriteFont font)
        {
            Bounds = new Rectangle(position, font.MeasureString(this.Value).ToPoint());
        }

        internal override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, Value, Bounds.Location.ToVector2(), Color.White);
        }
    }
}
