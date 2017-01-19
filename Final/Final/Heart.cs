using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace Final
{
    class Heart : GameObject
    {
       
        

        public bool NeedsRemove { get; set; }

        public Heart(Texture2D heartTex, Vector2 position) : base(heartTex, position)
        {

            this.position = position;
            velocity = new Vector2(0, 0);
            NeedsRemove = false;
            



        }
         
        
        //public void Update(List<GameObject> heartList)
        //{
        //    position += velocity;
        //    foreach (GameObject obstacle in heartList)
        //    {
                
        //    }

        //}



    }
}