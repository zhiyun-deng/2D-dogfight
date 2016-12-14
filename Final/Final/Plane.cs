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

            // position.X += velocity.X;

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
            velocity.Y = -5;
        }
        public void Foreward()
        {
            velocity.X = -5;
        }

        public void Back()
        {
            velocity.X = +5;
        }

        public void Down()
        {
            velocity.Y = 5;
        }

        public void Stop()
        {
            velocity.Y = 0;
        }

    }


}

