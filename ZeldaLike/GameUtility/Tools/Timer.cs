using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaLike.GameUtility.Tools
{
    public class Timer
    {
        public bool enabled = true;
        public double currentTime = 0;
        double _interval = 0;
        public float percent = 0;
        public double interval
        {
            get
            {
                return _interval;
            }
            set
            {
                _interval = value;
                currentTime = _interval;
            }
        }
        
        public Timer(double interval)
        {
            this.interval = interval;
        }

        public bool get(GameTime gt)
        {
            currentTime -= gt.ElapsedGameTime.TotalSeconds;
            percent = (float)(currentTime / interval);
            if(currentTime <= 0)
            {
                currentTime = _interval;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
