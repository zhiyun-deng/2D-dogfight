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
        private float acceleration;

        public Balloon(Texture2D texture, Vector2 pos, Vector2 velocity):base(texture, pos, velocity)
        {
            position = pos;
            this.texture = texture;
            this.velocity = velocity;
            acceleration = 0.2f;
        }
        
        public void Up()
        {
            velocity.Y -= acceleration;
        }
        public void Down()
        {
            velocity.Y += acceleration;
        }
        public void Left()
        {
            velocity.X -= acceleration;
        }
        public void Right()
        {
            velocity.X += acceleration;
        }
        public void MoveRandom()
        {
            Random rng = new Random();
            int verticalDirection = rng.Next(2);
            int horiDirection = rng.Next(2);
            if (verticalDirection == 0)
            {
                Up();
            }
            else
            {
                Down();
            }
            if (horiDirection == 0)
            {
                Left();
            }
            else
            {
                Right();
            }
            
        }
        

    }
}
