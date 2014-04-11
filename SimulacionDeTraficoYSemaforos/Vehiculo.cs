using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SimulacionDeTraficoYSemaforos
{
    public class Vehiculo : Sprite
    {
        private Vector2 velocidad;
        private Vector2 centroDeRotacion;
        private Rectangle dimensiones;
        private float anguloDeRotacion;

        public Vehiculo(Texture2D textura, Vector2 posicion, Vector2 centroDeRotacion, float anguloDeRotacion, Vector2 velocidad, Rectangle dimensiones) :
            base(textura, posicion)
        {
            this.velocidad = velocidad;
            this.centroDeRotacion = centroDeRotacion;
            this.anguloDeRotacion = anguloDeRotacion;
            this.dimensiones = dimensiones;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, posicion, null, Color.White, anguloDeRotacion,
            centroDeRotacion, 1.0f, SpriteEffects.None, 0f);
        }

        public override void Update(GameTime gametime)
        {
            posicion += velocidad;

            base.Update(gametime);
        }

        public void Crash()
        {
            velocidad.X = 0f;
            velocidad.Y = 0f;
        }

        protected override Rectangle CreateBoundingBoxFromPosition(Vector2 posicion)
        {
            return new Rectangle((int)posicion.X, (int)posicion.Y, dimensiones.Width, dimensiones.Height);
        }
        
    }
}
