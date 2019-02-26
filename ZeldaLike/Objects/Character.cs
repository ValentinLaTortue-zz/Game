using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaLike.Objects
{
    public class Character : GameUtility.GameElement
    {
        int _state = 0;
        public int state
        {
            get { return _state; }
            set
            {
                _state = value;
                var t = regard;
            }
        }
        public float speed = 250;
        public Items.Arme firstHand, secondHand;
        public List<GameUtility.Animation> animations;
        public GameUtility.Animation currentAnimation;
        public int currentFrameIndex = 0;

        Vector2 oldRegard = Vector2.UnitX;
        public Vector2 regard
        {
            get
            {
                Vector2 regard = Vector2.Zero;
                if (state == 1)
                    regard = Vector2.UnitX;
                else if (state == 2)
                    regard = -Vector2.UnitX;
                else if (state == 3)
                    regard = -Vector2.UnitY;
                else if (state == 4)
                    regard = Vector2.UnitY;
                else
                    regard = oldRegard;
                oldRegard = regard;
                return regard;
            }
        }
        public int stateFromRegard
        {
            get
            {
                Vector2 regard1 = regard;
                int state = -1;
                if (regard1 == Vector2.UnitX)
                    state = 1;
                else if (regard1 == -Vector2.UnitX)
                    state = 2;
                else if (regard1 == -Vector2.UnitY)
                    state = 3;
                else if (regard1 == Vector2.UnitY)
                    state = 4;
                Console.WriteLine(state);

                return state;
            }
        }

        public virtual void Attack()
        {
            firstHand.Attack();
        }

        public override void Update(KeyboardState keyboard, MouseState mouse, GameTime gt)
        {
            if(firstHand != null)
                firstHand.Update(keyboard, mouse, gt);
            if (secondHand != null)
                secondHand.Update(keyboard, mouse, gt);
        }
    }
}
