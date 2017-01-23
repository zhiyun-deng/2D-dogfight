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
        private float acceleration; //acceleration of balloon: rate at which balloon changes its course
        protected List<Bullet> bulletList = new List<Bullet>();//stores list of bullets that balloon shoots out
        private float angle;//the angle at which balloon shoots bullets
        Texture2D bulletTex;//texture of bullets

        //constructor
        public Balloon(Texture2D texture, Vector2 pos, Vector2 velocity,Texture2D bulletTex):base(texture, pos, velocity)
        {
            position = pos;
            this.texture = texture;
            this.velocity = velocity;
            acceleration = 0.2f;
            this.bulletTex = bulletTex;
        }
        

        //change the course (velocity) of the balloon so that it goes more upward
        public void Up()
        {
            velocity.Y -= acceleration;
        }
        //change the course (velocity) of the balloon so that it goes more downward
        public void Down()
        {
            velocity.Y += acceleration;
        }
        //change the course (velocity) of the balloon so that it goes more to the left
        public void Left()
        {
            velocity.X -= acceleration;
        }
        //change the course (velocity) of the balloon so that it goes more to the right
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
        public void Shoot()
        {
            Random rng = new Random();
            if (rng.Next(20) == 0)
            {
                Bullet bullet = new Bullet(bulletTex, position, this);
                float xChange = 0, yChange = 0;
                if (Math.Floor(angle/Math.PI*2) %4 == 0)
                { 
                    xChange = 1;
                    yChange = (float)Math.Tan(angle); 
                }
                else if (Math.Floor(angle / Math.PI * 2) % 4 == 1)
                {
                    xChange = -1;
                    yChange = -(float)Math.Tan(angle);
                }
                else if (Math.Floor(angle / Math.PI * 2) % 4 == 2)
                {
                    xChange = -1;
                    yChange = -(float)Math.Tan(angle);
                }
                else if (Math.Floor(angle / Math.PI * 2) % 4 == 3)
                {
                    xChange = 1;
                    yChange = (float)Math.Tan(angle);
                }
                bullet.MoveTo(new Vector2(position.X + xChange, position.Y + yChange));

                bulletList.Add(bullet);
            }
        }
        public void Update(List<GameObject> obstacleList)
        {
            base.Update();
            foreach (Bullet bullet in bulletList.Reverse<Bullet>())
            {
                bullet.Update(obstacleList);
                if (bullet.NeedsRemove == true)
                {
                    bulletList.Remove(bullet);

                }
            }
            angle += 0.01f;
            //if(angle > Math.PI)
            //{
                
            //}
            
            Shoot();

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (Bullet bullet in bulletList.Reverse<Bullet>())
            {
                bullet.Draw(spriteBatch);
            }
        }
        


    }
}
