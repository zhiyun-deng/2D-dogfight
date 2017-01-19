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
    class Level5: Level
    {
        
        TimeSpan timeSpan = TimeSpan.FromMilliseconds(0000);
        double finishTime = 0.0;
        Balloon[] balloonList;

        public Level5()
        {
            objective = "For a twist, COOPERATE with your friend so that ONE OF THE PLANES survives the longest!";
            balloonList = new Balloon[8];
        }


        public override void Load(ContentManager Content)
        {
            base.Load(Content);
            trophy.Position = new Vector2(-100, -100);

            


            Texture2D balloonImage = Content.Load<Texture2D>("balloon - Copy");
            Random RNG = new Random();
            for (int x = 0; x <= 7; x++)
            {
                balloon = new Balloon(balloonImage, new Vector2(RNG.Next(1, 1280), RNG.Next(1, 720)), new Vector2(RNG.Next(-4, 4) / 4, RNG.Next(-4, 4) / 4), bulletTex);
                balloon.SetSize(45, 70);
                balloonList[x] = balloon;
                planeList.Add(balloon);

            }



        }




       

        public void Update(KeyboardState state, MouseState mouse,GameTime gameTime)
        {


            base.Update(state, mouse);
            timeSpan += gameTime.ElapsedGameTime;
            gettingResponse = false;
            text = "";
            secondText = "";

            for (int i = 0; i < balloonList.Length; i++)
            {
                balloonList[i].Update(planeList);
                //BalloonList[i].MoveRandom();
                //BalloonList[i].Shoot();
            }

            if (playerOne.Position.Y > Constants.screenHeight && playerTwo.Position.Y > Constants.screenHeight)
            {
                if (finishTime == 0.0)
                {
                    finishTime = Math.Floor(timeSpan.TotalSeconds);
                }
                text = "You manage to hold on for " + finishTime + " seconds!" ;
                secondText = "Press enter close the game.";
                gettingResponse = true;
            }
            

            
            










        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
            if (finishTime == 0.0)
            {
                spriteBatch.DrawString(smallFont, timeSpan.TotalSeconds.ToString(), new Vector2(100, 100), Color.Black);
            }

            for (int i = 0; i < balloonList.Length; i++)
            {
                balloonList[i].Draw(spriteBatch);
            }



        }
    }
}
