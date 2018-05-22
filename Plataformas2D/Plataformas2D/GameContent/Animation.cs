using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Plataformas2D.GameContent
{
    class Animation
    {
        #region Constructor
        private Texture2D texture;
        Vector2 position;
        int frameHeight;
        int frameWidth;

        public Animation(Texture2D texture, Vector2 position, int frameHeight, int frameWidth)
        {
            this.Texture = texture;
            this.Position = position;
            this.FrameHeight = frameHeight;
            this.FrameWidth = frameWidth;
        }

        #endregion

        #region Propiedades
        public Texture2D Texture { get => texture; set => texture = value; }
        public Vector2 Position { get => position; set => position = value; }
        public int FrameHeight { get => frameHeight; set => frameHeight = value; }
        public int FrameWidth { get => frameWidth; set => frameWidth = value; }
        #endregion

        private Rectangle rectangle;
        Vector2 origin;
        Vector2 velocity;

        public int currentFrame;
        public int currentRow;

        SpriteEffects effects;

        public bool direction = true; //true = right - false = left

        float timer;
        float interval = 50;
        
        public void Update(GameTime gameTime)
        {
            rectangle = new Rectangle(currentFrame * FrameWidth, currentRow * FrameHeight, FrameWidth, FrameHeight);
            origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            Position = Position + velocity;

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

            if (Position.X <= 0)
                velocity.X = 3;

            if (Position.X + FrameWidth >= 800)
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
                if (direction)
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, rectangle, Color.White, 0f, origin, 1.0f, effects, 0);
        }

    }
}
