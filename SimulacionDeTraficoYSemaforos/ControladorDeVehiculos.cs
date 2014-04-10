using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SimulacionDeTraficoYSemaforos
{
    public class ControladorDeVehiculos : Controlador
    {
        private List<Vehiculo> vehiculos = new List<Vehiculo>();

        public ControladorDeVehiculos(Texture2D[] textura) :
            base(textura)
        {

        }


        //private void CrearVehiculo()
        //{
        //    var posicion = PosicionAletorea();
        //    var vehiculo = new Vehiculo();
        //    vehiculos.Add();
        //}

        public override void Update()
        {
            foreach (var vehiculo in vehiculos)
            {
                vehiculo.Update();
            }

            for (int i = 0; i < vehiculos.Count; i++)
            {
                //if (!bounds.Contains(vehiculos[i].BoundingBox))
                //{
                //    vehiculos.Remove(vehiculos[i]);
                //}
            }

            base.Update();
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
    }
}
