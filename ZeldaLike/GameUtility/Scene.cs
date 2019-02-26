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
    public class Scene
    {
        public List<GameElement> elements = new List<GameElement>();


        List<GameElement> elToAdd, elToDel;

        public void AddElement(GameElement element)
        {
            if (elToAdd == null)
                elToAdd = new List<GameElement>();
            elToAdd.Add(element);
        }
        public void DelElement(GameElement element)
        {
            if (elToDel == null)
                elToDel = new List<GameElement>();
            elToDel.Add(element);
        }

        public virtual void Update(KeyboardState keyboard, MouseState mouse, GameTime gt)
        {
            foreach(var element in elements)
            {
                element.Update(keyboard, mouse, gt);
            }
            if(elToAdd != null)
            {
                elements.AddRange(elToAdd);
                elToAdd = null;
            }
            if(elToDel != null)
            {
                foreach(var el in elToDel)
                {
                    elements.Remove(el);
                }
                elToDel = null;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var element in elements)
            {
                element.Draw(spriteBatch);
            }
        }

        public virtual void DrawMenu(SpriteBatch spriteBatch)
        {
            foreach (var element in elements)
            {
                element.DrawMenu(spriteBatch);
            }
        }

        public static void LoadScene(Scene scene)
        {

        }
    }
}
