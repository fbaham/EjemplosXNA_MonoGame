using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2D
{
    class Player
    {
        Texture2D texture;

        Vector2 position;
        public Vector2 velocity;

        Rectangle rectangle;

        bool hasJumped;

        public Player(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            hasJumped = true;
        }

        public Texture2D Texture { get => texture;}
        public Vector2 Position { get => position;}
        //public Vector2 Velocity { get => velocity; set => velocity = value; }
        public Rectangle Rectangle { get => rectangle;}
        public bool HasJumped { get => hasJumped; set => hasJumped = value; }

        public void Update(GameTime gameTime)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            if (Keyboard.GetState().IsKeyDown(Keys.Right)) velocity.X = 3f;
            else if (Keyboard.GetState().IsKeyDown(Keys.Left)) velocity.X = -3f;
            else velocity.X = 0;

            if(Keyboard.GetState().IsKeyDown(Keys.Space) && !HasJumped)
            {
                position.Y -= 10f;
                velocity.Y = -5f;
                hasJumped = true;

            }

            float i = 1;
            velocity.Y += 0.15f * i;
         
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }

    }
}
