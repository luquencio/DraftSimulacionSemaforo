using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SimulacionDeTraficoYSemaforos
{
    public class Vehiculo : Sprite
    {
        private Vector2 velocity;
        private Vector2 origin;
        private float AnguloDeRotacion;

        public Vehiculo(Texture2D textura, Vector2 posicion, Vector2 origin, float AnguloDeRotacion) :
            base(textura, posicion)
        {
            velocity = new Vector2(0, 0);
            this.origin = origin;
            this.AnguloDeRotacion = AnguloDeRotacion;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, posicion, null, Color.White, AnguloDeRotacion,
            origin, 1.0f, SpriteEffects.None, 0f);
        }

        public override void Update(GameTime gametime)
        {
            velocity.Y = -0.5f;

            posicion += velocity;

            base.Update(gametime);
        }

        public void Update(GameTime gametime, float vel)
        {
            velocity.Y = vel;

            posicion += velocity;

            base.Update(gametime);
        }

        public void Crash()
        {
            throw new NotImplementedException();

        }
        
    }
}
