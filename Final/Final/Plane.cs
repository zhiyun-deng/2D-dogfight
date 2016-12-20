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
        private float angleSpeed = 0.03f;
        double speed = 1;


        
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
        //velocity might not be needed
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

        }

        public override void Draw(SpriteBatch sprite)
        {

            sprite.Draw(texture, position, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);


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


        // Check for x wall collision

        for (int i = 0; i < wallList.Count; i++)
        {
            if (IsCollide(wallList[i]))
            {
                CollideWallX(wallList[i]);
            }
        }
    }

        //if faceright and up, angle -
        //right and down +
        //left and down -
        //left and up +


            // Check for Y wall collision

        public void Up() //not for ffaceright
        {
            if (!faceRight) { angle += angleSpeed; }
            else
            {
                angle -= angleSpeed;
            }
            double ratio = texture.Width / (speed*speed);
            for (int i = 0; i < wallList.Count; i++)
            {
                if (IsCollide(wallList[i]))
                {
                    CollideWallY(wallList[i]);
                }
            }

            
          }

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
        
        
        public void Up()
        {
            velocity.Y = -2;
        }
        public void Left()
        {
            velocity.X = -2;
        }

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



    }
}


