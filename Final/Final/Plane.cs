﻿using System;
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
        double speed = 3;
        Texture2D leftTexture;
        Texture2D rightTexture;
        int health = 10;
        bool shield;
        AnimatedClass explosion;
        bool dead = false;
        bool exploding = false;



        
        public Plane(Texture2D leftTexture, Texture2D rightTexture, Vector2 position, bool right, AnimatedClass explosion, Texture2D bulletTex) : base(leftTexture, position)
        {
            this.leftTexture = leftTexture;
            this.rightTexture = rightTexture;
            this.position = position;
            this.explosion = explosion;

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

            this.bulletTex = bulletTex;
        }
        //velocity might not be needed
        public Plane(Texture2D leftTexture, Texture2D rightTexture, Vector2 position, Vector2 velocity, bool right, AnimatedClass explosion) : base(leftTexture, position, velocity)
        {
            this.leftTexture = leftTexture;
            this.rightTexture = rightTexture;
            this.position = position;
            this.velocity = velocity;
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            this.explosion = explosion;

            //providing the plane is horizontal
            if (right)
            {
                faceRight = true;
                tailPos = position;
                headPos = new Vector2(position.X + texture.Width, position.Y);
                origin = new Vector2(0, 0);
                texture = rightTexture;

            // Check for x wall collision

            

            
            position.Y += velocity.Y;
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

            this.bulletTex = bulletTex;

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

        public void Update(List<GameObject> wallList, List<GameObject> obstacleList)
        {

            foreach  (GameObject obstacle in obstacleList)
            {
                if (obstacle.CollisionRectangle.Intersects(CollisionRectangle) && obstacle != this)
                {
                    explode();
                    
                } 
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
            if (exploding)
            {
                explosion.Draw(sprite, new Vector2(position.X-50, position.Y-50));

            }



        }


        //if faceright and up, angle -
        //right and down +
        //left and down -
        //left and up +
        public void Shoot()
        {
            Bullet bullet = new Final.Bullet(bulletTex, position);
            bullet.MoveTo(Vector2.Zero);
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
            health = 0;
            dead = true;
            exploding = true;
        }
        

        


    }
}



