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
        Random rand = new Random();
        private float tiempoDeGeneracionRandom = 0f;

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

        public ControladorDeVehiculos(Texture2D[] texturas, List<Tuple<Semaforo, Sincronizacion>> semaforos) :
            base(texturas)
        {
            this.semaforos = semaforos;
        }


        //private void CrearVehiculo()
        //{
        //    var posicion = PosicionAletorea();
        //    var vehiculo = new Vehiculo();
        //    vehiculos.Add();
        //}

        public override void Update(GameTime gametime)
        {
            foreach (var vehiculo in vehiculos)
            {
                vehiculo.Update(gametime);
            }

            //for (int i = 0; i < vehiculos.Count; i++)
            //{
            //    //if (!bounds.Contains(vehiculos[i].BoundingBox))
            //    //{
            //    //    vehiculos.Remove(vehiculos[i]);
            //    //}
            //}
            

            tiempoDeGeneracionRandom += (float)gametime.ElapsedGameTime.TotalSeconds;

            if (tiempoDeGeneracionRandom > 2f)
            {
                CrearVehiculos(rand.Next(vectoresOrigen.Length));
                tiempoDeGeneracionRandom = 0;
            }

            CheckCollisions();


            foreach (var vehiculo in vehiculos)
            {
                
                vehiculo.Arrancar();               
            }

            base.Update(gametime);
        }

        private void CheckCollisions()
        {
            CheckCrash();
            CheckInterseccion();
        }

        private void CrearVehiculos(int cantidad)
        {
            List<int> posicionesGeneradas = new List<int>();

            for (int vehiculo = 0; vehiculo < cantidad; vehiculo++)
            {
                int texturaRandom = rand.Next(TexturasDisponibles);
                int posicionRandom = rand.Next(vectoresOrigen.Length);

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
            Semaforo semaf = null;

            foreach (var semaforo in semaforos)
            {
                if ((direccion == Direccion.Norte || direccion == Direccion.Sur) && (semaforo.Item2 == Sincronizacion.NorteSur))
                {
                    semaf = semaforo.Item1;
                    break;
                }

                if ((direccion == Direccion.Este || direccion == Direccion.Oeste) && (semaforo.Item2 == Sincronizacion.EsteOeste))
                {
                    semaf = semaforo.Item1;
                    break;
                }

            }

            if (semaf == null)
            {
                throw (new Exception("Se cayo"));
            }

            return semaf;
        }
        

        private void CheckInterseccion()
        {
            for (int i = 0; i < vehiculos.Count; i++)
            {
                foreach (var semaforo in semaforos)
                {
                    if (vehiculos[i].BoundingBox.Intersects(semaforo.Item1.BoundingBox))
                    {
                        vehiculos[i].Detenerse();
                    }                    
                }                
            }
            
        }

        private void CheckCrash()
        {          

            for (int i = 0; i < vehiculos.Count; i++)
            {
                for (int j = 0; j < vehiculos.Count; j++)
                {
                    if ((i != j) && vehiculos[i].BoundingBox.Intersects(vehiculos[j].BoundingBox))
                    {
                        vehiculos[i].Detenerse();
                    }
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
    }
}
