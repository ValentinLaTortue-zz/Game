using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaLike.GameUtility
{
    public class Camera
    {
        public Vector2 position;
        

        public Matrix Update()
        {
            Matrix translation = Matrix.CreateTranslation(fromVector2(position));
            return translation;
        }

        public Vector3 fromVector2(Vector2 vector)
        {
            return new Vector3(vector.X, vector.Y, 0);
        }
    }
}
