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
    class Level

        //This is the first real level, with basic features. All other levels inherit from it. 
    {
        //stores all moving objects, including BALLOON AND PLANE
        protected List<GameObject> planeList;
        //planeList properties
        public List<GameObject> PlaneList
        {
            get
            {
                return planeList;
            }
        }

        
        //store walls, or any other objects that will not destroy plane(such as blimp)
        protected List<GameObject> wallList;
        public List<GameObject> WallList
        {
            get
            {
                return wallList;
            }
        }

        
        
        protected Texture2D background;//background image
        

        protected Plane playerOne;//blue blane
        protected Plane playerTwo;//red plane

        protected GameObject trophy;
        
        protected KeyboardState previousState;//
        protected MouseState previousMouse;

        protected Balloon balloon; //red balloon, not used in this level, but it is inherited
        
        protected SpriteFont font;
        protected SpriteFont smallFont;
        protected AnimatedClass explosion;
        protected Texture2D bulletTex;
        protected Texture2D heartTex;
        protected string text = "";
        protected string secondText = "";
        protected string objective = "Compete with the other plane: The one who touches the trophy wins!";
        //timer initialization 


        protected bool gettingResponse = false;

        protected bool done = false;
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
            smallFont = Content.Load<SpriteFont>("SmallText");

            //load background
            background = Content.Load<Texture2D>("sky");
            
            //load explosion effect
            Texture2D texture = Content.Load<Texture2D>("explosion17");
            explosion = new AnimatedClass(texture, 5, 5);

            ////load blimp image
            //Texture2D blimpImage = Content.Load<Texture2D>("Blimp");

            //load plane images
            Texture2D bluePlaneImage = Content.Load<Texture2D>("bluebibplane80good");
            Texture2D redPlaneImage = Content.Load<Texture2D>("biplanered80");
            Texture2D redRight = Content.Load<Texture2D>("biplanered80goodRight");
            Texture2D blueLeft = Content.Load<Texture2D>("bluebibplane80goodLEFT");
            Texture2D balloonImage = Content.Load<Texture2D>("balloon - Copy");
            bulletTex = Content.Load<Texture2D>("bulletgood");
            heartTex = Content.Load<Texture2D>("heart");


            //initializing planes, balloons
            playerOne = new Plane(blueLeft, bluePlaneImage, Constants.planeOneStartPostion, Vector2.Zero, true, explosion, bulletTex,heartTex);
            planeList.Add(playerOne);
            

            playerTwo = new Plane(redPlaneImage, redRight, Constants.planeTwoStartPostion, Vector2.Zero, false,explosion,bulletTex,heartTex);
            planeList.Add(playerTwo);

             

            

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

            


           

            playerOne.Update(wallList, planeList);
            playerTwo.Update(wallList, planeList);

            
            
            //balloon.MoveTo(playerOne.Position);
            


          




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



            if (playerOne.Position.Y > Constants.screenHeight && playerTwo.Position.Y > Constants.screenHeight)
            {
                text = "Ah! Too bad!";
                secondText = "Press enter to go to next level.";
                gettingResponse = true;
            }
            if (trophy.IsCollide(playerOne)&&playerOne.Health!=0)
            {
                text = "Blue plane won!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
                secondText = "Press enter to go to next level";
                gettingResponse = true;
            }

            if (playerOne.Position.Y > Constants.screenHeight  && playerTwo.Health != 0)
            {
                text = "Red plane won!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
                secondText = "Press enter to go to next level";
                gettingResponse = true;

            }

            if (playerTwo.Position.Y > Constants.screenHeight && playerOne.Health != 0)
            {
                text = "Blue plane won!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
                secondText = "Press enter to go to next level";
                gettingResponse = true;

            }
            else if (trophy.IsCollide(playerTwo) && playerTwo.Health != 0)
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
            spriteBatch.DrawString(smallFont, "Level Objective: "+objective, new Vector2(0, -5), Color.Black);



        }
        public bool Ended()
        {
            return done;
        }
        
        
        
    }
}
