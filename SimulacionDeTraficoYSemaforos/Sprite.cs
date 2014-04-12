using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SimulacionDeTraficoYSemaforos
{
    public class Sprite
    {
        protected Texture2D textura;
        protected Vector2 posicion;

        public Sprite(Texture2D textura, Vector2 posicion)
        {
            this.textura = textura;
            this.posicion = posicion;

            // (Velocity*(float) gameTime.ElapsedGameTime.TotalSeconds);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, posicion, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        

        protected virtual Rectangle CreateBoundingBoxFromPosition(Vector2 posicion)
        {
            return new Rectangle((int)posicion.X, (int)posicion.Y, (int)Width, (int)Height);
        }


        protected Vector2 Posicion { get { return posicion; } }
        protected float Width { get { return textura.Width; } }
        protected float Height { get { return textura.Height; } }        

        public Rectangle BoundingBox
        {
            get { return CreateBoundingBoxFromPosition(posicion); }
        }

        //protected Texture2D Textura { set { textura = value; } }
    }
}
