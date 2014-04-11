#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace SimulacionDeTraficoYSemaforos
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D carrito;
        Texture2D fondo;
        private Vehiculo accord;
        private Vehiculo porshe;
        int contador = 0;
        private Vector2 origin;
        private float RotationAngle;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            

            Texture2D[] texturasDeCarros = { Content.Load<Texture2D>("CamionetaPeq"),
                                             Content.Load<Texture2D>("CarroAzulPeq"),
                                             Content.Load<Texture2D>("CarroRojoPeq"),
                                             Content.Load<Texture2D>("CamionPeq"),
                                             Content.Load<Texture2D>("PoliciaPeq") }; 


            // TODO: use this.Content to load your game content here
            carrito = Content.Load<Texture2D>("CamionetaPeq");
            fondo = Content.Load<Texture2D>("FondoCalle");
            

            var gameBoundaries = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            accord = new Vehiculo(Content.Load<Texture2D>("CarroAzulPeq"), new Vector2(653, 508), origin, 0f);
            //porshe = new Vehiculo(Content.Load<Texture2D>("CarroRojoPeq"),new Vector2(653, 508), origin, 0f);
            origin.X = Content.Load<Texture2D>("CarroAzul").Width / 2;
            origin.Y = Content.Load<Texture2D>("CarroAzul").Height / 2;
            float circle = MathHelper.Pi * 2;
            RotationAngle = RotationAngle % circle;

            

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Random rand = new Random();

            float speed1 = -0.5f;
            float speed2 = -0.5f;

            if (contador == 100)
            {
                speed1 = -(float)rand.Next(2);
                speed2 = -(float)rand.Next(2);
                contador = 0;
            }

            contador++;
            



            // TODO: Add your update logic here
            accord.Update(gameTime, speed1);


            //porshe.Update(gameTime,speed2 );


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();


            spriteBatch.Draw(fondo, Vector2.Zero, Color.White);
            accord.Draw(spriteBatch);
            //porshe.Draw(spriteBatch);

            spriteBatch.Draw(Content.Load<Texture2D>("CarroAzul"), new Vector2(385, 50), null, Color.White, MathHelper.TwoPi,
            origin, 1.0f, SpriteEffects.None, 0f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
