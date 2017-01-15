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
            BalloonList = new Balloon[8];
        }
        public override void Load(ContentManager Content)
        {
            //load font for text
            font = Content.Load<SpriteFont>("Text");
            smallFont = Content.Load<SpriteFont>("SmallText");

            //load background
            background = Content.Load<Texture2D>("sky");

            //load explosion effect
            Texture2D texture = Content.Load<Texture2D>("explosion17");
            explosion = new AnimatedClass(texture, 5, 5);



            //load plane images
            Texture2D bluePlaneImage = Content.Load<Texture2D>("bluebibplane80good");
            Texture2D redPlaneImage = Content.Load<Texture2D>("biplanered80");
            Texture2D redRight = Content.Load<Texture2D>("biplanered80goodRight");
            Texture2D blueLeft = Content.Load<Texture2D>("bluebibplane80goodLEFT");
            Texture2D balloonImage = Content.Load<Texture2D>("balloon - Copy");
            bulletTex = Content.Load<Texture2D>("bulletgood");

            //initializing planes, balloons
            playerOne = new Plane(blueLeft, bluePlaneImage, Constants.planeOneStartPostion, Vector2.Zero, true, explosion, bulletTex);
            planeList.Add(playerOne);

            playerTwo = new Plane(redPlaneImage, redRight, Constants.planeTwoStartPostion, Vector2.Zero, false, explosion, bulletTex);
            planeList.Add(playerTwo);



            balloon = new Balloon(balloonImage, new Vector2(400, 400), new Vector2(1, 1));

            balloon.SetSize(45, 75);

            planeList.Add(balloon);

            //Horizontal Walls
            Texture2D wallImage = Content.Load<Texture2D>("Border1280");
            GameObject wall = new GameObject(wallImage, Vector2.Zero);

            wallList.Add(wall);

            wall = new GameObject(wallImage, new Vector2(0, Constants.screenHeight - wallImage.Height));

            wallList.Add(wall);

            // Side Walls

            wallImage = Content.Load<Texture2D>("Border720");

            wall = new GameObject(wallImage, Vector2.Zero);

            wallList.Add(wall);

            wall = new GameObject(wallImage, new Vector2(Constants.screenWidth - wallImage.Width, 0));

            wallList.Add(wall);



            //obstacles


            //trophy
            Texture2D trophyImage = Content.Load<Texture2D>("Golden Vector Trophy");
            trophy = new GameObject(trophyImage, new Vector2(550, 600));
            trophy.SetSize(50, 100);
            Random RNG = new Random();
            for (int x = 0; x <= 7; x++)
            {
                balloon = new Balloon(balloonImage, new Vector2(RNG.Next(1,Constants.screenHeight), RNG.Next(1,Constants.screenHeight)), new Vector2(RNG.Next(-4,4)/4, RNG.Next(-4,4)/4));
                balloon.SetSize(45, 70);
                BalloonList[x] = balloon;
                planeList.Add(balloon);

            }
            

        }
        public override void Update(KeyboardState state, MouseState mouse)
        {


            if (gettingResponse)
            {
                if (state.IsKeyDown(Keys.Enter))
                {
                    done = true;
                }
                else
                {
                    return;
                }
            }

            // PlayerOne Controls

            if (state.IsKeyDown(Keys.A))
            {
                playerOne.left();

            }

            //if (state.IsKeyDown(Keys.A) && !previousState.IsKeyDown(Keys.A))
            //{
            //    playerOne.Left();
            //}
            //if (state.IsKeyDown(Keys.D) && !previousState.IsKeyDown(Keys.D))
            //{
            //    playerOne.Right();
            //}
            if (state.IsKeyDown(Keys.D))
            {

                playerOne.right();
            }
            if ((state.IsKeyUp(Keys.A)) && (state.IsKeyUp(Keys.D)))
            {
                playerOne.Stop();
            }

            if (state.IsKeyDown(Keys.S))
            {

                playerOne.Shoot();
            }




            //for (int i = 0; i < planeList.Count; i++)
            //{
            //    planeList[i].Update();
            //}

            playerOne.Update(wallList, planeList);
            playerTwo.Update(wallList, planeList);
            for (int i = 0; i < BalloonList.Length; i++)
            {
                BalloonList[i].Update();
                BalloonList[i].MoveRandom();
            }









            //player two controls


            if (state.IsKeyDown(Keys.Left))
            {
                playerTwo.left();
            }


            if (state.IsKeyDown(Keys.Right))
            {

                playerTwo.right();
            }
            if ((state.IsKeyUp(Keys.Left)) && (state.IsKeyUp(Keys.Right)))
            {
                playerTwo.Stop();
            }

            if (state.IsKeyDown(Keys.Down))
            {

                playerTwo.Shoot();
            }


            if (mouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton != ButtonState.Pressed)
            {
                playerOne.accelerate(0.5);
            }



            if (playerOne.Position.Y > Constants.screenHeight || playerTwo.Position.Y > Constants.screenHeight)
            {
                text = "Ah! Too bad!";
                secondText = "Press enter to go to next level.";
                gettingResponse = true;
            }
            if (trophy.IsCollide(playerOne))
            {
                text = "Blue plane won!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
                secondText = "Press enter to go to next level";
                gettingResponse = true;
            }
            else if (trophy.IsCollide(playerTwo))
            {
                text = "Red plane won!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
                secondText = "Press enter to go to next level";
                gettingResponse = true;
            }




            

            previousState = state;
            previousMouse = mouse;

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
