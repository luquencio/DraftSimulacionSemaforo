using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SimulacionDeTraficoYSemaforos
{
    public class Controlador
    {
        private Texture2D[] textura;

        public Controlador(Texture2D[] textura)
        {
            this.textura = textura;
        }

        public virtual void Update(GameTime gametime)
        {

        }
    }
}
