using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Final
{
    class Level2 : Level
    {
        Balloon[] BalloonList;
        

        public Level2()
        {
            BalloonList = new Balloon[3];
        }
        public override void Load(ContentManager Content)
        {
            base.Load(Content);
            Texture2D balloonImage = Content.Load<Texture2D>("balloon - Copy");
            balloon = new Balloon(balloonImage, new Vector2(200,200), new Vector2(0,0), bulletTex);
            balloon.SetSize(45, 70);
            BalloonList[0] = balloon;
            planeList.Add(balloon);
            balloon = new Balloon(balloonImage, new Vector2(600, 400), new Vector2(0, 0), bulletTex);
            balloon.SetSize(45, 70);
            BalloonList[1] = balloon;
            planeList.Add(balloon);
            balloon = new Balloon(balloonImage, new Vector2(800, 100), new Vector2(0, 0), bulletTex);
            balloon.SetSize(45, 70);
            BalloonList[2] = balloon;
            planeList.Add(balloon);



        }
        public override void Update(KeyboardState state, MouseState mouse)
        {


            base.Update(state, mouse);
            for (int i = 0; i < BalloonList.Length; i++)
            {
                BalloonList[i].Update(planeList);
                //BalloonList[i].MoveRandom();
                //BalloonList[i].Shoot();
            }









            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            for (int i = 0; i < BalloonList.Length; i++)
            {
                BalloonList[i].Draw(spriteBatch);
            }

        }
    }
}
