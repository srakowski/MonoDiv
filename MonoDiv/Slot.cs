// MIT License - Copyright (C) Shawn Rakowski
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace MonoDiv
{
    internal class Slot : Div
    {
        internal override void UpdateLayout(Point position, SpriteFont font)
        {
            var nextPosition = position;
            foreach (var child in Children)
            {
                child.UpdateLayout(nextPosition, font);
                nextPosition += new Point(0, child.Bounds.Height);
            }
            var maxWidth = Children.Select(c => c.Bounds.Width).Max();
            var height = Children.Select(c => c.Bounds.Height).Sum();
            Bounds = new Rectangle(position, new Point(maxWidth, height));
        }

        internal override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            foreach (var child in Children)
                child.Draw(spriteBatch, font);
        }
    }
}
