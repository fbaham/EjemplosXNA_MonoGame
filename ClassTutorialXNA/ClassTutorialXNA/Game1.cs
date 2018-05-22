using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ClassTutorialXNA
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public enum GameState
        {
            MainMenu,
            LevelSelect,
            Level1,
        }

        GameState CurrentState = GameState.MainMenu;

        int Level;

        SpriteFont font;
        int score;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Scrolling sky;
        Scrolling rocksBack1;
        Scrolling rocksFront1;

        Scrolling rocksBack2;
        Scrolling rocksFront2;

        Scrolling[] clouds;

        // Game World
        Player luke;

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
            sky = new Scrolling(Content.Load<Texture2D>("layers/sky"), new Rectangle(0, 0, 800, 480), Color.White);

            rocksBack1 = new Scrolling(Content.Load<Texture2D>("layers/rocks_1"), new Rectangle(0, 0, 800, 480), Color.White);
            rocksFront1 = new Scrolling(Content.Load<Texture2D>("layers/rocks_2"), new Rectangle(0, 0, 800, 480), Color.White);

            rocksBack2 = new Scrolling(Content.Load<Texture2D>("layers/rocks_1"), new Rectangle(800, 0, 800, 480), Color.White);
            rocksFront2 = new Scrolling(Content.Load<Texture2D>("layers/rocks_2"), new Rectangle(800, 0, 800, 480), Color.White);

            rocksFront2 = new Scrolling(Content.Load<Texture2D>("layers/rocks_2"), new Rectangle(800, 0, 800, 480), Color.White);

            clouds = new Scrolling[4];
            clouds[0] = new Scrolling(Content.Load<Texture2D>("layers/clouds_1"), new Rectangle(0, 0, 800, 480), Color.White);
            clouds[1] = new Scrolling(Content.Load<Texture2D>("layers/clouds_2"), new Rectangle(0, 0, 800, 480), Color.White);
            clouds[2] = new Scrolling(Content.Load<Texture2D>("layers/clouds_3"), new Rectangle(0, 0, 800, 480), Color.White);
            clouds[3] = new Scrolling(Content.Load<Texture2D>("layers/clouds_4"), new Rectangle(0, 0, 800, 480), Color.White);

            luke = new Player(Content.Load<Texture2D>("Sprites/Luke"), new Vector2(100, 400), 49, 49);

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
            

            font = Content.Load<SpriteFont>("Fonts/basicFont");

            
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch(CurrentState)
            {
                case GameState.MainMenu:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        CurrentState = GameState.LevelSelect;
                    }
                    break;

                case GameState.LevelSelect:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        CurrentState = GameState.Level1;
                    }
                    break;           
            }

            if (rocksBack1.rectangle.X + rocksBack1.rectangle.Width <= 0)
                rocksBack1.rectangle.X = rocksBack2.rectangle.X + rocksBack2.rectangle.Width;

            if (rocksBack2.rectangle.X + rocksBack2.rectangle.Width <= 0)
                rocksBack2.rectangle.X = rocksBack1.rectangle.X + rocksBack1.rectangle.Width;

            if (rocksFront1.rectangle.X + rocksFront1.rectangle.Width <= 0)
                rocksFront1.rectangle.X = rocksFront2.rectangle.X + rocksFront2.rectangle.Width;

            if (rocksFront2.rectangle.X + rocksFront2.rectangle.Width <= 0)
                rocksFront2.rectangle.X = rocksFront1.rectangle.X + rocksFront1.rectangle.Width;

            luke.Update(gameTime);

            rocksBack1.Update();
            rocksFront1.Update();

            rocksBack2.Update();
            rocksFront2.Update();
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (CurrentState)
            {
                case GameState.MainMenu:
                    GraphicsDevice.Clear(Color.Blue);
                    break;

                case GameState.LevelSelect:
                    GraphicsDevice.Clear(Color.Red);
                    break;

                case GameState.Level1:
                    sky.Draw(spriteBatch);

                    rocksBack1.Draw(spriteBatch);
                    rocksFront1.Draw(spriteBatch);

                    rocksBack2.Draw(spriteBatch);
                    rocksFront2.Draw(spriteBatch);

                    luke.Draw(spriteBatch);

                    foreach (Scrolling cloud in clouds)
                        cloud.Draw(spriteBatch);
                    spriteBatch.DrawString(font, "Current Frame: " + luke.currentFrame, new Vector2(10,10), Color.Purple);
                    spriteBatch.DrawString(font, "Current Row: " + luke.currentRow, new Vector2(10, 25), Color.Purple);
                    spriteBatch.DrawString(font, "Direction: " + luke.direction, new Vector2(10, 40), Color.Purple);
                    break;
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
