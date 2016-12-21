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
    class Balloon : GameObject

    {

        public Balloon(Texture2D texture, Vector2 pos, Vector2 velocity):base(texture, pos, velocity)
        {
            position = pos;
            this.texture = texture;
            this.velocity = velocity;
        }
        public void draw(SpriteBatch sprite)
        {
            sprite.Draw(texture, new Rectangle(texture, );
        }
        
    }
}
