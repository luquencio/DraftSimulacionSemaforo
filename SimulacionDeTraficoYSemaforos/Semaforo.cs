using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace SimulacionDeTraficoYSemaforos
{
    public enum Estado
    {
        Rojo,
        Amarillo,
        Verde
    }

    public class Semaforo : Sprite
    {
        private Estado estado;
        private object _lock = new object();
        private Rectangle barrera;

        public Semaforo(Texture2D textura, Vector2 posicion)
            : base(textura, posicion)
        {

        }

        private Estado LeerEstado()
        {
            // parte critical
            lock (_lock)
            {
                while (estado != Estado.Verde)
                {
                    Monitor.Wait(_lock);
                }

                return estado;
            }
        }

        public void LiberarBarrera()
        {
            lock (_lock)
            {
                Monitor.PulseAll(_lock);
            }
        }

    }
}
