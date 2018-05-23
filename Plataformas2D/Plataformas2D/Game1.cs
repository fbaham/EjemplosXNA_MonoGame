using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Plataformas2D.GameContent;

namespace Plataformas2D
{
    public class Game1 : Game
    {
        #region Constructor
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 baseScreenSize = new Vector2(800, 480);
        private Matrix globalTransformation;

        private Level level;
        // We store our input states so that we only poll once per frame, 
        // then we use the same input state wherever needed
        private GamePadState gamePadState;
        private KeyboardState keyboardState;
        private TouchCollection touchState;
        private AccelerometerState accelerometerState;

        private VirtualGamePad virtualGamePad;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Accelerometer.Initialize();
        }
        #endregion

        #region Metodos
        protected override void Initialize()
        {
            level = new Level(Services);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Work out how much we need to scale our graphics to fill the screen
            float horScaling = GraphicsDevice.PresentationParameters.BackBufferWidth / baseScreenSize.X;
            float verScaling = GraphicsDevice.PresentationParameters.BackBufferHeight / baseScreenSize.Y;
            Vector3 screenScalingFactor = new Vector3(horScaling, verScaling, 1);
            globalTransformation = Matrix.CreateScale(screenScalingFactor);

            virtualGamePad = new VirtualGamePad(baseScreenSize, globalTransformation, Content.Load<Texture2D>("Sprites/Control/VirtualControlArrow"));
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            HandleInput(gameTime);

            level.Update(gameTime, keyboardState, gamePadState, accelerometerState, Window.CurrentOrientation);

            base.Update(gameTime);
        }

        private void HandleInput(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            touchState = TouchPanel.GetState();
            gamePadState = virtualGamePad.GetState(touchState, GamePad.GetState(PlayerIndex.One));
            accelerometerState = Accelerometer.GetState();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            level.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion
    }
}
