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
    class GameObject
    {
        protected Texture2D texture;  //picture representation
        protected Vector2 position;   // where it is
        protected Vector2 velocity;   // how fast it is moving
        protected Vector2 oldPosition; // where it was last frame

        // Properties

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }

        // This is used to calculate the recatangle of the texture for collision

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    texture.Width,
                    texture.Height);
            }
        }

        // Constructors

        public GameObject(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
        }

        public GameObject(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            velocity = new Vector2(0, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position);
        }

        public bool IsCollide(GameObject target)
        {
            if (BoundingBox.Intersects(target.BoundingBox))
            {
                return true;
            }

            return false;
        }

        public virtual void Update()
        {
            position += velocity;
        }
    }
}
