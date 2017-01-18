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
       


        //levellist has the size of the number of levels
        Level[] levelList = new Level[7];
        Level currentLevel;
        

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

            Level startMenu = new StartMenu();
            levelList[0] = startMenu;

            Level instructionScreen = new InstructionScreen();
            levelList[1] = instructionScreen;

            Level one = new Level();
            levelList[2] = one;
            
            Level2 two = new Level2();
            levelList[3] = two;

            Level3 three = new Level3();
            levelList[4] = three;

            Level4 four = new Level4();
            levelList[5] = four;

            Level5 five = new Level5();
            levelList[6] = five;
            
            currentLevel = levelList[0];
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
            foreach (Level level in levelList)
            {
                level.Load(Content); 
            }
            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
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
                MouseState mouse = Mouse.GetState();

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                if (GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                if (currentLevel.Done == true)
                {
                    if (currentLevel == levelList[levelList.Length - 1])
                    {
                        Exit();
                    }
                    else
                    {
                        currentLevel = levelList[Array.IndexOf(levelList, currentLevel) + 1];
                    }
                }

                if (currentLevel is Level5)
                {
                    ((Level5)currentLevel).Update(state, mouse,gameTime); 
                }
                else
                {
                    currentLevel.Update(state, mouse);
                }



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
            

            
            

            currentLevel.Draw(spriteBatch);
            

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        
    }
}
