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
    public class Vehiculo : Sprite
    {
        private const float VELOCIDAD = 0.8f;
        private Vector2 velocidad;
        private Vector2 centroDeRotacion;
        private float anguloDeRotacion;
        Direccion direccion;
        Rectangle dimensiones;
        Semaforo semaforo;
        public bool viSemaforo = true;

        public Vehiculo(Texture2D textura, Vector2 posicion, Direccion direccion, Semaforo semaforo) :
            base(textura, posicion)
        {
            this.direccion = direccion;
            this.velocidad = DeterminarDireccionDeVelocidad(direccion);
            // esta asignacion se puede eliminar
            this.centroDeRotacion = DeterminarCentroDeRotacion(textura);
            this.anguloDeRotacion = DeterminarAnguloDeRotacion(direccion);
            this.dimensiones = Dimensiones(direccion);
            this.semaforo = semaforo;
            
            // DeterminarCentroDeRotacion(texturaRandom), DeterminarAnguloDeRotacion(direccion),
            //                  DeterminarDireccionDeVelocidad(direccion)
            // Dimensiones(direccion)
        }

        private Vector2 DeterminarDireccionDeVelocidad(Direccion direccion)
        {
            Vector2 velocidad = new Vector2(0, 0);

            switch (direccion)
            {
                case Direccion.Norte:
                    velocidad.X = 0f;
                    velocidad.Y = -VELOCIDAD;
                    break;
                case Direccion.Sur:
                    velocidad.X = 0f;
                    velocidad.Y = VELOCIDAD;
                    break;
                case Direccion.Este:
                    velocidad.X = VELOCIDAD;
                    velocidad.Y = 0f;
                    break;
                case Direccion.Oeste:
                    velocidad.X = -VELOCIDAD;
                    velocidad.Y = 0f;
                    break;
                default:
                    break;
            }

            return velocidad;
        }

        private Rectangle Dimensiones(Direccion direccion)
        {

            Rectangle dimensiones = new Rectangle(0, 0, 0, 0);

            if (direccion == Direccion.Norte || direccion == Direccion.Sur)
            {
                dimensiones.Width = 11;
                dimensiones.Height = 26;
            }
            else
            {
                dimensiones.Width = 26;
                dimensiones.Height = 11;
            }

            return dimensiones;
        }

        private float DeterminarAnguloDeRotacion(Direccion direccion)
        {
            float angulo = 0f;

            switch (direccion)
            {
                case Direccion.Norte:
                    angulo = RotarAlNorte;
                    break;
                case Direccion.Sur:
                    angulo = RotarAlSur;
                    break;
                case Direccion.Este:
                    angulo = RotarAlEste;
                    break;
                case Direccion.Oeste:
                    angulo = RotarAlOeste;
                    break;
                default:
                    break;
            }

            return angulo;
        }

        private Vector2 DeterminarCentroDeRotacion(Texture2D textura)
        {
            Vector2 centroDePosicion;
            centroDePosicion.X = textura.Width / 2;
            centroDePosicion.Y = textura.Height / 2;
            return centroDePosicion;
        }
        

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, posicion, null, Color.White, anguloDeRotacion,
            centroDeRotacion, 1.0f, SpriteEffects.None, 0f);
        }

        public override void Update(GameTime gametime)
        {
            posicion += velocidad;           

            base.Update(gametime);
        }


        public void Detenerse()
        {

            velocidad.X = 0f;
            velocidad.Y = 0f;

            viSemaforo = false;

            //Thread VerSemaforo = new Thread(semaforo.EsperarSemaforo);
            //VerSemaforo.Start(this);
            
        }

        protected override Rectangle CreateBoundingBoxFromPosition(Vector2 posicion)
        {
            return new Rectangle((int)posicion.X - 7, (int)posicion.Y - 7, dimensiones.Width, dimensiones.Height);
        }

        public Direccion Direccion { get { return direccion; } }
        private float RotarAlNorte { get { return MathHelper.TwoPi; } }
        private float RotarAlSur { get { return MathHelper.Pi; } }
        private float RotarAlEste { get { return MathHelper.PiOver2; } }
        private float RotarAlOeste { get { return (MathHelper.Pi + MathHelper.PiOver2); } }

        public void Arrancar()
        {
            //semaforo.EsperarSemaforo(this);

            if (semaforo.Estado != Estado.Rojo)
            {
                velocidad = DeterminarDireccionDeVelocidad(direccion);
            }        
        }

        public Vector2 Velocity { get { return velocidad; } }


        internal void Moverse()
        {
            velocidad = DeterminarDireccionDeVelocidad(direccion);
        }
    }
}
