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

        public void CollideWall(GameObject wall)
        {
            
        }

        public void Up()
        {
            velocity.Y = -5;
        }
        public void Left()
        {
            velocity.X = -5;
        }

        public void Right()
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
            velocity.X = 0;
        }

    }


}

