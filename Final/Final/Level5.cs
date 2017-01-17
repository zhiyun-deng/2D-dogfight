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
    class Level5: Level
    {
        
        TimeSpan timeSpan = TimeSpan.FromMilliseconds(0000);

        public Level5()
        {
            
        }
        public override void Load(ContentManager Content)
        {
            base.Load(Content);
            



        }
        public void Update(KeyboardState state, MouseState mouse,GameTime gameTime)
        {


            base.Update(state, mouse);
            timeSpan += gameTime.ElapsedGameTime;











        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
            spriteBatch.DrawString(font, timeSpan.TotalSeconds.ToString(), new Vector2(100, 100), Color.Black);

        }
    }
}
