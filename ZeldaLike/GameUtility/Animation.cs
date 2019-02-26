using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ZeldaLike.GameUtility
{

    public class TextureObject
    {
        public Texture2D texture;
        public List<Frame> frames = new List<Frame>();

        List<Animation> _animations = new List<Animation>();
        public List<Animation> animations { get
            {
                if (animationLoaded)
                    return _animations;
                else
                    throw new Exception("No animations loaded !");
            }
        }

        public TextureObject(Texture2D texture, List<Frame> frames)
        {
            this.texture = texture;
            this.frames = frames;
        }

        bool animationLoaded = false;
        public TextureObject(Texture2D texture, List<Frame> frames, List<Animation> animations)
        {
            animationLoaded = true;
            _animations = animations;
            this.texture = texture;
            this.frames = frames;
        }
    }

    public class Animation
    {
        public string name;
        public List<int> frames = new List<int>();

        public Animation(string name, List<int> frames)
        {
            this.name = name;
            this.frames = frames;
        }

        static string finded = "";
        public static Animation find(string name, List<Animation> animations)
        {
            finded = name;
            return animations.Find(searchPred);
        }

        static bool searchPred(Animation anim)
        {
            if (anim.name == finded)
                return true;
            else
                return false;
        }

        public static string getDirStringFromVector2(Vector2 dir)
        {
            if (dir == Vector2.UnitX)
                return "Right";
            else if (dir == -Vector2.UnitX)
                return "Left";
            else if (dir == -Vector2.UnitY)
                return "Up";
            else if (dir == Vector2.UnitY)
                return "Down";
            else
                return "";
        }

        public static List<Animation> loadXML(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlNode main = doc.SelectSingleNode("Animations");

            List<Animation> animations = new List<Animation>();

            foreach(XmlNode animation in main.ChildNodes)
            {
                string name = animation.Attributes["name"].InnerText;
                List<int> frames = new List<int>();

                foreach(XmlNode frame in animation.ChildNodes)
                {
                    frames.Add(Convert.ToInt32(frame.Attributes["n"].InnerText));
                }

                animations.Add(new Animation(name, frames));
            }

            return animations;
        }
    }

    public class Frame
    {
        public Vector2 pos;
        public Vector2 size;
        public Vector2 pPos;
        public Rectangle rect
        {
            get
            {
                return new Rectangle(pos.ToPoint(), size.ToPoint());
            }
        }
        public bool rotate;

        public Frame(Vector2 pos, Vector2 size, Vector2 pPos, bool rotate)
        {
            this.pos = pos;
            this.size = size;
            this.pPos = pPos;
            this.rotate = rotate;
        }

        public static List<Frame> loadXML(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlNode main = doc.SelectSingleNode("TextureAtlas");
            List<Frame> frames = new List<Frame>();
            foreach(XmlNode frame in main.ChildNodes)
            {
                Vector2 pos = new Vector2(
                    Convert.ToInt32(frame.Attributes["x"].InnerText),
                    Convert.ToInt32(frame.Attributes["y"].InnerText));
                Vector2 size = new Vector2(
                    Convert.ToInt32(frame.Attributes["w"].InnerText), 
                    Convert.ToInt32(frame.Attributes["h"].InnerText));

                Vector2 pPos = new Vector2(
                    (float)Parse(frame.Attributes["pX"].InnerText), 
                    (float)Parse(frame.Attributes["pY"].InnerText));

                bool rotate = false;
                var rot = frame.Attributes["r"];

                if (rot != null && rot.InnerText == "y")
                    rotate = true;

                frames.Add(new Frame(pos, size, pPos, rotate));
            }
            Console.WriteLine(frames.Count);
            return frames;
        }

        static double Parse(string text)
        {
            var splited = text.Split('.');
            var integerPart = double.Parse(splited[0]);
            var decPart = double.Parse(splited[1]);
            while(decPart > 1)
            {
                decPart /= 10;
            }
            return integerPart + decPart;
        }
    }
}
