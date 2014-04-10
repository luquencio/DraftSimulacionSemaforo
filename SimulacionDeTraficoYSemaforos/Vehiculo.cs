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
        private Vector2 velocity;

        public Vehiculo(Texture2D textura, Vector2 posicion) :
            base(textura, posicion)
        {
            velocity = new Vector2(0, 0);
        }

        public override void Update(GameTime gametime)
        {
            velocity.Y = -0.5f;

            posicion += velocity;

            base.Update(gametime);
        }

        public void Update(GameTime gametime, float vel)
        {
            velocity.Y = vel;

            posicion += velocity;

            base.Update(gametime);
        }

        public void Crash()
        {
            throw new NotImplementedException();

        }
    }
}
