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
        Level[] levelList = new Level[7]; // creates a list for the levels with 7 elements
        Level currentLevel; // sets the current level to the level the player is currently on
        

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
            //initiating each level and load them to levelList
            Level startMenu = new StartMenu();
            levelList[0] = startMenu;

            InstructionScreen instructionScreen = new InstructionScreen();
            levelList[1] = instructionScreen; //sets the instruction screen as the 2nd level

            Level one = new Level();
            levelList[2] = one;            // sets the first actual playing level as the third level
            
            Level3 two = new Level3();
            levelList[3] = two;

            Level2 three = new Level2();
            levelList[4] = three;

            Level4 four = new Level4();
            levelList[5] = four;

            Level5 five = new Level5();
            levelList[6] = five;
            
            currentLevel = levelList[5];  // sets the current level so that the game advances
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

            //load content in each level
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
            
                //getting states
                KeyboardState state = Keyboard.GetState();
                MouseState mouse = Mouse.GetState();

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                if (GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                //if level has finished and flagged "Done"
                if (currentLevel.Done == true)
                {

                    //if this is last level, close the game
                    if (currentLevel == levelList[levelList.Length - 1])
                    {
                        Exit();
                    }
                    //otherwise, advance to next level
                    else
                    {
                        currentLevel = levelList[Array.IndexOf(levelList, currentLevel) + 1];
                    }
                }
                //needs to pass gameTime variable to level five because it needs a clock
                if (currentLevel is Level5)
                {
                    ((Level5)currentLevel).Update(state, mouse,gameTime); 
                }
                //update other levels as normal
                else
                {
                    currentLevel.Update(state, mouse);
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
