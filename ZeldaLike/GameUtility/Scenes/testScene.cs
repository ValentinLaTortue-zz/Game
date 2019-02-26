using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaLike.GameUtility.Scenes
{
    public class testScene : Scene
    {
        public testScene()
        {
            elements.Add(new Objects.Characters.Player());
        }
    }
}
