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
        public Vector2 headPos;
        public Vector2 tailPos;
        public bool faceRight = true;
        public float angle = 0.0f;
        Rectangle sourceRectangle;
       

        Vector2 origin;
        private float angleSpeed = 0.04f;
        double speed = 1.5;
        Texture2D leftTexture;
        Texture2D rightTexture;
        




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


   





        //public override void Update()
        //{
        //    position += velocity;
        //}

        public override void Draw(SpriteBatch sprite)
        {
            sprite.Draw(texture, position, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
        }

        public void CollideWallY(GameObject wall)
        {
            if (position.Y - oldPosition.Y > 0)
            {
                position.Y = wall.CollisionRectangle.Y - CollisionRectangle.Height;
            }
            else if (position.Y - oldPosition.Y < 0)
            {
                position.Y = wall.CollisionRectangle.Y + wall.CollisionRectangle.Height;
            }
        }

        public void CollideWallX(GameObject wall)
        {
            if (position.X - oldPosition.X > 0)
            {
                position.X = wall.CollisionRectangle.X - CollisionRectangle.Width;
            }
            else if (position.X - oldPosition.X < 0)
            {
                position.X = wall.CollisionRectangle.X + wall.CollisionRectangle.Width;
            }
        }

        public void Update(List<GameObject> wallList, List<Plane> planeList)
        {
            oldPosition = position;

            position.X += velocity.X;

            // Check for x wall collision

            for (int i = 0; i < wallList.Count; i++)
            {
                if (IsCollide(wallList[i]))
                {
                    CollideWallX(wallList[i]);
                }
            }


            position.Y += velocity.Y;

            // Check for Y wall collision

            for (int i = 0; i < wallList.Count; i++)
            {
                if (IsCollide(wallList[i]))
                {
                    CollideWallY(wallList[i]);
                }
            }

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
                if (angle > Math.PI / 2)
                {
                    angle = (float)Math.PI / 2;
                }
                else
                {
                    angle = -(float)Math.PI / 2;
                }

            }

            double ratio = texture.Width / (speed * speed);

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

            double ratio = texture.Width / (speed * speed);

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



    }
}



