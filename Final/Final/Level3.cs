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
    class Level3 : Level
    {
        public Level3()
        {

        }
            public override void Load(ContentManager Content)
        {
            base.Load(Content);
            Texture2D wallImage = Content.Load<Texture2D>("Border1280");
            GameObject wall = new GameObject(wallImage, new Vector2(300,300));
            wall.SetSize(250, 80);
            wallList.Add(wall);

        }
        public override void Update(KeyboardState state, MouseState mouse)
        {


            // TODO: Add your update logic here

            base.Update(state, mouse);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

        }
    }
    
}
