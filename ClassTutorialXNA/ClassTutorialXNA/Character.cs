using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTutorialXNA
{
    class Character : Sprite
    {
        public Character(Texture2D newTexture, Rectangle newRectangle, int screenWidth, int screenHeight)
        {
            this.texture = newTexture;
            this.rectangle = newRectangle;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                rectangle.X += 3;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                rectangle.X -= 3;

            if (rectangle.X <= 0)
                rectangle.X = -rectangle.X;

            if (rectangle.X + texture.Width >= screenWidth)
                rectangle.X = -rectangle.X;

        }

    }
}
