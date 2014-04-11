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
        private float anguloDeRotacion;

        public Vehiculo(Texture2D textura, Vector2 posicion, Vector2 centroDeRotacion, float anguloDeRotacion, Vector2 velocidad) :
            base(textura, posicion)
        {
            this.velocidad = velocidad;
            this.centroDeRotacion = centroDeRotacion;
            this.anguloDeRotacion = anguloDeRotacion;
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
            throw new NotImplementedException();

        }
        
    }
}
