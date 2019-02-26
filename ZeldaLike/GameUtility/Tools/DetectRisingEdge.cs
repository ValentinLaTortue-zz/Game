using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaLike.GameUtility.Tools
{
    public class DetectRisingEdge
    {
        bool oldState = false;

        public bool get(bool state)
        {
            if(state && !oldState)
            {
                oldState = true;
                return true;
            }
            else
            {
                if (!state && oldState)
                    oldState = false;
                return false;
            }
        }
    }
}
