using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaLike.GameUtility
{
    public class GameElement
    {

        public Rectangle aff = new Rectangle();
        public Rectangle hitbox = new Rectangle();
        public bool useNativeAff = false;


        public virtual void Update(KeyboardState keyboard, MouseState mouse, GameTime gt)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void DrawMenu(SpriteBatch spriteBatch)
        {

        }

        public void Destroy()
        {
            
        }
    }
}
