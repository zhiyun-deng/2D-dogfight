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

        
        
        public void Up()
        {
            velocity.Y = -2;
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

