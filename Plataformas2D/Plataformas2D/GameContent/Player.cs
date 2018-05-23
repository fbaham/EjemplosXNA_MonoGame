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
        #region Constructor

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

        SpriteFont font;

        public Player(Level level, Vector2 position)
        {
            this.level = level;

            LoadContent();

            Reset(position);
        }

        #endregion

        #region Propiedades

        internal Level Level { get => level; }
        public Vector2 Velocity { get => velocity; set => velocity = value; }
        public Vector2 Position { get => position; set => position = value; }
        public bool IsAlive { get => isAlive; }

        #endregion

        #region Metodos

        public void Reset(Vector2 position)
        {
            Position = position;
            Velocity = Vector2.Zero;
            isAlive = true;
            sprite.PlayAnimation(playerIdle);
        }

        private void LoadContent()
        {
            playerIdle = new Animation(Level.Content.Load<Texture2D>("Sprites/Player/player1"),48,48,7,0,50);
            playerRun = new Animation(Level.Content.Load<Texture2D>("Sprites/Player/player1"), 48, 48, 7, 1, 50);
            playerJump = new Animation(Level.Content.Load<Texture2D>("Sprites/Player/player1"), 48, 48, 7, 4, 50);
            playerAttack = new Animation(Level.Content.Load<Texture2D>("Sprites/Player/player1"), 48, 48, 7, 2, 50);
            playerDeath = new Animation(Level.Content.Load<Texture2D>("Sprites/Player/player1"), 48, 48, 7, 3, 50);

            font = Level.Content.Load<SpriteFont>("basicFont");
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            GetInput(keyboardState);
            ApplyPhysics(gameTime);
        }

        private void GetInput(KeyboardState keyboardState)
        {
            Position = Position + Velocity;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                sprite.PlayAnimation(playerRun);
                velocity.X = 3;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                sprite.PlayAnimation(playerRun);
                velocity.X = -3;
            }
            else
            {
                sprite.PlayAnimation(playerIdle);
                Velocity = Vector2.Zero;
            }
            
        }
        private void ApplyPhysics(GameTime gameTime)
        {
            
            if (Position.X < 0)
                velocity.X = -velocity.X;

            if (Position.X > 800)
                velocity.X = -velocity.X;
            
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Velocity.X > 0)
                flip = SpriteEffects.FlipHorizontally;
            else if (Velocity.X < 0)
                flip = SpriteEffects.None;

            sprite.Draw(gameTime, spriteBatch, Position, flip);
            spriteBatch.DrawString(font, "Position: " + Position.X, new Vector2(10,10), Color.Red);
            spriteBatch.DrawString(font, "Velocity: " + Velocity.X, new Vector2(10, 25), Color.Red);

        }
        #endregion
    }
}
