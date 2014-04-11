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
        private Texture2D[] texturas;

        public Controlador(Texture2D[] texturas)
        {
            this.texturas = texturas;
        }

        public virtual void Update(GameTime gametime)
        {

        }

        public Texture2D[] Texturas { get { return this.texturas; } }
        public int TexturasDisponibles { get { return this.texturas.Count(); } }
    }
}
