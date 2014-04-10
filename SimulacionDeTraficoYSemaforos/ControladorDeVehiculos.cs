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

        private Vector2[] vectoresOrigen = { new Vector2(385,-50),
                                             new Vector2(654,-50),
                                             new Vector2(672,-50),
                                             new Vector2(829,197),
                                             new Vector2(829,217),
                                             new Vector2(671,508),
                                             new Vector2(653,508),
                                             new Vector2(402,508),
                                             new Vector2(-50,261),
                                             new Vector2(-50,261),
                                           };

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

            base.Update(gametime);
        }

        private void CrearVehiculos()
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
    }
}
