using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SimulacionDeTraficoYSemaforos
{
    public enum Sincronizacion
    {
        NorteSur,
        EsteOeste
    }

    class ControladorDeSemaforos : Controlador
    {
        List<Tuple<Semaforo, Sincronizacion>> semaforos = new List<Tuple<Semaforo, Sincronizacion>>();
        private float tiempoDeGeneracionRandom = 0f;
        bool cambieEstado;

        private Vector2[] vectoresBarreraNorteSur = { new Vector2(115, 193),
                                                      new Vector2(365,193),
                                                     
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
                Semaforo semaforoTemporal = new Semaforo(Texturas[0], semaforo, Estado.Rojo, Texturas, new Rectangle((int)semaforo.X, (int)semaforo.Y, 31, 1));
                semaforos.Add(new Tuple<Semaforo, Sincronizacion> (semaforoTemporal, Sincronizacion.NorteSur ));
            }

            foreach (var semaforo in vectoresBarreraEsteOeste)
	        {
                Semaforo semaforoTemporal = new Semaforo(Texturas[2], semaforo, Estado.Verde, Texturas, new Rectangle((int)semaforo.X, (int)semaforo.Y, 1, 31));

                semaforos.Add(new Tuple<Semaforo, Sincronizacion>(semaforoTemporal, Sincronizacion.EsteOeste));
	        }
        }

        public override void Update(GameTime gametime)
        {
            tiempoDeGeneracionRandom += (float)gametime.ElapsedGameTime.TotalSeconds;

            

            if (tiempoDeGeneracionRandom > 4f && cambieEstado == false)
            {
                foreach (var semaforo in semaforos)
                {
                    if (semaforo.Item1.Estado == Estado.Verde)
                    {
                        semaforo.Item1.CambiarAAmarillo();
                    }
                }

                cambieEstado = true;
            }


            if (tiempoDeGeneracionRandom > 7f)
            {
                foreach (var semaforo in semaforos)
                {
                    if (semaforo.Item1.Estado == Estado.Amarillo)
                    {
                        semaforo.Item1.CambiarARojo();
                    }

                    else if (semaforo.Item1.Estado == Estado.Rojo)
                    {
                        semaforo.Item1.CambiarAVerde();
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
                semaforo.Item1.Draw(spriteBatch);
            }
        }

        public List<Tuple<Semaforo, Sincronizacion>> Semaforos { get { return semaforos; } }

        //private List<Semaforo> GenerarListaDeSemaforos()
        //{
        //    List<Semaforo> semaforos = new List<Semaforo>();

        //    foreach (var semaforo in semaforosNorteSur)
        //    {
        //        semaforos.Add(semaforo);
        //    }

        //    foreach (var semaforo in semaforosEsteOeste)
        //    {
        //        semaforos.Add(semaforo);
        //    }

        //    return semaforos;
        //}





        internal void Draw(SpriteBatch spriteBatch, Game1 game1)
        {
            foreach (var semaforo in Semaforos)
            {
                semaforo.Item1.Draw(spriteBatch);
                //game1.DrawRectangle(semaforo.Item1.BoundingBox, Color.Fuchsia);
            }
        }
    }
}
