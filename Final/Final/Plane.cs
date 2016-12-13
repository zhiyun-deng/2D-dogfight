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
    class Plane:GameObject
    {
        private Vector2 headPos;
        private Vector2 tailPos;
        private bool faceRight = true;
        private float angle = 0f;
        
        
        public Plane(Texture2D texture, Vector2 position):base(texture, position)
        {
            this.texture = texture;
            this.position = position;
            tailPos = position;
            headPos = new Vector2(position.X+texture.Width, )

            velocity = new Vector2(0, 0);
        }
        public Plane(Texture2D texture, Vector2 position, Vector2 velocity) : base(texture, position, velocity)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
        }



    }
}
