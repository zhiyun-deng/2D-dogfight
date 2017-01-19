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



        }
        public void Update(KeyboardState state, MouseState mouse,GameTime gameTime)
        {


            base.Update(state, mouse);
            timeSpan += gameTime.ElapsedGameTime;
            gettingResponse = false;
            text = "";
            secondText = "";
            
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

        }
    }
}
