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
        //need to fix protection level of variables
        private Vector2 headPos;
        private Vector2 tailPos;
        public bool faceRight = true;
        public float angle = 0.0f;
        Rectangle sourceRectangle;

        Vector2 origin;
        private float angleSpeed = 0.015f;
        double speed = 2;
        Texture2D leftTexture;
        Texture2D rightTexture;
        int health;
        bool shield;


        
        public Plane(Texture2D leftTexture, Texture2D rightTexture, Vector2 position, bool right) : base(leftTexture, position)
        {
            this.leftTexture = leftTexture;
            this.rightTexture = rightTexture;
            this.position = position;

            //providing the plane is horizontal
            if (right)
            {
                faceRight = true;
                tailPos = position;
                headPos = new Vector2(position.X + texture.Width, position.Y);
                origin = new Vector2(0, 0);
                texture = rightTexture;

            }
            else
            {
                faceRight = false;
                headPos = position;
                tailPos = new Vector2(position.X + texture.Width, position.Y);
                origin = new Vector2(texture.Width, texture.Height);
                texture = leftTexture;
            }
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);

            velocity = new Vector2(0, 0);
        }
        //velocity might not be needed
        public Plane(Texture2D leftTexture, Texture2D rightTexture, Vector2 position, Vector2 velocity, bool right) : base(leftTexture, position, velocity)
        {
            this.leftTexture = leftTexture;
            this.rightTexture = rightTexture;
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
                texture = rightTexture;

            }
            else
            {
                faceRight = false;
                headPos = position;
                tailPos = new Vector2(position.X + texture.Width, position.Y);
                origin = new Vector2(texture.Width, texture.Height);
                texture = leftTexture;
            }
        }


        public void Update(Plane another)
        {
            position += velocity;
            if (another.BoundingBox.Intersects(this.BoundingBox))
            {
                position.X -= 300;
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

            if (angle > Math.PI / 2 || angle < -Math.PI / 2)
            {
                flip();
                if(angle> Math.PI / 2)
                {
                    angle = (float)Math.PI / 2;
                }
                else
                {
                    angle = -(float)Math.PI / 2;
                }
                
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
            if (angle > Math.PI / 2 || angle < -Math.PI / 2)
            {
                flip();
                if (angle > Math.PI / 2)
                {
                    angle = (float)Math.PI / 2;
                }
                else
                {
                    angle = -(float)Math.PI / 2;
                }

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
            angle = -angle;
            //still jumpy
            position.X = position.X - texture.Height;
        }
        public void right()
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
        public void left()
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
        public void accelerate(double speedAdded)
        {
            speed += speedAdded;
        }
        public void sheild()
        {

        }
        public void explode()
        {

        }

        


    }
}

