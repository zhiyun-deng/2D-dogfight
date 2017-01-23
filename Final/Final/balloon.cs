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
    //the balloon object, special abilities, by David D.
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

        //adjust the course of the balloon randomly. The method is not used in the game itself.
        public void MoveRandom()
        {
            Random rng = new Random();

            //randomly selects a horizintal direction move(0 = left,1 =right)
            //do the same for vertical direction(0 = up, 1 = right)
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

        //shoots at the direction indicated by the angle
        public void Shoot()
        {
            Random rng = new Random();

            //random function makes sure that balloon does not shoot with every update call
            //instead, it has a i in 15 chance of firing
            //this reduces predictability and reduce the frequency of shooting to once every 15 frames on average
            if (rng.Next(15) == 0)
            {
                //create bullet
                Bullet bullet = new Bullet(bulletTex, position, this);

                //calculate target position based on angle
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

                //commands bullet to move to target
                bullet.MoveTo(new Vector2(position.X + xChange, position.Y + yChange));

                
                bulletList.Add(bullet);
            }
        }

        //move balloon if needed, fire shots, remove bullets as needed, 
        public void Update(List<GameObject> obstacleList)
        {
            //move balloon
            base.Update();

            //update each bullet(moving it), remove some bullets if necessary
            foreach (Bullet bullet in bulletList.Reverse<Bullet>())
            {
                bullet.Update(obstacleList);
                if (bullet.NeedsRemove == true)
                {
                    bulletList.Remove(bullet);

                }
            }

            //change the direction of shooting gradually
            angle += 0.01f;
            
            
            Shoot();

        }
        //draw balloon and bullets
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
