using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZeldaLike.GameUtility.Scenes
{
    public class CollisionScene : Scene
    {
        public Collisions.Polygon polygon = Collisions.Polygon.RectToPolygons(new Rectangle(50, 25, 100, 100), 0, Vector2.One * 0.25F);
        public Collisions.Polygon polygon2 = Collisions.Polygon.RectToPolygons(new Rectangle(200, 50, 100, 100), 0.1F, Vector2.One * 0.5F);
        //public Collisions.Polygon poly = new Collisions.Polygon(new List<Vector2>() { new Vector2(50, 50), new Vector2(50, 100) });

        /*public Collisions.Segment segm1 = new Collisions.Segment(new Vector2(25, 50), new Vector2(100, 100));
        public Collisions.Segment segm2 = new Collisions.Segment(new Vector2(25, 75), new Vector2(75, 100));*/

        public CollisionScene()
        {
            elements.Add(polygon);
            elements.Add(polygon2);
            /*elements.Add(segm1);
            elements.Add(segm2);*/
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            /*if (segm1.Intersect(segm2))
            {
                spriteBatch.Draw(Resources.pixel, new Rectangle(0, 0, 30, 30), Color.Red);
            }*/

            if(polygon.Intersect(polygon2))
            {
                spriteBatch.Draw(Resources.pixel, new Rectangle(0, 0, 30, 30), Color.Red);
            }
            //spriteBatch.Draw(Resources.pixel, new Rectangle(50, 50, 5, 5), Color.Yellow);
        }

        public override void DrawMenu(SpriteBatch spriteBatch)
        {
            base.DrawMenu(spriteBatch);
        }

        Vector2 oldPos = Vector2.One * 50;

        public override void Update(KeyboardState keyboard, MouseState mouse, GameTime gt)
        {
            base.Update(keyboard, mouse, gt);
            Vector2 pos = mouse.Position.ToVector2();

            polygon.position = mouse.Position.ToVector2();
            polygon.angle = mouse.ScrollWheelValue * 0.001F;

            oldPos = pos;

            //segm2.A = mouse.Position.ToVector2();
        }
    }
}
