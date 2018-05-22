using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Plataformas2D.GameContent
{
    class Animation
    {
        #region Constructor

        readonly Texture2D texture;
        readonly int frameWidth;
        readonly int frameHeight;
        readonly int frameCount;
        readonly int frameRow;
        readonly float frameTime;
        
        public Animation(Texture2D texture, int frameHeight, int frameWidth, int frameCount, int frameRow, float frameTime)
        {
            this.texture = texture;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameRow = frameRow;
            this.frameTime = frameTime;
        }

        #endregion

        #region Propiedades

        public Texture2D Texture { get => texture;  }
        public int FrameWidth { get => frameWidth; }
        public int FrameHeight { get => frameHeight; }
        public int FrameCount { get => frameCount; }
        public int FrameRow { get => frameRow * frameHeight; }
        public float FrameTime { get => frameTime; }

        #endregion

    }
}
