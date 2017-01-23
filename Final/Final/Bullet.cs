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
    //bullet object, by David A. and David D.
    class Bullet : GameObject

    {
        
        GameObject control;   //the object which fired the bullet. The balloon will not damage its creator for obvious reasons

        public bool NeedsRemove { get; set; }  // if a bullet collides with a plane or leaves the screen it will be removed

        public Bullet(Texture2D bulletTex, Vector2 position,GameObject control) : base(bulletTex, position)  // constructor
        {

            this.position = position;  // the position of the bullet
            
            velocity = new Vector2(10, 10); // thus is the speed that the bullet fires at
            NeedsRemove = false;  // if a bullet does not hit anything it can stay on the screen
            this.control = control;



        }
        public void CheckCollide(Plane plane) // checks for collissions between planes and bullets
        {
            if(CollisionRectangle.Intersects(plane.CollisionRectangle) && plane != control)
            {
                plane.damage(); // if the plane gets hit--remove health
                NeedsRemove = true;//and the bullet tells the controller to remove itself from the bulletList
            }
        }
        public void Update(List<GameObject> obstacleList)
        {
            position += velocity; // moves the planes coordinates causeing it to move
            foreach (GameObject obstacle in obstacleList)  // checks for collissions between bullets and other objects
            {
                if(obstacle is Plane) // if the obsticle is another plane, run the bullet/plane collission 
                {
                    CheckCollide((Plane)obstacle);
                }
            }
            
        }



    }
}