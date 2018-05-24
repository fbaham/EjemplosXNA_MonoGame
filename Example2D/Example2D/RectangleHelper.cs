using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2D
{
    static class RectangleHelper
    {
        const int penetrationMargin = 5;
        public static bool isOnTopOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= r2.Top - penetrationMargin &&
                    r1.Bottom <= r2.Top &&
                    r1.Right >= r2.Left + 5 &&
                    r1.Left <= r2.Right - 5);
        }
    }
}
