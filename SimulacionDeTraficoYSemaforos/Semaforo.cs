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

        public Semaforo(Texture2D textura, Vector2 posicion,Estado estado , Texture2D[] texturas)
            : base(textura, posicion)
        {
            this.texturas = texturas;
            this.estado = estado;
        }

        //public void CambiarEstado() 
        //{
        //    if (this.estado == Estado.Verde)
        //    {
        //        CambiarARojo();
        //        //Thread cambiadorDeSemaforo = new Thread(CambiarARojo);
        //        //cambiadorDeSemaforo.Start();
        //        //cambiadorDeSemaforo.Join();
        //    }

        //    else
        //    {
        //        estado = Estado.Verde;
        //        textura = texturas[2];
        //    }

        //    //LiberarBarrera();

        //}

        //private void CambiarARojo()
        //{
        //    estado = Estado.Rojo;
        //    //this.textura = texturas[1];
        //    //Thread.Sleep(3000);
        //    this.textura = texturas[0];
        //}


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
    }
}
