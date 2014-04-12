using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SimulacionDeTraficoYSemaforos
{
    class ControlDeTransito
    {
        ControladorDeVehiculos controladorDeVehiculos;
        ControladorDeSemaforos controladorDeSemaforos; 

        public ControlDeTransito(ContentManager Content)
        {
            Texture2D[] texturasDeCarros = { Content.Load<Texture2D>("CamionetaPeq"),
                                             Content.Load<Texture2D>("CarroAzulPeq"),
                                             Content.Load<Texture2D>("CarroRojoPeq"),
                                             Content.Load<Texture2D>("CamionPeq"),
                                             Content.Load<Texture2D>("PoliciaPeq") };

            Texture2D[] texturasDeSemaforos = { Content.Load<Texture2D>("SemaforoRojoPeq"), 
                                               Content.Load<Texture2D>("SemaforoAmarilloPeq"),
                                               Content.Load<Texture2D>("SemaforoVerdePeq")
                                             };

            controladorDeSemaforos = new ControladorDeSemaforos(texturasDeSemaforos);
            controladorDeVehiculos = new ControladorDeVehiculos(texturasDeCarros, controladorDeSemaforos.Semaforos);
                     
        }

        public void Update(GameTime gameTime)
        {
            controladorDeVehiculos.Update(gameTime);
            controladorDeSemaforos.Update(gameTime);
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            controladorDeVehiculos.Draw(spriteBatch);
            controladorDeSemaforos.Draw(spriteBatch);
        }

        internal void Draw(SpriteBatch spriteBatch, Game1 game1)
        {
            controladorDeSemaforos.Draw(spriteBatch, game1);
        }
    }
}
