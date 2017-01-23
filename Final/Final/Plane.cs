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
    //plane object that flies and shoots
    class Plane : GameObject
    {
        
        public Vector2 headPos;//where the head of the plane is

        private Vector2 tailPos;//where the tail of the plane is
        private bool faceRight = true;//if the plane faces right, faceright = true
        private float angle = 0.0f;//the angle at which plane goes
        Rectangle sourceRectangle;//the rectangle selected from the plane picture to draw
       

        Vector2 origin;//the point around which plane rotates
        private float angleSpeed = 0.035f;//speed at which plane rotates
        double speed = 2.5;//speed at which plane moves

        //two pictures of the plane are needed for plane to flip
        Texture2D leftTexture;//picture of the plane facing left
        Texture2D rightTexture;//picture of plane facing right

        Texture2D bulletTex;//bullet image
        private int health = 5;
        bool shield;
        Texture2D heartTex;
        AnimatedClass explosion;
        bool dead = false;
        bool exploding = false;
        protected List<Bullet> bulletList;
        protected List<GameObject> heartList;
        protected int lastHealth;
        

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }



        //constructor
        public Plane(Texture2D leftTexture, Texture2D rightTexture, Vector2 position, bool right, AnimatedClass explosion, Texture2D bulletTex,Texture2D heartTex) : base(leftTexture, position)
        {
            this.leftTexture = leftTexture;
            this.rightTexture = rightTexture;
            this.position = position;
            this.explosion = explosion;
            this.bulletTex = bulletTex;
            bulletList = new List<Bullet>();
            heartList = new List<GameObject>();
            lastHealth = health;

            //creating health index(hearts)
            //if the plane starts by facing left, health index is on the right part of screen
            if (!right)
            {

                for (int i = 0; i < 5; i++)
                {
                    GameObject heart = new GameObject(heartTex, new Vector2(1230 - 20 * i, 680));
                    heart.SetSize(20, 20);
                    heartList.Add(heart);
                }
            }
            //otherwise, put it on left part
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    GameObject heart = new GameObject(heartTex, new Vector2(20 + 20 * i, 680));
                    heart.SetSize(20, 20);
                    heartList.Add(heart);
                }
            }

            //these variables' initialvalue depend in orientation of plane
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
        
      //because position is always at the tail of the plane, collision rectangle has to be modified to
      //take into account plane's orietation
        public override Rectangle CollisionRectangle
        {
            get
            {

                if (faceRight)
                {
                    return new Rectangle(
                                            (int)position.X,
                                            (int)position.Y,
                                            texture.Width,
                                            texture.Height); 
                }
                else
                {
                    return new Rectangle(
                                            (int)position.X-texture.Width,
                                            (int)position.Y-texture.Height,
                                            texture.Width,
                                            texture.Height);
                }
            }

            

        }


   





      
        

        public void CollideWallY(GameObject wall)
        {
            //bottom wall
            if (position.Y - oldPosition.Y > 0)
            {
                position.Y = wall.CollisionRectangle.Y - CollisionRectangle.Height;
            }
            else if (position.Y - oldPosition.Y < 0)
            {
                position.Y = wall.CollisionRectangle.Y + wall.CollisionRectangle.Height+texture.Height;
            }
        }

        public void CollideWallX(GameObject wall)
        {
            if (position.X - oldPosition.X > 0&&faceRight)
            {
                position.X = wall.CollisionRectangle.X - CollisionRectangle.Width;
            }
            else if (position.X - oldPosition.X < 0&&(!faceRight))
            {
                position.X = wall.CollisionRectangle.X + wall.CollisionRectangle.Width+texture.Width;
            }
            //if (position.X - oldPosition.X > 0 && (!faceRight))
            //{
            //    position.X = wall.CollisionRectangle.X - CollisionRectangle.Width;
            //}

        }
        

        public void Update(List<GameObject> wallList, List<GameObject> obstacleList)//moves plane, make it explode if necessary
        {
            //for every health point deducted, remove a heart
            if((lastHealth - health >= 1) && heartList.Count != 0 )
            {
                heartList.RemoveAt(heartList.Count - 1);
                lastHealth = health;
                if(health == 0)
                {
                    heartList.Clear();
                }
            }

            //update bullet, removing bullet from list if needed
            foreach (Bullet bullet in bulletList.Reverse<Bullet>())
            {
                bullet.Update(obstacleList);
                if (bullet.NeedsRemove == true)
                {
                    bulletList.Remove(bullet);

                }
            }

            foreach (GameObject obstacle in obstacleList)
            {
                if (obstacle.CollisionRectangle.Intersects(CollisionRectangle) && obstacle != this)
                {
                    explode();

                }
            }

            if (health == 0)
            {
                explode();
            }
            explosion.Update();
            if (dead)
            {
                velocity = new Vector2(0, 7f);
            }



            if (!dead)
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
            else
            {
                position += velocity; // velocity 
            }

            

        }
        public override void Draw(SpriteBatch sprite)
        {

            sprite.Draw(texture, position, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
            
            if (exploding&&faceRight)
            {
                explosion.Draw(sprite, new Vector2(position.X-50, position.Y-50));

            }
            else if (exploding)
            {
                explosion.Draw(sprite, new Vector2(position.X - 100, position.Y - 100));
            }
            foreach (Bullet bullet in bulletList)
            {
                bullet.Draw(sprite);
            }

            foreach (GameObject heart in heartList)
            {
                heart.Draw(sprite);
            }



        }


        //if faceright and up, angle -
        //right and down +
        //left and down -
        //left and up +
        public void Shoot()
        {
            Random rng = new Random();
            if (rng.Next(10) == 0)
            {
                Bullet bullet = new Bullet(bulletTex, position, this);
                float xChange = 0, yChange = 0;
                if (faceRight)
                {
                    xChange = 1;
                    yChange = (float)Math.Tan(angle);
                }
                else
                {
                    xChange = -1;
                    yChange = -(float)Math.Tan(angle);
                }
                bullet.MoveTo(new Vector2(position.X+ xChange,position.Y+yChange));

                bulletList.Add(bullet); 
            }
        }


        public void Up() //not for ffaceright
        {
            if (dead) return;
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
            if (dead) return;
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
            if (dead) return;
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
                
                tailPos = position;
                headPos = new Vector2(position.X + texture.Width, position.Y);
                origin = new Vector2(0, 0);
                texture = rightTexture;


            }
            else
            {
               
                headPos = position;
                tailPos = new Vector2(position.X + texture.Width, position.Y);
                origin = new Vector2(texture.Width, texture.Height);
                texture = leftTexture;

            }
            angle = -angle;
            

            
        }
        public void left()
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
        public void right()
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
            health = 0;
            dead = true;
            exploding = true;
        }
        public void damage()
        {
            health--;
        }





    }
}



