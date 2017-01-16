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
    class StartMenu : Level
    {

        Texture2D startMenu;

        public StartMenu()
        {
            
        }
        public override void Load(ContentManager Content)
        {
            base.Load(Content);
            startMenu = Content.Load<Texture2D>("MainMenu");
           


        }
        public override void Update(KeyboardState state, MouseState mouse)
        {


            base.Update(state, mouse);
            gettingResponse = true;










        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(startMenu, new Vector2(0, 0), Color.White);

        }
    }
}
