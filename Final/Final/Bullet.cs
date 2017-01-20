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
    class Bullet : GameObject

    {
        Rectangle bulletRectangle;  // create a rectangle around the bullet for colissions 
        GameObject control;   //  

        public bool NeedsRemove { get; set; }  // if a bullet collides with a plane or leaves the screen it will be removed

        public Bullet(Texture2D bulletTex, Vector2 position,GameObject control) : base(bulletTex, position)  // create a bullet object
        {

            this.position = position;  // the position of the bullet
            bulletRectangle = new Rectangle(0, 0, texture.Width, texture.Height);  // sets the width and height of the bullet texture
            velocity = new Vector2(5, 5); // thus is the speed that the bullet fires at
            NeedsRemove = false;  // if a bullet does not hit anything it can stay on the screen
            this.control = control;



        }
        public void CheckCollide(Plane plane)
        {
            if(CollisionRectangle.Intersects(plane.CollisionRectangle) && plane != control)
            {
                plane.damage();
                NeedsRemove = true;
            }
        }
        public void Update(List<GameObject> obstacleList)
        {
            position += velocity;
            foreach (GameObject obstacle in obstacleList)
            {
                if(obstacle is Plane)
                {
                    CheckCollide((Plane)obstacle);
                }
            }
            
        }



    }
}