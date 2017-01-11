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
            background = Content.Load<Texture2D>("sky");
            //redPlane = Content.Load<Texture2D>("biplanered80");
            //redPosition = new Vector2(1000, 400);
            //redVelocity = new Vector2(-1, 0);
            //bluePlane = Content.Load<Texture2D>("bluebibplane80");
            //bluePosition = new Vector2(0, 200);
            //blueVelocity = new Vector2(1, 0);

            Texture2D bluePlaneImage = Content.Load<Texture2D>("bluebibplane80good");
            Texture2D redPlaneImage = Content.Load<Texture2D>("biplanered80");
            Texture2D redRight = Content.Load<Texture2D>("biplanered80goodRight");
            Texture2D blueLeft = Content.Load<Texture2D>("bluebibplane80goodLEFT");
            Texture2D balloonImage = Content.Load<Texture2D>("balloon - Copy");
            playerOne = new Plane(blueLeft, bluePlaneImage, Constants.planeOneStartPostion, Vector2.Zero, true, null);
            planeList.Add(playerOne);

            playerTwo = new Plane(redPlaneImage, redRight, Constants.planeTwoStartPostion, Vector2.Zero, false, null);
            planeList.Add(playerTwo);

            balloon = new Balloon(balloonImage, new Vector2(300, 300), new Vector2(1, 1));

            //Walls
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

            Random RNG = new Random();
            for (int x = 0; x <= 7; x++)
            {
                balloon = new Balloon(balloonImage, new Vector2(RNG.Next(1,Constants.screenHeight), RNG.Next(1,Constants.screenHeight)), new Vector2(RNG.Next(-4,4)/4, RNG.Next(-4,4)/4));
                balloon.SetSize(45, 70);
                BalloonList[x] = balloon;

            }

        }
        public override void Update(KeyboardState state, MouseState mouse)
        {


            // TODO: Add your update logic here

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


            for (int i = 0; i < planeList.Count; i++)
            {
                planeList[i].Update();
            }
            balloon.MoveRandom();
            //balloon.MoveTo(playerOne.Position);
            
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

            //if (state.IsKeyDown(Keys.A) && !previousState.IsKeyDown(Keys.A))
            //{
            //    playerOne.Left();
            //}
            //if (state.IsKeyDown(Keys.D) && !previousState.IsKeyDown(Keys.D))
            //{
            //    playerOne.Right();
            //}
            if (state.IsKeyDown(Keys.Right))
            {

                playerTwo.right();
            }
            if ((state.IsKeyUp(Keys.Left)) && (state.IsKeyUp(Keys.Right)))
            {
                playerTwo.Stop();
            }

            /*ball.Update(wallList, planeList)*/
            if (mouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton != ButtonState.Pressed)
            {
                playerOne.accelerate(0.5);
            }

            previousState = state;
            previousMouse = mouse;

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 720), Color.White);
            for (int i = 0; i < planeList.Count; i++)
            {
                planeList[i].Draw(spriteBatch);
            }
            for (int i = 0; i < BalloonList.Length; i++)
            {
                BalloonList[i].Draw(spriteBatch);
            }
            //spriteBatch.Draw(redPlane, redPosition);
            //spriteBatch.Draw(bluePlane, bluePosition);

        }
    }
}
