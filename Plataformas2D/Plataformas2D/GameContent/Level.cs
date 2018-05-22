using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Plataformas2D.GameContent
{
    class Level : IDisposable
    {
        Player player;
        ContentManager content;

        private Vector2 start;

        public Level(IServiceProvider serviceProvider)
        {
            content = new ContentManager(serviceProvider, "Content");
            loadPlayer();
        }

        public ContentManager Content { get => content; }
        internal Player Player { get => player; }

        private void loadPlayer()
        {
            start = new Vector2(80, 80);
            player = new Player(this, start);
        }

        public void Dispose()
        {
            Content.Unload();
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            Player.Update(gameTime, keyboardState);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Player.Draw(gameTime, spriteBatch);
        }
    }
}
