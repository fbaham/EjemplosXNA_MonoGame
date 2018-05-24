using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2D
{
    class Platform
    {
        Texture2D texture;
        Vector2 position;
        public Rectangle rectangle;

        public Platform(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;

            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, new Rectangle(16, 0, 16, 16), Color.White);
        }

    }
}
