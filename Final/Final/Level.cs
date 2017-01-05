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
    class Level
    {
        List<Plane> planeList;
        List<GameObject> wallList;

        private Texture2D background;
        //Texture2D redPlane;
        //Vector2 redPosition;
        //Vector2 redVelocity;

        Plane playerOne;
        Plane playerTwo;

        //Texture2D bluePlane;
        //Vector2 bluePosition;
        //Vector2 blueVelocity;
        KeyboardState previousState;
        MouseState previousMouse;
        Balloon balloon;
        public Level()
        {
            wallList = new List<GameObject>();

            //planeList = new List<GameObject>();

            planeList = new List<Plane>();
            previousState = Keyboard.GetState();
            
        }
        public virtual void Load()
        {

        }
        public virtual void Update()
        {

        }
        public virtual void Draw(SpriteBatch sprite)
        {

        }
    }
}
