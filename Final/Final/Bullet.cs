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
        Rectangle bulletRectangle;
        GameObject control;

        public bool NeedsRemove { get; set; }

        public Bullet(Texture2D bulletTex, Vector2 position,GameObject control) : base(bulletTex, position)
        {

            this.position = position;
            bulletRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            velocity = new Vector2(10, 10);
            NeedsRemove = false;
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