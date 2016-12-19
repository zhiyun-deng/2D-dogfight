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
        private float angleSpeed = 0.02f;
        double speed = 1.5;
        Texture2D rightTexture;
        Texture2D leftTexture;
        bool flip = false;


        //texture faces left, right texture faces right
        public Plane(Texture2D LeftTexture,Texture2D  rightTexture, Vector2 position, bool right) : base(LeftTexture, position)
        {
            
            this.position = position;
            this.rightTexture = rightTexture;
            this.leftTexture = LeftTexture;

            //providing the plane is horizontal
            if (right)
            {
                texture = rightTexture;
                faceRight = true;
                tailPos = position;
                headPos = new Vector2(position.X + texture.Width, position.Y);
                origin = new Vector2(0, 0);

            }
            else
            {
                texture = leftTexture;
                faceRight = false;
                headPos = position;
                tailPos = new Vector2(position.X + texture.Width, position.Y);
                origin = new Vector2(texture.Width, texture.Height);
            }
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);

            velocity = new Vector2(0, 0);
        }
        //velocity might not be needed
        public Plane(Texture2D leftTexture, Texture2D rightTexture, Vector2 position, Vector2 velocity, bool right) : base(leftTexture, position, velocity)
        {
            
            this.position = position;
            this.velocity = velocity;
            this.rightTexture = rightTexture;
            this.leftTexture = leftTexture;
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);

            //providing the plane is horizontal
            if (right)
            {
                this.texture = rightTexture;
                faceRight = true;
                tailPos = position;
                headPos = new Vector2(position.X + texture.Width, position.Y);
                origin = new Vector2(0, 0);

            }
            else
            {
                this.texture = leftTexture;
                faceRight = false;
                headPos = position;
                tailPos = new Vector2(position.X + texture.Width, position.Y);
                origin = new Vector2(texture.Width, texture.Height);
            }
        }


        public override void Update()
        {
            position += velocity;
            if(angle < Math.PI/2 && angle > -Math.PI / 2)
            {
                flip();
            }

        }
        public override void Draw(SpriteBatch sprite)
        {

            sprite.Draw(texture, position, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);


        }


        //if faceright and up, angle -
        //right and down +
        //left and down -
        //left and up +



        public void Up() //not for ffaceright
        {
            if (!faceRight) { angle += angleSpeed; }
            else
            {
                angle -= angleSpeed;
            }
            double ratio = texture.Width / (speed*speed);

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
            if (!faceRight) { angle -= angleSpeed; }
            else
            {
                angle += angleSpeed;
            }


            double ratio = texture.Width / (speed*speed);

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
                if (faceRight)
                {
                    Up(); 
                }
                else
                {
                    Down();
                }
            }
            else if (angle < 0)
            {
                if (faceRight)
                {
                    Down(); 
                }
                else
                {
                    Up();
                }
            }
        }
        public void flip()
        {
            faceRight = !faceRight;
            if (faceRight)
            {
                texture = rightTexture;

            }
            else
            {
                texture = leftTexture;
            }
        }



    }
}

