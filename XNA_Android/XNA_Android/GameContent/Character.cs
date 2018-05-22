using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework.Input;

namespace XNA_Android.GameContent
{
    class Character
    {
        #region Constructor

        Texture2D texture;

        Vector2 position;
        Vector2 velocity;

        bool hasJumped;

        public Character(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;
            hasJumped = true;
        }

        #endregion

        #region Propiedades

        public Texture2D Texture { get => texture; set => texture = value; }
        public Vector2 Position { get => position; set => position = value; }

        #endregion

        #region Metodos

        public void Update(GameTime gameTime)
        {
            position += velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right)) velocity.X = 3f;
            else if (Keyboard.GetState().IsKeyDown(Keys.Left)) velocity.X = -3f;
            else velocity.X = 0f;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        #endregion
    }
}