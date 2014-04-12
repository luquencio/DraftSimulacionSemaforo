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
        private Sincronizacion sincronizacion;

        public Semaforo(Texture2D textura, Vector2 posicion, Estado estado, Texture2D[] texturas, Rectangle barrera, Sincronizacion sincronizacion)
            : base(textura, posicion)
        {
            this.texturas = texturas;
            this.estado = estado;
            this.barrera = barrera;
            dimensionXDeBarrera = (int)barrera.X;
            dimensionYDeBarrera = (int)barrera.Y;
            this.sincronizacion = sincronizacion;
        }           


        public Estado EsperarSemaforo()
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
            if (sincronizacion == Sincronizacion.NorteSur)
            {

                textura = texturas[5];
                
            }

            else
            {
                textura = texturas[2];
            }
            estado = Estado.Verde;
            

            dimensionXDeBarrera = 1;
            dimensionYDeBarrera = 1;

            LiberarBarrera();
            
        }

        internal void CambiarAAmarillo()
        {
            if (sincronizacion == Sincronizacion.NorteSur)
            {
                textura = texturas[4];

            }

            else
            {
                
                textura = texturas[1];
            }

            estado = Estado.Amarillo;
            dimensionXDeBarrera = (int)barrera.X;
            dimensionYDeBarrera = (int)barrera.Y;
        }

        internal void CambiarARojo()
        {
            if (sincronizacion == Sincronizacion.NorteSur)
            {

                textura = texturas[3];

            }

            else
            {
                
                textura = texturas[0];
            }

            estado = Estado.Rojo;

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
