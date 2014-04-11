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
        List<Semaforo> SemaforosNorteSur = new List<Semaforo>();
        List<Semaforo> SemaforosEsteOeste = new List<Semaforo>();
        private float tiempoDeGeneracionRandom = 0f;

        private Vector2[] vectoresBarreraNorteSur = { new Vector2(115, 193),
                                                      new Vector2(378,193),
                                                     
                                                      new Vector2(651,276),
                                                      new Vector2(401,276),
                                                       
                                                    };



        private Vector2[] vectoresBarreraEsteOeste = { new Vector2(376,240),
                                                       new Vector2(645,239),
                                                       new Vector2(419,195),
                                                       new Vector2(152,195),
                                                       new Vector2(687,196),
                                                       new Vector2(109,240)
                                                    };


        public ControladorDeSemaforos(Texture2D[] texturas) :
            base(texturas)
        {
            CrearSemaforos();
        }

        private void CrearSemaforos()
        {
            foreach (var semaforo in vectoresBarreraNorteSur)
            {
                SemaforosNorteSur.Add(new Semaforo(Texturas[0], semaforo, Estado.Rojo , Texturas));
            }

            foreach (var semaforo in vectoresBarreraEsteOeste)
	        {
                SemaforosEsteOeste.Add(new Semaforo(Texturas[2], semaforo,Estado.Verde , Texturas));
	        }
        }

        public override void Update(GameTime gametime)
        {
            tiempoDeGeneracionRandom += (float)gametime.ElapsedGameTime.TotalSeconds;

            if (tiempoDeGeneracionRandom > 2f)
            {
                foreach (var semaforo in SemaforosNorteSur)
                {
                    semaforo.CambiarEstado();

                }

                foreach (var semaforo in SemaforosEsteOeste)
                {
                    semaforo.CambiarEstado();
                }

                tiempoDeGeneracionRandom = 0;
            }

            base.Update(gametime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var semaforo in SemaforosNorteSur)
            {
                semaforo.Draw(spriteBatch);
            }

            foreach (var semaforo in SemaforosEsteOeste)
            {
                semaforo.Draw(spriteBatch);
            }
        }

    }
}
