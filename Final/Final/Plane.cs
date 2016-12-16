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
    class Plane : GameObject
    {
        private Vector2 headPos;
        private Vector2 tailPos;
        private bool faceRight = true;
        private float angle = 0.0f;
        Rectangle sourceRectangle;

        Vector2 origin;
        private float angleSpeed = 0.0f;



        public Plane(Texture2D texture, Vector2 position, bool right) : base(texture, position)
        {
            this.texture = texture;
            this.position = position;

            //providing the plane is horizontal
            if (right)
            {
                faceRight = true;
                tailPos = position;
                headPos = new Vector2(position.X + texture.Width, position.Y);
                origin = new Vector2(0, 0);

            }
            else
            {
                faceRight = false;
                headPos = position;
                tailPos = new Vector2(position.X + texture.Width, position.Y);
                origin = new Vector2(texture.Width, texture.Height);
            }
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);

            velocity = new Vector2(0, 0);
        }
        public Plane(Texture2D texture, Vector2 position, Vector2 velocity, bool right) : base(texture, position, velocity)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);

            //providing the plane is horizontal
            if (right)
            {
                faceRight = true;
                tailPos = position;
                headPos = new Vector2(position.X + texture.Width, position.Y);
                origin = new Vector2(0, 0);

            }
            else
            {
                faceRight = false;
                headPos = position;
                tailPos = new Vector2(position.X + texture.Width, position.Y);
                origin = new Vector2(texture.Width, texture.Height);
            }
        }


        public override void Update()
        {
            position += velocity;

        }
        public override void Draw(SpriteBatch sprite)
        {

        public void CollideWall(GameObject wall)
        {
            
        }
            sprite.Draw(texture, position, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);


        }
        public void Up()
        {
            angle += 0.01f;
            double ratio = texture.Width / 4;

            //velocity.Y = (float)Math.Tan(angle) * velocity.X;
            float upChange = (float)Math.Sin(angle) * texture.Width;//
            float horiChange = (float)Math.Sqrt(texture.Width * texture.Width - upChange * upChange);

            if (!faceRight)
            {
                upChange *= -1;
                horiChange *= -1;
            }
            tailPos = position;
            if (faceRight)
            {
                headPos = new Vector2(tailPos.X + horiChange, tailPos.Y + upChange);
            }
            else
            {
                headPos = new Vector2(tailPos.X - horiChange, tailPos.Y - upChange);
            }
            velocity.X = (float)(horiChange / ratio);
            velocity.Y = upChange / (float)ratio;

        }

        public void Down()
        {
            angle -= 0.01f;


            double ratio = texture.Width / 4;

            //velocity.Y = (float)Math.Tan(angle) * velocity.X;
            float upChange = (float)Math.Sin(angle) * texture.Width;//
            float horiChange = (float)Math.Sqrt(texture.Width * texture.Width - upChange * upChange);

            if (!faceRight)
            {
                upChange *= -1;
                horiChange *= -1;
            }
            tailPos = position;
            if (faceRight)
            {
                headPos = new Vector2(tailPos.X + horiChange, tailPos.Y + upChange);
            }
            else
            {
                headPos = new Vector2(tailPos.X - horiChange, tailPos.Y - upChange);
            }
            velocity.X = (float)(horiChange / ratio);
            velocity.Y = upChange / (float)ratio;
        }

        public void Stop()
        {
            if (angle > 0)
            {
                Down();
            }
            if (angle < 0)
            {
                Up();
            }
            double ratio = texture.Width / 4;

            //velocity.Y = (float)Math.Tan(angle) * velocity.X;
            float upChange = (float)Math.Sin(angle) * texture.Width;//
            float horiChange = (float)Math.Sqrt(texture.Width * texture.Width - upChange * upChange);

            if (!faceRight)
            {
                upChange *= -1;
                horiChange *= -1;
            }
            tailPos = position;
            if (faceRight)
            {
                headPos = new Vector2(tailPos.X + horiChange, tailPos.Y + upChange);
            }
            else
            {
                headPos = new Vector2(tailPos.X - horiChange, tailPos.Y - upChange);
            }
            velocity.X = (float)(horiChange / ratio);
            velocity.Y = upChange / (float)ratio;

        }



    }
}

