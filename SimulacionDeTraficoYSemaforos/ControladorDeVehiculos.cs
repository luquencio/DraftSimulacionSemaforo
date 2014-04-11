using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
        private const float VELOCIDAD = 0.5f;
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

        public ControladorDeVehiculos(Texture2D[] texturas) :
            base(texturas)
        {
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

            if (tiempoDeGeneracionRandom > 5f)
            {
                CrearVehiculos(rand.Next(vectoresOrigen.Length));
                tiempoDeGeneracionRandom = 0;
            }

            CheckCollisions();

            base.Update(gametime);
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

                vehiculos.Add(new Vehiculo(Texturas[texturaRandom], vectoresOrigen[posicionRandom].Item1,
                              DeterminarCentroDeRotacion(texturaRandom), DeterminarAnguloDeRotacion(vectoresOrigen[posicionRandom].Item2),
                              DeterminarDireccionDeVelocidad(vectoresOrigen[posicionRandom].Item2)));
                
            }
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

        private Vector2 DeterminarCentroDeRotacion(int texturaRandom)
        {
            Vector2 centroDePosicion;
            centroDePosicion.X = Texturas[texturaRandom].Width / 2;
            centroDePosicion.Y = Texturas[texturaRandom].Height / 2;
            return centroDePosicion;
        }

        private void CheckCollisions()
        {
            CheckCrash();
            CheckInterseccion();
        }

        private void CheckInterseccion()
        {

            //throw new NotImplementedException();
        }

        private void CheckCrash()
        {
            for (int i = 0; i < vehiculos.Count; i++)
            {
                for (int j = 0; j < vehiculos.Count; j++)
                {
                    // en cuenta
                    if ( (i != j) && vehiculos[i].BoundingBox.Intersects(vehiculos[j].BoundingBox))
                    {
                        vehiculos[i].Crash();
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
