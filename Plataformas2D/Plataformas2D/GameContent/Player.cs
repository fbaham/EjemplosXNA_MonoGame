using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Plataformas2D.GameContent
{
    class Player
    {
        private Animation playerIdle;
        private Animation playerRun;
        private Animation playerJump;
        private Animation playerAttack;
        private Animation playerDeath;
        private SpriteEffects flip = SpriteEffects.None;
        private AnimationPlayer sprite;
        private Vector2 start;
        Vector2 velocity;
        bool isAlive;

        Level level;
        Vector2 position;

        public Player(Level level, Vector2 position)
        {
            this.level = level;

            LoadContent();

            Reset(position);
        }

        internal Level Level { get => level; }
        public Vector2 Velocity { get => velocity; set => velocity = value; }
        public Vector2 Position { get => position; set => position = value; }
        public bool IsAlive { get => isAlive; }


        public void Reset(Vector2 position)
        {
            Position = position;
            Velocity = Vector2.Zero;
            isAlive = true;
            sprite.PlayAnimation(playerIdle);
        }

        private void LoadContent()
        {
            playerIdle = new Animation(Level.Content.Load<Texture2D>("Sprites/Player/player1"),48,48,8,0,50);
            playerRun = new Animation(Level.Content.Load<Texture2D>("Sprites/Player/player1"), 48, 48, 8, 1, 50);
            playerJump = new Animation(Level.Content.Load<Texture2D>("Sprites/Player/player1"), 48, 48, 8, 4, 50);
            playerAttack = new Animation(Level.Content.Load<Texture2D>("Sprites/Player/player1"), 48, 48, 8, 2, 50);
            playerDeath = new Animation(Level.Content.Load<Texture2D>("Sprites/Player/player1"), 48, 48, 8, 3, 50);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            GetInput(keyboardState);
            Position = Position + Velocity;


            if (Position.X <= 0)
                velocity.X = 3;

            if (Position.X + 48 >= 800)
                velocity.X = -3;

        }

        private void GetInput(KeyboardState keyboardState)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {

                velocity.X = 3;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {

                velocity.X = -3;
            }
            else
            {

                Velocity = Vector2.Zero;
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Velocity.X > 0)
                flip = SpriteEffects.FlipHorizontally;
            else if (Velocity.X < 0)
                flip = SpriteEffects.None;

            sprite.Draw(gameTime, spriteBatch, Position, flip);
        }

    }
}
