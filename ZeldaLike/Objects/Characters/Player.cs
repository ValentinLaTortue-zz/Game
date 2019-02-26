using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZeldaLike.Objects.Characters
{
    public class Player : Character
    {
        List<Item> inventory = new List<Item>();
        int oldState = 0;

        public float scale = 2.5f;


        // To Sync :
        public int Skin = 0;
        public Vector2 pos
        {
            get
            {
                return aff.Location.ToVector2();
            }
            set
            {
                aff.Location = value.ToPoint();
            }
        }

        // currentAnimationInt
        // currentFrame


        // End

        public Player()
        {
            aff = new Rectangle(10, 10, 28, 32);
            firstHand = new Items.Armes.Epee(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (currentAnimation != null)
            {
                ReadFrame(spriteBatch);
            }
            else
            {
                currentAnimation = GameUtility.Resources.player[Skin].animations[0];
            }
        }

        /*public void Draw(SpriteBatch spriteBatch, Rectangle source)
        {
            spriteBatch.Draw(GameUtility.Resources.player[Skin].texture, aff, source, Color.White);
        }*/

        #region Animations

        public void ReadFrame(SpriteBatch spriteBatch)
        {
            if(animations == null)
                animations = GameUtility.Resources.player[Skin].animations;

            currentFrameIndex %= currentAnimation.frames.Count;
            GameUtility.Frame currentFrame = GameUtility.Resources.player[Skin].frames[currentAnimation.frames[currentFrameIndex]];
            aff.Size = (scale * currentFrame.size).ToPoint();
            spriteBatch.Draw(GameUtility.Resources.player[Skin].texture, aff, currentFrame.rect, Color.White, (currentFrame.rotate) ? -(float)Math.PI / 2 : 0, currentFrame.size * currentFrame.pPos, SpriteEffects.None, 0);
        }

        #endregion
        

        public override void DrawMenu(SpriteBatch spriteBatch)
        {
            base.DrawMenu(spriteBatch);
        }

        GameUtility.Tools.Timer timer = new GameUtility.Tools.Timer(0.2d);
        GameUtility.Tools.DetectRisingEdge attack = new GameUtility.Tools.DetectRisingEdge();
        public override void Update(KeyboardState keyboard, MouseState mouse, GameTime gt)
        {
            base.Update(keyboard, mouse, gt);

            if (state <= 4)
            {
                Vector2 direction = Vector2.Zero;

                if (keyboard.IsKeyDown(Keys.D))
                {
                    direction.X = 1;
                    state = 1;
                }
                else if (keyboard.IsKeyDown(Keys.Q))
                {
                    direction.X = -1;
                    state = 2;
                }
                else if(oldState <= 4)
                {
                    state = 0;
                }

                if (keyboard.IsKeyDown(Keys.Z))
                {
                    direction.Y = -1;
                    state = 3;
                }
                else if (keyboard.IsKeyDown(Keys.S))
                {
                    direction.Y = 1;
                    state = 4;
                }


                if (direction != Vector2.Zero)
                    direction = Vector2.Normalize(direction);

                if (attack.get(keyboard.IsKeyDown(Keys.Space)))
                {
                    firstHand.Attack();
                }

                pos += speed * (float)gt.ElapsedGameTime.TotalSeconds * direction;


                if(oldState != state)
                {
                    oldState = state;
                    currentFrameIndex = 0;

                    currentAnimation = getAnimFromState(state);
                }
                

                if (state != 0)
                {
                    if (timer.get(gt))
                        currentFrameIndex++;
                }
            }
        }

        GameUtility.Animation getAnimFromState(int state)
        {
            Console.WriteLine("getting animation");
            GameUtility.Animation anim = currentAnimation;
            switch (state)
            {
                case 1:
                    anim = GameUtility.Animation.find("walkRight", GameUtility.Resources.player[Skin].animations);
                    break;
                case 2:
                    anim = GameUtility.Animation.find("walkLeft", GameUtility.Resources.player[Skin].animations);
                    break;
                case 3:
                    anim = GameUtility.Animation.find("walkUp", GameUtility.Resources.player[Skin].animations);
                    break;
                case 4:
                    anim = GameUtility.Animation.find("walkDown", GameUtility.Resources.player[Skin].animations);
                    break;
            }
            return anim;
        }
    }
}
