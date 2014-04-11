using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SimulacionDeTraficoYSemaforos
{
    class ControladorDeSemaforos : Controlador
    {
        private Vector2[] vectoresBarrera = { new Vector2(118, 193),
                                              new Vector2(358,193),
                                              new Vector2(687,196),
                                              new Vector2(651,276),
                                              new Vector2(401,276),
                                              new Vector2(109,240),
                                              new Vector2(376,240),
                                              new Vector2(645,239),
                                              new Vector2(419,195),
                                              new Vector2(152,195),
                                            };


        public ControladorDeSemaforos(Texture2D[] textura) :
            base(textura)
        {
        }

        private void CrearSemaforos()
        {
        }

        public override void Update(GameTime gametime)
        {

            base.Update(gametime);
        }
    }
}
