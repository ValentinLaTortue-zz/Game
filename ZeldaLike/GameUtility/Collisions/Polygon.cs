using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaLike.GameUtility.Collisions
{
    public class Polygon : GameElement
    {
        public List<Vector2> points = new List<Vector2>();
        List<Segment> _segments;

        public Polygon(List<Vector2> points)
        {
            this.points = points;
        }

        public List<Segment> segments
        {
            get
            {
                if (_segments != null)
                    return _segments;
                else
                {
                    List<Segment> segm = new List<Segment>();
                    for (int i = 0; i < points.Count - 1; i++)
                    {
                        var seg = new Segment(points[i], points[i + 1]);
                        segm.Add(seg);
                    }
                    segm.Add(new Segment(points[points.Count - 1], points[0]));
                    _segments = segm;
                    return segm;
                }
            }
        }


        public static Polygon RectToPolygons(Rectangle rect, float Angle, Vector2 pivot)
        {
            var pivotPT = rect.Location.ToVector2() + new Vector2(rect.Width * pivot.X, rect.Height * pivot.Y);

            List<Vector2> points = new List<Vector2>();
            List<Vector2> pointsFinal = new List<Vector2>();
            points.Add(rect.Location.ToVector2());
            points.Add(rect.Location.ToVector2() + rect.Width * Vector2.UnitX);
            points.Add(rect.Location.ToVector2() + rect.Width * Vector2.UnitX + rect.Height * Vector2.UnitY);
            points.Add(rect.Location.ToVector2() + rect.Height * Vector2.UnitY);

            foreach(var pt in points)
            {
                var rPt = pt - pivotPT;
                Imaginaires.Complex Zpt = new Imaginaires.Complex(rPt.X, rPt.Y);
                Zpt *= new Imaginaires.Complex(1, Angle, false);
                rPt = new Vector2((float)Zpt.Re, (float)Zpt.Im);
                pointsFinal.Add(pivotPT + rPt);
            }
            return new Polygon(pointsFinal);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            /*foreach (var seg in segments)
                seg.Draw(spriteBatch);*/
            segments[0].Draw(spriteBatch);
        }


        public void Translate(Vector2 vect)
        {
            for(int i = 0; i < points.Count; i++)
            {
                points[i] += vect;
            }
            _segments = null;
        }

        public bool Intersect(Polygon poly)
        {
            Console.WriteLine(poly.segments[0].ToString() + "   " + segments[0].ToString());
            if (poly.segments[0].Intersect(segments[0]))
            {
                return true;
            }
            else return false;
        }
    }

    public class Segment : GameElement
    {
        public Vector2 A, B;


        public Segment(Vector2 a, Vector2 b)
        {
            A = a;
            B = b;
        }

        public bool Owned(Vector2 pt)
        {
            Vector2 vd = B - A;
            Vector2 vd2 = pt - A;
            if(Vector2.Normalize(vd) == Vector2.Normalize(vd2) || Vector2.Normalize(vd) == -Vector2.Normalize(vd2))
            {
                return true;
                if(Vector2.Dot(vd, vd2) >= 0)
                {
                    if(vd2.Length() <= vd.Length())
                    { return true; }
                    else
                    { return false; }
                }
                else
                { return false; }
            }
            else
            { return false; }
        }

        public bool Intersect(Segment other)
        {
            var vdir1 = B - A;
            var vdir2 = other.B - other.A;

            if (Tools.staticFunctions.produitMixte(vdir1, vdir2) != 0)
            {
                var a1 = Tools.staticFunctions.produitMixte(vdir1, other.B - A);
                var a2 = Tools.staticFunctions.produitMixte(vdir1, other.A - A);
                var a = a1 * a2;
                var b = Tools.staticFunctions.produitMixte(vdir2, B - other.A) * Tools.staticFunctions.produitMixte(vdir2, A - other.A);


                if (a <= 0 && b <= 0)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public override void Draw(SpriteBatch spriteBacth)
        {
            var vd = Vector2.Normalize(B - A);
            var length = (B - A).Length();
            for(int i = 0; i < length; i++)
            {
                Rectangle rect = new Rectangle((A + vd * i).ToPoint(), new Point(1, 1));
                spriteBacth.Draw(Resources.pixel, rect, Color.White);
            }
        }

        public override string ToString()
        {
            return "A : " + A.ToString() + "  B : " + B.ToString();
        }
    }
}
