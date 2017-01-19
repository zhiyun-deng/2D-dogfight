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
    class Level4 : Level
    {
        Balloon[] BalloonList;

        //Level Four of the Game

        public Level4()
        {
            objective = "Compete With the Other Plane: Shoot Down Your Opponent To Win!";
            BalloonList = new Balloon[0];
        }

        //Loads Objects
        public override void Load(ContentManager Content)
        {
            base.Load(Content);
            Texture2D balloonImage = Content.Load<Texture2D>("balloon - Copy");
            Random RNG = new Random();
            for (int x = 0; x < 0; x++)
            {
                balloon = new Balloon(balloonImage, new Vector2(RNG.Next(1, Constants.screenHeight), RNG.Next(1, Constants.screenHeight)), new Vector2(RNG.Next(-4, 4) / 4, RNG.Next(-4, 4) / 4), bulletTex);
                balloon.SetSize(45, 70);
                BalloonList[x] = balloon;
                planeList.Add(balloon);

            }
            trophy.Position = (new Vector2(-100, -100));
            playerOne.Health = 5;
            playerTwo.Health = 5;

        }
        public override void Update(KeyboardState state, MouseState mouse)
        {


            base.Update(state, mouse);
            for (int i = 0; i < BalloonList.Length; i++)
            {
                BalloonList[i].Update();
                BalloonList[i].MoveRandom();
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