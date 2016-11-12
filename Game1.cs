using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace IndependentResolutionRendering
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D _texture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Resolution.Init(ref graphics);
            Content.RootDirectory = "Content";
            // Change Virtual Resolution 
            Resolution.SetVirtualResolution(1024, 768);
            Resolution.SetResolution(1280, 800, false);
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _texture = Content.Load<Texture2D>("alley_1024x768");
            //_texture = Content.Load<Texture2D>("Braid_screenshot8");
            

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Resolution.BeginDraw();

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend,SpriteSortMode.Immediate,SaveStateMode.SaveState,Resolution.getTransformationMatrix());
            spriteBatch.Draw(_texture, Vector2.Zero, Color.White);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
