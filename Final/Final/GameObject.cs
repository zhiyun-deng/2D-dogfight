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
        protected int width = 0;
        protected int height = 0;
        

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

        public virtual Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    width,
                    height);
            }
        }
        

        // Constructors

        public GameObject(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            width = texture.Width;
            height = texture.Height;
        }

        public GameObject(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            velocity = new Vector2(0, 0);
            width = texture.Width;
            height = texture.Height;

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, width, height), Color.White);
        }
        

        public bool IsCollide(GameObject target)
        {
            if (CollisionRectangle.Intersects(target.CollisionRectangle))
            {
                return true;
            }

            return false;
        }
        public void MoveTo(Vector2 target)
        {

            if (position.Equals(target))
            {
                velocity = Vector2.Zero;
                return;
            }

            //speed of gameObject calculated from the two vectors
            double speed = Math.Sqrt((double)velocity.X * velocity.X + (double)velocity.Y * velocity.Y);
            //ratio of the velocity.X and Y needed for object to move toward target
            double ratio = (target.X - position.X) / (target.Y - position.Y);
            velocity.Y = (float)Math.Sqrt(speed * speed / (ratio * ratio + 1));
            velocity.X = (float)Math.Sqrt(speed * speed - velocity.Y * velocity.Y);

            if (target.X < position.X)
            {
                velocity.X *= -1;
            }
            if (target.Y < position.Y)
            {
                velocity.Y *= -1;
            }
        }

        public virtual void Update()
        {
            position += velocity;
        }
        public void SetSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }


       
    }
}
