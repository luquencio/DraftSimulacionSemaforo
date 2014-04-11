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
        private Texture2D[] texturas;

        public Semaforo(Texture2D textura, Vector2 posicion,Estado estado , Texture2D[] texturas, Rectangle dimensiones)
            : base(textura, posicion, dimensiones)
        {
            this.texturas = texturas;
            this.estado = estado;
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

        public Estado Estado { get { return estado; } }


        internal void CambiarAVerde()
        {
            estado = Estado.Verde;
            textura = texturas[2];
        }

        internal void CambiarAAmarillo()
        {
            estado = Estado.Amarillo;
            textura = texturas[1];
        }

        internal void CambiarARojo()
        {
            estado = Estado.Rojo;
            textura = texturas[0];
        }

        protected override Rectangle CreateBoundingBoxFromPosition(Vector2 posicion)
        {
            return new Rectangle((int)Dimensiones.X, (int)Dimensiones.Y, Dimensiones.Width, Dimensiones.Height);
        }

        
    }
}
