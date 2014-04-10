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

        }

        public override void Update()
        {
            base.Update();
        }

        public void Crash()
        {
            throw new NotImplementedException();

        }
    }
}
