﻿using System;
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
    class Level
    {
        //stores all moving objects
        protected List<GameObject> planeList;


        //store walls, will not destroy plane
        protected List<GameObject> wallList;

        //the red block

        protected Texture2D background;
        

        protected Plane playerOne;
        protected Plane playerTwo;

        protected GameObject trophy;
        
        protected KeyboardState previousState;
        protected MouseState previousMouse;
        protected Balloon balloon;
        private SpriteFont font;
        AnimatedClass explosion;

        string text = "";
        string secondText = "";

        bool gettingResponse = false;

        private bool done = false;
        public bool Done
        {
            get
            {
                return done;
            }
            set
            {
                done = value;
            }
        }


        public Level()
        {
            wallList = new List<GameObject>();

            

            planeList = new List<GameObject>();
            previousState = Keyboard.GetState();

            
        }
        public virtual void Load(ContentManager Content)
        {

            //load font for text
            font = Content.Load<SpriteFont>("Text");
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

            //initializing planes, balloons
            playerOne = new Plane(blueLeft, bluePlaneImage, Constants.planeOneStartPostion, Vector2.Zero, true, explosion);
            planeList.Add(playerOne);

            playerTwo = new Plane(redPlaneImage, redRight, Constants.planeTwoStartPostion, Vector2.Zero, false,explosion);
            planeList.Add(playerTwo);

            

            balloon = new Balloon(balloonImage, new Vector2(400,400), new Vector2(1, 1));
            
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
            wall = new GameObject(wallImage, new Vector2(550, 600));
            wall.SetSize(100, 100);
            wallList.Add(wall);

            //trophy
            Texture2D trophyImage = Content.Load<Texture2D>("Golden Vector Trophy");
            trophy = new GameObject(trophyImage, new Vector2(550, 600));
            trophy.SetSize(50, 100);


        }
        public virtual void Update(KeyboardState state, MouseState mouse)
        {


            // TODO: Add your update logic here

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


            //for (int i = 0; i < planeList.Count; i++)
            //{
            //    planeList[i].Update();
            //}

            playerOne.Update(wallList, planeList);
            playerTwo.Update(wallList, planeList);

            
            balloon.MoveRandom();
            balloon.MoveTo(playerOne.Position);
            balloon.Update();







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
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 720), Color.White);
            for (int i = 0; i < planeList.Count; i++)
            {
                planeList[i].Draw(spriteBatch);
            }
            for (int i = 0; i < wallList.Count; i++)
            {
                wallList[i].Draw(spriteBatch);
            }

            trophy.Draw(spriteBatch);
            spriteBatch.DrawString(font, text, new Vector2(400, 400), Color.WhiteSmoke);
            spriteBatch.DrawString(font, secondText, new Vector2(400, 450), Color.WhiteSmoke);



        }
        public bool Ended()
        {
            return done;
        }
        
        
    }
}
