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
        GameObject blimp;
        public Level3()
        {
            
        }

        public override void Load(ContentManager Content)
        {
            base.Load(Content);
            Texture2D blimpImage = Content.Load<Texture2D>("Blimp");
            blimp = new GameObject(blimpImage, new Vector2(300, 300));
        }

        public override void Update(KeyboardState state, MouseState mouse)
        {
            // TODO: Add your update logic here
            base.Update(state, mouse);
            //blimp.Draw(spriteBatch);
        }
    }
}