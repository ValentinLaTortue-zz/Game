using Imaginaires;
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
        Vector2 _position;
        public Vector2 position {
            set {
                _position = value;
                ResetWorldPoint();
            }
            get { return _position; }
        }
        float _angle;
        public float angle
        {
            set
            {
                _angle = value;
                ResetWorldPoint();
            }
            get { return _angle; }
        }

        void ResetWorldPoint()
        {
            _worldPosition = null;
            _segments = null;
            _relativePosition = null;
        }

        List<Vector2> initRelPos;
        List<Vector2> _relativePosition;
        public List<Vector2> relativePosition {
            get
            {
                if(_relativePosition == null)
                {
                    _relativePosition = new List<Vector2>();
                    foreach (var relPos in initRelPos)
                    {
                        var posComp = new Complex(relPos.X, relPos.Y);
                        posComp *= new Complex(1, angle, false);

                        _relativePosition.Add(new Vector2((float)posComp.Re, (float)posComp.Im));
                    }
                }
                return _relativePosition;
            }
        }
        List<Vector2> _worldPosition;

        public List<Vector2> worldPosition { get
            {
                if (_worldPosition == null) { 
                    _worldPosition = new List<Vector2>();
                    foreach(var relPos in relativePosition)
                    {
                        _worldPosition.Add(position + relPos);
                    }
                }
                return _worldPosition;
            }
        }

        List<Segment> _segments;

        public Polygon(List<Vector2> relativePointsPosition)
        {
            Console.WriteLine(relativePointsPosition.Count);
            initRelPos = relativePointsPosition;
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
                    List<Vector2> points = worldPosition;
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

            points.Add(-pivot * rect.Size.ToVector2());
            points.Add(-pivot * rect.Size.ToVector2() + rect.Width * Vector2.UnitX);
            points.Add(-pivot * rect.Size.ToVector2() + rect.Height * Vector2.UnitY);
            
            var poly = new Polygon(points);
            poly.angle = Angle;
            poly.position = rect.Location.ToVector2() + 0.5F * rect.Size.ToVector2();

            return poly;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var seg in segments)
                seg.Draw(spriteBatch);
        }

        public bool Intersect(Polygon poly)
        {
            foreach(Segment segm1 in segments)
            {
                foreach(Segment segm2 in poly.segments)
                {
                    if (segm1.Intersect(segm2))
                        return true;
                }
            }
            return false;
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
