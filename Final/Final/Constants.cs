using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final
{
    class Constants
    {
        public static int screenWidth { get { return 1280; } }
        public static int screenHeight { get { return 720; } }

        public static Vector2 planeOneStartPostion { get { return new Vector2(300, 500 / 2); } }
        public static Vector2 planeTwoStartPostion { get { return new Vector2(1000, 250); } }
    }
}
