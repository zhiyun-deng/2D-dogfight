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
        private float acceleration;
        protected List<Bullet> bulletList = new List<Bullet>();
        private float angle;
        Texture2D bulletTex;

        public Balloon(Texture2D texture, Vector2 pos, Vector2 velocity,Texture2D bulletTex):base(texture, pos, velocity)
        {
            position = pos;
            this.texture = texture;
            this.velocity = velocity;
            acceleration = 0.2f;
            this.bulletTex = bulletTex;
        }
        
        public void Up()
        {
            velocity.Y -= acceleration;
        }
        public void Down()
        {
            velocity.Y += acceleration;
        }
        public void Left()
        {
            velocity.X -= acceleration;
        }
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
            if (rng.Next(10) == 0)
            {
                Bullet bullet = new Bullet(bulletTex, position, this);
                float xChange = 0, yChange = 0;
                if (Math.Floor(angle/Math.PI*2) %2 == 0)
                { 
                    xChange = 1;
                    yChange = (float)Math.Tan(angle); 
                }
                else
                {
                    xChange = -1;
                    yChange = -(float)Math.Tan(angle);
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
