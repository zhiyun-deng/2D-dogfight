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
        private float angle = 1.0f;
        Rectangle sourceRectangle;

        Vector2 origin = new Vector2(0, 0);
        private float angleSpeed = 0.0f;
        


        public Plane(Texture2D texture, Vector2 position):base(texture, position)
        {
            this.texture = texture;
            this.position = position;
            //tailPos = position;
            //headPos = new Vector2(position.X+texture.Width, )
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);

            velocity = new Vector2(0, 0);
        }
        public Plane(Texture2D texture, Vector2 position, Vector2 velocity) : base(texture, position, velocity)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
        }


        public override void Update()
        {
            position += velocity;
            
        }
        public override void Draw(SpriteBatch sprite)
        {
            
            sprite.Draw(texture, position, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);

        }
        



    }
}
