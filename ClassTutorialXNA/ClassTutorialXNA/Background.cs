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
    class Background
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }

    class Scrolling : Background
    {
        public Scrolling(Texture2D newTexture, Rectangle newRectange)
        {
            texture = newTexture;
            rectangle = newRectange;
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                rectangle.X -= 3;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                rectangle.X += 3;

        }

    }
}
