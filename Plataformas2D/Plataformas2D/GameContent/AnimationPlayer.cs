using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plataformas2D.GameContent
{
    struct AnimationPlayer
    {
        #region Propiedades
        Animation animation;
        int frameIndex;
        float time;

        public Animation Animation { get => animation; }
        public int FrameIndex { get => frameIndex; }
        public Vector2 Origin { get => new Vector2(Animation.FrameWidth / 2, Animation.FrameHeight / 2); }
        #endregion

        #region Métodos
        public void PlayAnimation(Animation animation)
        {
            if (Animation == animation)
                return;

            this.animation = animation;
            this.frameIndex = 0;
            this.time = 0.0f;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            if (Animation == null)
                throw new NotSupportedException("No hay animación para reproducir");

            time += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (time > Animation.FrameTime)
            {
                frameIndex++;
                time = 0;
                if (frameIndex > Animation.FrameCount)
                {
                    frameIndex = 0;
                }
            }
            Rectangle source = new Rectangle(FrameIndex * Animation.FrameWidth, Animation.FrameRow, Animation.FrameWidth, Animation.FrameHeight);

            spriteBatch.Draw(Animation.Texture, position, source, Color.White, 0f, Origin, 1.0f, spriteEffects, 0);
        }
        #endregion
    }
}

