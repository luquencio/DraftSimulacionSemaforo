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
        private Texture2D[] texturas;
        private Rectangle barrera;
        private int dimensionXDeBarrera;
        private int dimensionYDeBarrera; 

        public Semaforo(Texture2D textura, Vector2 posicion,Estado estado , Texture2D[] texturas, Rectangle barrera)
            : base(textura, posicion)
        {
            this.texturas = texturas;
            this.estado = estado;
            this.barrera = barrera;
            dimensionXDeBarrera = (int)barrera.X;
            dimensionYDeBarrera = (int)barrera.Y;
        }      


        public Estado LeerEstado()
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

            dimensionXDeBarrera = 1;
            dimensionYDeBarrera = 1;

            LiberarBarrera();
            
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

            dimensionXDeBarrera = (int)barrera.X;
            dimensionYDeBarrera = (int)barrera.Y;
        }

        protected override Rectangle CreateBoundingBoxFromPosition(Vector2 posicion)
        {
            return new Rectangle(dimensionXDeBarrera, dimensionYDeBarrera, barrera.Width, barrera.Height);
        }

        //protected Rectangle Dimensiones { get { return dimensiones; } }

        
    }
}
