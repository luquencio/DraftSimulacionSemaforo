﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SimulacionDeTraficoYSemaforos
{
    public class Sprite
    {
        protected Texture2D textura;
        protected Vector2 posicion;
        private Rectangle dimensiones;

        public Sprite(Texture2D textura, Vector2 posicion, Rectangle dimensiones)
        {
            this.textura = textura;
            this.posicion = posicion;
            this.dimensiones = dimensiones;

            // (Velocity*(float) gameTime.ElapsedGameTime.TotalSeconds);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, posicion, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        //private bool Blocked(Vector2 newPosition)
        //{
        //    var boundingBox = CreateBoundingBoxFromPosition(newPosition);
        //    return !movementBounds.Contains(boundingBox);
        //}

//        x1 =(width * math.abs(math.cos(angle) + height * math.abs(math.sin(angle))*-.5
//x2 =(width * math.abs(math.cos(angle) + height * math.abs(math.sin(angle))*.5
//y1 =(width * math.abs(math.sin(angle) + height * math.abs(math.cos(angle))*-.5
//y2 =(width * math.abs(math.sin(angle) + height * math.abs(math.cos(angle))*.5

//width = x2-x1;
//height = y2-y1;

        protected virtual Rectangle CreateBoundingBoxFromPosition(Vector2 posicion)
        {
            return new Rectangle((int)posicion.X, (int)posicion.Y, (int)Width, (int)Height);
        }


        protected Vector2 Posicion { get { return posicion; } }
        protected float Width { get { return textura.Width; } }
        protected float Height { get { return textura.Height; } }
        protected Rectangle Dimensiones { get { return dimensiones; } }

        public Rectangle BoundingBox
        {
            get { return CreateBoundingBoxFromPosition(posicion); }
        }

        //protected Texture2D Textura { set { textura = value; } }
    }
}
