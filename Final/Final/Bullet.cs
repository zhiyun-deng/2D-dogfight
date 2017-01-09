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
    class Bullet : GameObject

    {
        Rectangle bulletRectangle;
        


        public Bullet(Texture2D bulletTexture, Vector2 position):base(bulletTexture,position)
        {
            
            this.position = position;
            bulletRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            

        }


        }
}