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
        public Plane(Texture2D texture, Vector2 position, Vector2 velocity) : base(texture, position, velocity)
        {

        }

        public void Update(List<GameObject> wallList)
        {
            oldPosition = position;

            position.X += velocity.X;

            position.Y += velocity.Y;

            //for (int i = 0; i < wallList.Count; i++)
            //{
            //    if (IsCollide(wallList[i]))
            //    {
            //        CollideWallY(wallList[i]);
            //    }
            //}
        }

        //public void CollideWallY(GameObject wall)
        //{
        //    if (position.Y - oldPosition.Y > 0)
        //    {
        //        position.Y = wall.BoundingBox.Y - BoundingBox.Height;
        //    }
        //    else if (position.Y - oldPosition.Y < 0)
        //    {
        //        position.Y = wall.BoundingBox.Y + wall.BoundingBox.Height;
        //    }

        //    velocity.Y = 0;
        //}

        
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
        public void Left()
        {
            velocity.X = -2;
        }

        public void Right()
        {
            velocity.X = +2;
        }

        public void Down()
        {
            velocity.Y = 2;
        }

        public void Stop()
        {
            velocity.Y = 0;
            velocity.X = 0;
        }

    }


}

