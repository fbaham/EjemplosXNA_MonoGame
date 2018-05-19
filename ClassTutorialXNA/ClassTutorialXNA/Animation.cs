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
    class Animation : Sprite
    {
        Vector2 position;
        Vector2 origin;
        Vector2 velocity;

        public int currentFrame;
        public int currentRow;
        int frameHeight;
        int frameWidth;

        SpriteEffects effects;

        public bool direction = true; //true = right - false = left

        float timer;
        float interval = 50;

        public Animation(Texture2D texture, Vector2 position, int frameHeight, int frameWidth)
        {
            this.texture = texture;
            this.position = position;
            this.frameHeight = frameHeight;
            this.frameWidth = frameWidth;
        }

        public void Update(GameTime gameTime)
        {
            rectangle = new Rectangle(currentFrame * frameWidth, currentRow * frameHeight, frameWidth, frameHeight);
            origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            position = position + velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                AnimateRight(gameTime);
                velocity.X = 3;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                AnimateLeft(gameTime);
                velocity.X = -3;
            }
            else
            {
                AnimateIdle(gameTime);
                velocity = Vector2.Zero;
            }

            if (position.X <= 0)
                velocity.X = 3;

            if (position.X + frameWidth >= 800)
                velocity.X = -3;

        }

        public void AnimateRight(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                if (!direction)
                    direction = true;
                effects = SpriteEffects.None;
                currentRow = 1;
                currentFrame++;
                timer = 0;
                if (currentFrame > 8)
                {
                    currentFrame = 0;
                }                   
            }
        }

        public void AnimateLeft(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                if (direction)
                    direction = false;
                effects = SpriteEffects.None;
                currentRow = 2;
                currentFrame++;
                timer = 0;
                if (currentFrame > 8)
                {
                    currentFrame = 0;     
                }                    
            }
        }

        public void AnimateIdle(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 10;
            if (timer > interval)
            {
                if(direction)
                    effects = SpriteEffects.None;
                else
                    effects = SpriteEffects.FlipHorizontally;
            currentRow = 0;
                currentFrame++;
                timer = 0;
                if (currentFrame > 2)
                {
                    currentFrame = 0;
                }
                    
            }
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, origin, 1.0f, effects, 0);
        }
    }
}
