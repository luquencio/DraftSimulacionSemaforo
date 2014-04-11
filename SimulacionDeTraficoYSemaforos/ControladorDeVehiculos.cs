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
        Random rand;

        private Tuple<Vector2, Direccion>[] vectoresOrigen = { new Tuple<Vector2, Direccion> ( new Vector2(390,-40), Direccion.Sur ),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(659,-40), Direccion.Sur ),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(677,-40), Direccion.Sur),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(834,187), Direccion.Oeste ),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(834,216), Direccion.Oeste ),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(676,498), Direccion.Norte ),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(658,498), Direccion.Norte ),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(407,498), Direccion.Norte ),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(-45,271), Direccion.Este ),
                                                               new Tuple<Vector2, Direccion> ( new Vector2(-45,271), Direccion.Este )
                                                             };

        public ControladorDeVehiculos(Texture2D[] textura) :
            base(textura)
        {
            Random rand = new Random();

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

            for (int i = 0; i < vehiculos.Count; i++)
            {
                //if (!bounds.Contains(vehiculos[i].BoundingBox))
                //{
                //    vehiculos.Remove(vehiculos[i]);
                //}
            }

            CrearVehiculos(rand.Next(10));

            base.Update(gametime);
        }

        private void CrearVehiculos(int cantidad)
        {
            
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
                for (int j = i + 1; j < vehiculos.Count; j++)
                {
                    // en cuenta
                    if (vehiculos[i].BoundingBox.Intersects(vehiculos[j].BoundingBox))
                    {
                        vehiculos[i].Crash();
                    }

                }
            }

            //foreach (var vehiculoA in vehiculos)
            //{
            //    foreach (var vehiculoB in vehiculos)
            //    {
            //        if (vehiculoA.BoundingBox.Intersects(vehiculoB.BoundingBox))
            //        {
            //            vehiculoA.Crash();
            //        }
            //    }

            //}
        }

        private float RotarAlNorte { get { return MathHelper.TwoPi; } }
        private float RotarAlSur { get { return MathHelper.Pi; } }
        private float RotarAlEste { get { return MathHelper.PiOver2; } }
        private float RotarAlOeste { get { return (MathHelper.Pi + MathHelper.PiOver2); } }
    }
}
