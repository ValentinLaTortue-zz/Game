using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaLike.GameUtility
{
    public class Resources
    {
        public static List<TextureObject> player = new List<TextureObject>();
        public static Texture2D pixel;

        public static void loadTextures(ContentManager manager)
        {
            player.Add(new TextureObject(
                    manager.Load<Texture2D>("Player\\skin1\\texture"),
                    Frame.loadXML("Content\\Player\\skin1\\data.xml"),
                    Animation.loadXML("Content\\Player\\skin1\\anims.xml")
                ));

            pixel = manager.Load<Texture2D>("Pixel");
        }
    }
}
