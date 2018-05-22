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
    class Background : Sprite
    {
        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }
    }
}
