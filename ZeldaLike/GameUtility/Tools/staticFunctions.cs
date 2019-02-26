using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaLike.GameUtility.Tools
{
    class staticFunctions
    {
        public static bool pointInRect(Rectangle rect, Vector2 vect)
        {
            if (rect.Location.X < vect.X &&
                rect.Location.Y < vect.Y &&
                rect.Location.X + rect.Size.X > vect.X &&
                rect.Location.Y + rect.Size.Y > vect.Y)
                return true;
            else
                return false;
        }

        public static float produitMixte(Vector2 a, Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }
    }
}
