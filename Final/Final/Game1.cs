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
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
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


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;   // Width of screen
            graphics.PreferredBackBufferHeight = 720;   // Height of screen
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            wallList = new List<GameObject>();

            //planeList = new List<GameObject>();

            planeList = new List<Plane>();
            previousState = Keyboard.GetState();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("sky");
            //redPlane = Content.Load<Texture2D>("biplanered80");
            //redPosition = new Vector2(1000, 400);
            //redVelocity = new Vector2(-1, 0);
            //bluePlane = Content.Load<Texture2D>("bluebibplane80");
            //bluePosition = new Vector2(0, 200);
            //blueVelocity = new Vector2(1, 0);

            Texture2D redPlaneImage = Content.Load<Texture2D>("bluebibplane80");
            Texture2D bluePlaneImage = Content.Load<Texture2D>("biplanered80");
            playerOne = new Plane(redPlaneImage, Constants.planeOneStartPostion, Vector2.Zero, true);
            planeList.Add(playerOne);

            playerTwo = new Plane(bluePlaneImage, Constants.planeTwoStartPostion, Vector2.Zero, false);
            planeList.Add(playerTwo);

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

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            {
                KeyboardState state = Keyboard.GetState();

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                if (GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                // TODO: Add your update logic here

                // PlayerOne Controls

                if (state.IsKeyDown(Keys.W) && !previousState.IsKeyDown(Keys.W))
                {
                    playerOne.Up();
                }

                //if (state.IsKeyDown(Keys.A) && !previousState.IsKeyDown(Keys.A))
                //{
                //    playerOne.Left();
                //}
                //if (state.IsKeyDown(Keys.D) && !previousState.IsKeyDown(Keys.D))
                //{
                //    playerOne.Right();
                //}
                if (state.IsKeyDown(Keys.S) && !previousState.IsKeyDown(Keys.S))
                {

                    playerOne.Down();
                }
                if ((state.IsKeyUp(Keys.W)) && (state.IsKeyUp(Keys.S) && (state.IsKeyUp(Keys.A) && (state.IsKeyUp(Keys.D)))))
                {
                    playerOne.Stop();
                }


                for (int i = 0; i < planeList.Count; i++)
                {
                    planeList[i].Update();
                }



                playerOne.Update();
                playerTwo.Update();



                //player two controls


                if (state.IsKeyDown(Keys.Up) && !previousState.IsKeyDown(Keys.Up))
                {
                    playerTwo.Up();
                }
                ////if (state.IsKeyDown(Keys.Left) && !previousState.IsKeyDown(Keys.Left))
                ////{
                ////    playerTwo.Left();
                ////}
                ////if (state.IsKeyDown(Keys.Right) && !previousState.IsKeyDown(Keys.Right))
                ////{
                ////    playerTwo.Right();
                ////}
                if (state.IsKeyDown(Keys.Down) && !previousState.IsKeyDown(Keys.Down))
                {
                    playerTwo.Down();
                }
                //if ((state.IsKeyUp(Keys.Up)) && (state.IsKeyUp(Keys.Down)) && (state.IsKeyUp(Keys.Left) && (state.IsKeyUp(Keys.Right))))
                //{
                //    playerTwo.Stop();
                //}

                //for (int i = 0; i < planeList.Count; i++)
                //{
                //    planeList[i].Update(wallList);
                //}

                /*ball.Update(wallList, planeList)*/;

                previousState = state;

                base.Update(gameTime);
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 720), Color.White);


            for (int i = 0; i < planeList.Count; i++)
            {
                planeList[i].Draw(spriteBatch);
            }
            //spriteBatch.Draw(redPlane, redPosition);
            //spriteBatch.Draw(bluePlane, bluePosition);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
