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
        protected List<Plane> planeList;
        protected List<GameObject> wallList;

        protected Texture2D background;
        

        protected Plane playerOne;
        protected Plane playerTwo;

        
        protected KeyboardState previousState;
        protected MouseState previousMouse;
        protected Balloon balloon;
        private SpriteFont font;
        AnimatedClass explosion;

        public Level()
        {
            wallList = new List<GameObject>();

            

            planeList = new List<Plane>();
            previousState = Keyboard.GetState();

            
        }
        public virtual void Load(ContentManager Content)
        {

            font = Content.Load<SpriteFont>("Text");
            background = Content.Load<Texture2D>("sky");
            

            Texture2D texture = Content.Load<Texture2D>("explosion17");
            explosion = new AnimatedClass(texture, 5, 5);

            Texture2D bluePlaneImage = Content.Load<Texture2D>("bluebibplane80");
            Texture2D redPlaneImage = Content.Load<Texture2D>("biplanered80");
            Texture2D redRight = Content.Load<Texture2D>("biplanered80Right");
            Texture2D blueLeft = Content.Load<Texture2D>("bluebibplane80LEFT");
            Texture2D balloonImage = Content.Load<Texture2D>("balloon - Copy");
            playerOne = new Plane(blueLeft, bluePlaneImage, Constants.planeOneStartPostion, Vector2.Zero, true, explosion);
            planeList.Add(playerOne);

            playerTwo = new Plane(redPlaneImage, redRight, Constants.planeTwoStartPostion, Vector2.Zero, false,explosion);
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


            

        }
        public virtual void Update(KeyboardState state, MouseState mouse)
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


            //for (int i = 0; i < planeList.Count; i++)
            //{
            //    planeList[i].Update();
            //}

            playerOne.Update(playerTwo);
            playerTwo.Update(playerOne);
            balloon.MoveRandom();
            //balloon.MoveTo(playerOne.Position);
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
            
            balloon.DrawSize(spriteBatch, 45, 70);

            spriteBatch.DrawString(font, ".", playerTwo.Position, Color.Black);
            spriteBatch.DrawString(font, ".", playerOne.Position, Color.Black);
        }
        
    }
}
