using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace XNATest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Game World
        Texture2D gemTexture;
        Rectangle gemRectangle;
        int gemCentre;

        Texture2D lukeTexture;
        Rectangle lukeRectangle;
        Color lukeColor;

        //Enemy
        Texture2D enemyTexture;
        Rectangle enemyRectangle;
        Color enemyColor;
        int enemyCentre;

        Vector2 velocity;

        Random myRandom = new Random();

        //Screen Parameters
        int screenWidth;
        int screenHeight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            gemTexture = Content.Load<Texture2D>("Gem");
            gemRectangle = new Rectangle(300, 300, gemTexture.Width, gemTexture.Height);

            lukeTexture = Content.Load<Texture2D>("Luke_idle");
            lukeRectangle = new Rectangle(300, 400, lukeTexture.Width, lukeTexture.Height);
            lukeColor = Color.White;

            enemyTexture = Content.Load<Texture2D>("Luke_idle");
            enemyRectangle = new Rectangle(350, 10, enemyTexture.Width, enemyTexture.Height);
            enemyColor = Color.Red;

            //velocity.X = 3f;
            //velocity.Y = 3f;

            RandomLoad();

            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                RandomLoad();
            //if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //    myRectangle.X += 3;
            //if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //    myRectangle.X -= 3;

            //if (Keyboard.GetState().IsKeyDown(Keys.Up))
            //    myRectangle.Y -= 3;
            //if (Keyboard.GetState().IsKeyDown(Keys.Down))
            //    myRectangle.Y += 3;

            // Luke Movement
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                lukeRectangle.X -= 3;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                lukeRectangle.X += 3;

            //Luke and Gem interact
            if (gemRectangle.Intersects(lukeRectangle))
            {
                velocity.Y = -velocity.Y;
                //lukeColor = Color.Red;
            }

            if (lukeRectangle.X <= 0)
                lukeRectangle.X = -lukeRectangle.X;

            if (lukeRectangle.X + lukeTexture.Width >= screenWidth)
                lukeRectangle.X = -lukeRectangle.X;

            gemRectangle.X = gemRectangle.X + (int)velocity.X;
            gemRectangle.Y = gemRectangle.Y + (int)velocity.Y;

            if (gemRectangle.X <= 0)
                velocity.X = -velocity.X;

            if (gemRectangle.X + gemTexture.Width >= screenWidth)
                velocity.X = -velocity.X;

            if (gemRectangle.Y <= 0)
                velocity.Y = -velocity.Y;

            if (gemRectangle.Y + gemTexture.Height >= screenHeight)
                velocity.Y = -velocity.Y;

            //Centres
            gemCentre = gemRectangle.X + (gemTexture.Width / 2);
            enemyCentre = enemyRectangle.X + (enemyTexture.Width / 2);

            EnemyMovement();

            base.Update(gameTime);
        }

        void RandomLoad()
        {
            int random = myRandom.Next(0,4);
            //Down-right
            if (random == 0)
            {
                velocity.X = 3f;
                velocity.Y = 3f;
            }
            //Down-left
            if (random == 1)
            {
                velocity.X = -3f;
                velocity.Y = 3f;
            }
            //Up-left
            if (random == 2)
            {
                velocity.X = -3f;
                velocity.Y = -3f;
            }
            //Up-right
            if (random == 3)
            {
                velocity.X = 3f;
                velocity.Y = -3f;
            }

        }

        void EnemyMovement()
        {
            if (gemCentre > enemyCentre)
                enemyRectangle.X += 2;
            if (gemCentre < enemyCentre)
                enemyRectangle.X -= 2;

            if (gemRectangle.Intersects(enemyRectangle))
                velocity.Y = -velocity.Y;

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(gemTexture, gemRectangle, Color.Yellow);
            spriteBatch.Draw(lukeTexture, lukeRectangle, lukeColor);
            spriteBatch.Draw(enemyTexture, enemyRectangle, enemyColor);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
