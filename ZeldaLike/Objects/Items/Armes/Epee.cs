using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZeldaLike.Objects.Items.Armes
{
    public class Epee : Arme
    {
        bool isAttcking = false;

        GameUtility.Tools.Timer anim = new GameUtility.Tools.Timer(0.005d);
        int stateOfAttack = 0;

        public Epee(Character Owner)
        {
            anim.enabled = false;
            this.Owner = Owner;
        }

        public override void Attack()
        {
            if (!isAttcking)
            {
                isAttcking = true;
                anim.enabled = true;
                anim.interval = 0.075F;
                Owner.state = 5;
                Owner.currentAnimation = GameUtility.Animation.find("slice" + GameUtility.Animation.getDirStringFromVector2(Owner.regard), Owner.animations);
                stateOfAttack = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void DrawMenu(SpriteBatch spriteBatch)
        {
            base.DrawMenu(spriteBatch);
        }

        public override void Update(KeyboardState keyboard, MouseState mouse, GameTime gt)
        {
            base.Update(keyboard, mouse, gt);
            if (isAttcking)
            {
                if (anim.get(gt))
                {
                    if (stateOfAttack >= 2)
                    {
                        Owner.state = Owner.stateFromRegard;
                        Owner.currentFrameIndex = 0;
                        anim.enabled = false;
                        isAttcking = false;
                    }
                    if (stateOfAttack < 2)
                    {
                        stateOfAttack++;
                        Owner.currentFrameIndex++;
                    }
                }
            }
        }
    }
}
