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
        bool cambieEstado;

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
                // arreglar esto
                SemaforosNorteSur.Add(new Semaforo(Texturas[0], semaforo, Estado.Rojo, Texturas, new Rectangle((int)semaforo.X, (int)semaforo.Y + 15, 31, 1)));
            }

            foreach (var semaforo in vectoresBarreraEsteOeste)
	        {
                SemaforosEsteOeste.Add(new Semaforo(Texturas[2], semaforo, Estado.Verde, Texturas, new Rectangle((int)semaforo.X + 15, (int)semaforo.Y, 1, 31)));
	        }
        }

        public override void Update(GameTime gametime)
        {
            tiempoDeGeneracionRandom += (float)gametime.ElapsedGameTime.TotalSeconds;

            

            if (tiempoDeGeneracionRandom > 4f && cambieEstado == false)
            {
                foreach (var semaforo in SemaforosNorteSur)
                {

                    if (semaforo.Estado == Estado.Verde)
                    {
                        semaforo.CambiarAAmarillo();
                    }

                }

                foreach (var semaforo in SemaforosEsteOeste)
                {
                    if (semaforo.Estado == Estado.Verde)
                    {
                        semaforo.CambiarAAmarillo();
                    }
                }

                cambieEstado = true;
            }


            if (tiempoDeGeneracionRandom > 7f)
            {
                foreach (var semaforo in SemaforosNorteSur)
                {

                    if (semaforo.Estado == Estado.Amarillo)
                    {
                        semaforo.CambiarARojo();
                    }

                    else if (semaforo.Estado == Estado.Rojo)
                    {
                        semaforo.CambiarAVerde();
                    }

                }

                foreach (var semaforo in SemaforosEsteOeste)
                {
                    if (semaforo.Estado == Estado.Amarillo)
                    {
                        semaforo.CambiarARojo();
                    }

                    else if (semaforo.Estado == Estado.Rojo)
                    {
                        semaforo.CambiarAVerde();
                    }
                }

                cambieEstado = false;
                tiempoDeGeneracionRandom = 0;
            }

            base.Update(gametime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var semaforo in Semaforos)
            {
                semaforo.Draw(spriteBatch);
            }
        }

        public List<Semaforo> Semaforos { get { return GenerarListaDeSemaforos(); } }

        private List<Semaforo> GenerarListaDeSemaforos()
        {
            List<Semaforo> semaforos = new List<Semaforo>();

            foreach (var semaforo in SemaforosNorteSur)
            {
                semaforos.Add(semaforo);
            }

            foreach (var semaforo in SemaforosEsteOeste)
            {
                semaforos.Add(semaforo);
            }

            return semaforos;
        }




    }
}
