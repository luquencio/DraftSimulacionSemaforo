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
    public enum Direccion
	{
	    Norte,
        Sur,
        Este,
        Oeste
	}

    public class ControladorDeVehiculos : Controlador
    {
        private List<Vehiculo> vehiculos = new List<Vehiculo>();
        Random generadorDeNumerosRandom = new Random();
        private float tiempoDeGeneracionRandom = 0f;
        private const float TIEMPOENTREGENERACIONRANDOM = 2f;
        Rectangle limites;

        private Tuple<Vector2, Direccion>[] vectoresOrigen = { new Tuple<Vector2, Direccion> ( new Vector2(121,-40), Direccion.Sur ),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(140,-40), Direccion.Sur ),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(390,-40), Direccion.Sur),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(834,203), Direccion.Oeste ),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(834,223), Direccion.Oeste ),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(676,498), Direccion.Norte ),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(658,498), Direccion.Norte ),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(407,498), Direccion.Norte ),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(-45,266), Direccion.Este ),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(-45,246), Direccion.Este )
                                                             };
        private List<Tuple<Semaforo, Sincronizacion>> semaforos;

        public ControladorDeVehiculos(Texture2D[] texturas, List<Tuple<Semaforo, Sincronizacion>> semaforos, Rectangle limites) :
            base(texturas)
        {
            this.semaforos = semaforos;
            this.limites = limites;
            this.limites.Inflate(60,50);
        }

        public override void Update(GameTime gametime)
        {
            IniciarYContinuarMovimiento();


            RemoverVehiculosFueraDeRango();


            GenerarVehiculosSegunTiempoTranscurrido(gametime);

            DeterminarInterraciones();

            AnimarVehiculos(gametime);        

            base.Update(gametime);
        }        

        private void IniciarYContinuarMovimiento()
        {
            foreach (var vehiculo in vehiculos)
            {
                vehiculo.Moverse();
                vehiculo.Arrancar();
            }
        }

        private void RemoverVehiculosFueraDeRango()
        {
            for (int i = 0; i < vehiculos.Count; i++)
            {
                if (!limites.Contains(vehiculos[i].BoundingBox))
                {
                    vehiculos.Remove(vehiculos[i]);
                }
            }
        }

        private void AnimarVehiculos(GameTime gametime)
        {
            foreach (var vehiculo in vehiculos)
            {
                vehiculo.Update(gametime);
            }
        }

        private void GenerarVehiculosSegunTiempoTranscurrido(GameTime gametime)
        {
            tiempoDeGeneracionRandom += (float)gametime.ElapsedGameTime.TotalSeconds;

            if (tiempoDeGeneracionRandom > TIEMPOENTREGENERACIONRANDOM)
            {
                CrearVehiculos(generadorDeNumerosRandom.Next(vectoresOrigen.Length));
                tiempoDeGeneracionRandom = 0;
            }
        }        

        private void CrearVehiculos(int cantidad)
        {
            List<int> posicionesGeneradas = new List<int>();

            for (int vehiculo = 0; vehiculo < cantidad; vehiculo++)
            {
                int texturaRandom = generadorDeNumerosRandom.Next(TexturasDisponibles);
                int posicionRandom = generadorDeNumerosRandom.Next(vectoresOrigen.Length);

                // si la posicion se repite generar nuevo numero sin crear vehiculo
                if (posicionesGeneradas.Contains(posicionRandom))
                {
                    vehiculo--;
                    continue;
                }

                posicionesGeneradas.Add(posicionRandom);
                Direccion direccion = vectoresOrigen[posicionRandom].Item2;

                vehiculos.Add(new Vehiculo(Texturas[texturaRandom], vectoresOrigen[posicionRandom].Item1, direccion, AsignarSemaforo(direccion)));
                
            }
        }

        private Semaforo AsignarSemaforo(Direccion direccion)
        {
            Semaforo semaforoAsignado = null;

            foreach (var semaforo in semaforos)
            {
                if ((direccion == Direccion.Norte || direccion == Direccion.Sur) && (semaforo.Item2 == Sincronizacion.NorteSur))
                {
                    semaforoAsignado = semaforo.Item1;
                    break;
                }

                if ((direccion == Direccion.Este || direccion == Direccion.Oeste) && (semaforo.Item2 == Sincronizacion.EsteOeste))
                {
                    semaforoAsignado = semaforo.Item1;
                    break;
                }

            }

            if (semaforoAsignado == null)
            {
                throw (new Exception("Error en asignacion de semaforos"));
            }

            return semaforoAsignado;
        }
        

        private void DeterminarInterraciones()
        {
            for (int carro = 0; carro < vehiculos.Count; carro++)
            {
                ChequearInterracionConCarros(carro);

                foreach (var semaforo in semaforos)
                {
                    ChequearInterraciones(carro, semaforo);
                }

                
            }

            
        }

        private void ChequearInterraciones(int carro, Tuple<Semaforo, Sincronizacion> semaforo) 
        {
            Rectangle LimiteFrontalDeVehiculo = new Rectangle(0, 0, 0, 0);

            switch (vehiculos[carro].Direccion)
            {
                case Direccion.Norte:
                    LimiteFrontalDeVehiculo = DeterminarSiPasarInterseccion(carro,vehiculos[carro].BoundingBox.Center.X, 
                                                                            vehiculos[carro].BoundingBox.Top, semaforo, 
                                                                            LimiteFrontalDeVehiculo);
                    break;
                case Direccion.Sur:
                    LimiteFrontalDeVehiculo = DeterminarSiPasarInterseccion(carro, vehiculos[carro].BoundingBox.Center.X,
                                                                            vehiculos[carro].BoundingBox.Bottom, semaforo,
                                                                            LimiteFrontalDeVehiculo);
                    break;
                case Direccion.Este:
                    
                    LimiteFrontalDeVehiculo = DeterminarSiPasarInterseccion(carro, vehiculos[carro].BoundingBox.Right, 
                                                                            vehiculos[carro].BoundingBox.Center.Y, semaforo,
                                                                            LimiteFrontalDeVehiculo);
                    break;
                case Direccion.Oeste:
                    LimiteFrontalDeVehiculo = DeterminarSiPasarInterseccion(carro, vehiculos[carro].BoundingBox.Left,
                                                                            vehiculos[carro].BoundingBox.Center.Y, semaforo,
                                                                            LimiteFrontalDeVehiculo);
                    break;
                default:
                    break;
            }
        }

        /*
         * Las coordenadas A y B sirven para determinar las coordenadas del centro y la parte frontal del vehiculo
         * Si el carro esta en direccion Norte o Sur la CoordenadaA es el centro y la CoordenadaB es la parte frontal
         * Si el carro esta en direccion Este u Oeste la CoordenadaB es el centro y la CoordenadaA es la parte frontal
         */
        private Rectangle DeterminarSiPasarInterseccion(int carro, int CoordenadaA, int CoordenadaB, 
                                                        Tuple<Semaforo, Sincronizacion> semaforo, Rectangle LimiteFrontalDeVehiculo)
        {
            LimiteFrontalDeVehiculo = new Rectangle(CoordenadaA,CoordenadaB , 1, 1);
            if (LimiteFrontalDeVehiculo.Intersects(semaforo.Item1.BoundingBox))
            {
                vehiculos[carro].Detenerse();
            }

            else
            {
                vehiculos[carro].Arrancar();
            }
            return LimiteFrontalDeVehiculo;
        }

        private void ChequearInterracionConCarros(int carro)
        {
            for (int otroCarro = 0; otroCarro < vehiculos.Count; otroCarro++)
            {
                if ((carro != otroCarro) && vehiculos[carro].BoundingBox.Intersects(vehiculos[otroCarro].BoundingBox))
                {
                    vehiculos[carro].Detenerse();
                }

                else
                {
                    vehiculos[carro].Arrancar();
                }
            }
        } 
        

        private float RotarAlNorte { get { return MathHelper.TwoPi; } }
        private float RotarAlSur { get { return MathHelper.Pi; } }
        private float RotarAlEste { get { return MathHelper.PiOver2; } }
        private float RotarAlOeste { get { return (MathHelper.Pi + MathHelper.PiOver2); } }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var vehiculo in vehiculos)
            {
                vehiculo.Draw(spriteBatch);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Game1 game1)
        {
            foreach (var vehiculo in vehiculos)
            {
                vehiculo.Draw(spriteBatch);
                //game1.DrawRectangle(vehiculo.BoundingBox, Color.Fuchsia);
            }


        }
    }
}
