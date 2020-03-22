using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace BattleCity
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        const int MAP_WIDTH = 26;
        const int MAP_HEIGHT = 26;
        const int TILE_WIDTH = 8;
        const int TILE_HEIGHT = 8;
        const float THUMBSTICK_THRESHOLD = 0.2f;

        readonly Vector2 OFFSET = new Vector2(TILE_WIDTH, TILE_HEIGHT);

        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        TextureList _alltextures = new TextureList(new List<Texture2D>());
        Texture2D _line; //base for the line texture

        Player _myPlayer = new Player();
        Player _theirPlayer = new Player();
        Map _mapUnder,_mapOver;

        Rectangle _myPlayerBlockedRect = new Rectangle();
        Rectangle _myPlayerBlockedMapRect = new Rectangle();
        bool _myPlayerWasBlocked = false;
        ProjectileSpawner _projectileSpawner = new ProjectileSpawner(new List<Projectile>());
        ExplosionSpawner _explosionSpawner = new ExplosionSpawner(new List<Explosion>());

        //Connection connection = new Connection();

        public Game1()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.HardwareModeSwitch = true;

            this.Window.IsBorderless = true;

            Resolution.Init(ref _graphics);
            // Change Virtual Resolution 
            Resolution.SetVirtualResolution(256, 224);
#if DEBUG         
            Resolution.SetResolution(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height, false);
#else
            Resolution.SetResolution(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height, true);
#endif

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
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // create 1x1 texture for line drawing
            _line = new Texture2D(GraphicsDevice, 1, 1);
            _line.SetData<Color>(
                new Color[] { Color.White });// fill the texture with white

            FileStream tempstream;
            Slide tmpSlide, keepSlide;
            SlideShow tmpSlideShow;
            SlideShowMachine tmpSlideShowMachine;
            int tmpIndex;

            tempstream = new FileStream("battlecity_tank.png", FileMode.Open);
            tmpIndex = _alltextures.AddReturningIndex(Texture2D.FromStream(GraphicsDevice, tempstream));
            tempstream.Close();
            tmpSlide = new Slide();
            tmpSlide.SetTextureIndex(tmpIndex);
            tmpSlide.SetOrigin(new Vector2(7.5f, 7.5f));
            tmpSlide.SetBoundingArea(new BoundingArea(new Vector2(1, 1), new Vector2(13, 13), new Vector2(6.5f, 6.5f)));
            tmpSlideShow = new SlideShow(new List<Slide>());
            tmpSlideShow.AddSlide(tmpSlide);
            tmpSlideShowMachine = new SlideShowMachine(new Dictionary<int,SlideShow>());
            tmpSlideShowMachine.AddSlideShow(SlideShowMachine.SLIDESHOW_RIGHT,tmpSlideShow);
            _myPlayer.SetSlideShowMachine(tmpSlideShowMachine);

            tempstream = new FileStream("battlecity_tank2.png", FileMode.Open);
            tmpIndex = _alltextures.AddReturningIndex(Texture2D.FromStream(GraphicsDevice, tempstream));
            tempstream.Close();
            tmpSlide = new Slide();
            tmpSlide.SetTextureIndex(tmpIndex);
            tmpSlide.SetOrigin(new Vector2(7.5f, 7.5f));
            var a = _alltextures.GetTextureFromIndex(tmpIndex).Bounds;
            tmpSlide.SetBoundingArea(new BoundingArea(new Vector2(1, 1), new Vector2(13, 13), new Vector2(6.5f, 6.5f)));

            tmpSlideShow = new SlideShow(new List<Slide>());
            tmpSlideShow.AddSlide(tmpSlide);
            tmpSlideShowMachine = new SlideShowMachine(new Dictionary<int, SlideShow>());
            tmpSlideShowMachine.AddSlideShow(SlideShowMachine.SLIDESHOW_RIGHT, tmpSlideShow);
            _theirPlayer.SetSlideShowMachine(tmpSlideShowMachine);

            tempstream = new FileStream("battlecity_shot.png", FileMode.Open);
            tmpIndex = _alltextures.AddReturningIndex(Texture2D.FromStream(GraphicsDevice, tempstream));
            tempstream.Close();
            tmpSlide = new Slide();
            tmpSlide.SetTextureIndex(tmpIndex);
            tmpSlide.SetOrigin(new Vector2(7.5f, 7.5f));
            tmpSlide.SetBoundingArea(new BoundingArea(new Vector2(6, 6), new Vector2(4, 3), new Vector2(2, 1.5f)));
            tmpSlideShow = new SlideShow(new List<Slide>());
            tmpSlideShow.AddSlide(tmpSlide);
            tmpSlideShowMachine = new SlideShowMachine(new Dictionary<int, SlideShow>());
            tmpSlideShowMachine.AddSlideShow(SlideShowMachine.SLIDESHOW_RIGHT, tmpSlideShow);
            _projectileSpawner.SetSlideShowMachine(tmpSlideShowMachine);

            tempstream = new FileStream("battlecity_explosion1.png", FileMode.Open);
            tmpIndex = _alltextures.AddReturningIndex(Texture2D.FromStream(GraphicsDevice, tempstream));
            tempstream.Close();
            tmpSlide = new Slide();
            tmpSlide.SetTextureIndex(tmpIndex);
            tmpSlide.SetOrigin(new Vector2(7.5f, 7.5f));
            tmpSlide.SetDisplayTime(0.2f);
            tmpSlideShow = new SlideShow(new List<Slide>());
            tmpSlideShow.AddSlide(tmpSlide);
            keepSlide = tmpSlide;
            tempstream = new FileStream("battlecity_explosion2.png", FileMode.Open);
            tmpIndex = _alltextures.AddReturningIndex(Texture2D.FromStream(GraphicsDevice, tempstream));
            tempstream.Close();
            tmpSlide = new Slide();
            tmpSlide.SetTextureIndex(tmpIndex);
            tmpSlide.SetOrigin(new Vector2(7.5f, 7.5f));
            tmpSlide.SetDisplayTime(0.2f);
            tmpSlideShow.AddSlide(tmpSlide);
            tmpSlideShow.AddSlide(keepSlide);
            tmpSlideShowMachine = new SlideShowMachine(new Dictionary<int, SlideShow>());
            tmpSlideShowMachine.AddSlideShow(SlideShowMachine.SLIDESHOW_RIGHT, tmpSlideShow);
            _explosionSpawner.SetSlideShowMachine(tmpSlideShowMachine);

            tempstream = new FileStream("battlecity_tile_asphalt.png", FileMode.Open);
            int tileAsphaltIndex = _alltextures.AddReturningIndex(Texture2D.FromStream(GraphicsDevice, tempstream));
            tempstream.Close();

            tempstream = new FileStream("battlecity_tile_brick.png", FileMode.Open);
            int tileBrickIndex = _alltextures.AddReturningIndex(Texture2D.FromStream(GraphicsDevice, tempstream));
            tempstream.Close();

            tempstream = new FileStream("battlecity_tile_metal.png", FileMode.Open);
            int tileMetalIndex = _alltextures.AddReturningIndex(Texture2D.FromStream(GraphicsDevice, tempstream));
            tempstream.Close();

            tempstream = new FileStream("battlecity_tile_tree.png", FileMode.Open);
            int tileTreeIndex = _alltextures.AddReturningIndex(Texture2D.FromStream(GraphicsDevice, tempstream));
            tempstream.Close();

            //TODO: En lista över Tiles som finns,
            //skapa sen kartan från kopior av dessa (eller samma Tile, jag vet inte om de ändras)

            Random rnd = new Random();
            _mapUnder = new Map(MAP_WIDTH, MAP_HEIGHT, TILE_WIDTH, TILE_HEIGHT);
            _mapOver = new Map(MAP_WIDTH, MAP_HEIGHT, TILE_WIDTH, TILE_HEIGHT);
            Tile currentTile;
            for (int y = 0; y < MAP_HEIGHT; y++)
            {
                for (int x = 0; x < MAP_WIDTH; x++)
                {
                    currentTile = new Tile();
                    currentTile.SetBoundingArea(new BoundingArea(new Vector2(x * TILE_WIDTH, y * TILE_HEIGHT), new Vector2(TILE_WIDTH, TILE_HEIGHT), new Vector2(0,0)));

                    int rndVal = rnd.Next(100);

                    //sätt asfalt där spelarna spawnar
                    if (x <= 4 && y <= 4)
                        rndVal = 0;

                    if (rndVal > 96)
                    {                        
                        currentTile.SetTextureIndex(tileMetalIndex);
                        currentTile.SetIsBlocked(true);
                    }
                    else if (rndVal > 89)
                    {
                        currentTile.SetTextureIndex(tileBrickIndex);
                        currentTile.SetTextureIndexDestroyed(tileAsphaltIndex);
                        currentTile.SetIsBlocked(true);
                        //if (rndVal > 95)
                        //    currentTile.SetLeftDownDestroyed(true);
                        //if (rndVal > 94)
                        //    currentTile.SetLeftUpDestroyed(true);
                        //if (rndVal > 93)
                        //    currentTile.SetRightDownDestroyed(true);
                        //if (rndVal > 92)
                        //    currentTile.SetRightUpDestroyed(true);

                    }
                    else
                    {
                        currentTile.SetTextureIndex(tileAsphaltIndex);

                        if (rndVal > 50)
                        {
                            Tile topTile = new Tile();
                            topTile.SetTextureIndex(tileTreeIndex);
                            _mapOver.SetTileAtPosition(x, y, topTile);
                        }
                    }

                    _mapUnder.SetTileAtPosition(x, y, currentTile);    
                }
                //TODO: Kolla att det är ett tomt område (eller sätt asfalt där)
                _myPlayer.SetPos(new Vector2(16f, 20f));
                _theirPlayer.SetPos(new Vector2(32f, 20f));

            }
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

            if (!_myPlayer.GetDestroyed())
            {
                //myPlayer
                Vector2 myPosBefore = _myPlayer.GetPos();
                if (Keyboard.GetState().IsKeyDown(Keys.Left) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -THUMBSTICK_THRESHOLD) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X < -THUMBSTICK_THRESHOLD) || (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed))
                {
                    _myPlayer.MoveLeft(0.7f, (float)gameTime.ElapsedGameTime.TotalSeconds);
                    _myPlayer.SetRotationLeft();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > THUMBSTICK_THRESHOLD) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X > THUMBSTICK_THRESHOLD) || (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed))
                {
                    _myPlayer.MoveRight(0.7f, (float)gameTime.ElapsedGameTime.TotalSeconds);
                    _myPlayer.SetRotationRight();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > THUMBSTICK_THRESHOLD) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y > THUMBSTICK_THRESHOLD) || (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed))
                {
                    _myPlayer.MoveUp(0.7f, (float)gameTime.ElapsedGameTime.TotalSeconds);
                    _myPlayer.SetRotationUp();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -THUMBSTICK_THRESHOLD) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y < -THUMBSTICK_THRESHOLD) || (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed))
                {
                    _myPlayer.MoveDown(0.7f, (float)gameTime.ElapsedGameTime.TotalSeconds);
                    _myPlayer.SetRotationDown();
                }

                if (Keyboard.GetState().IsKeyDown(Keys.RightControl) || (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed))
                {
                    if (/*(!_myPlayer.GetFirePushed()) && */((gameTime.TotalGameTime - _myPlayer.GetTimeFireWasPushed()).TotalSeconds > 0.25))
                    {
                        _projectileSpawner.Spawn(_myPlayer.GetPos(), _myPlayer.GetRotation(), 1f);
                        _myPlayer.SetFirePushed(true);
                        _myPlayer.SetTimeFireWasPushed(gameTime.TotalGameTime);
                    }
                }
                else
                {
                    _myPlayer.SetFirePushed(false);
                }

                //TODO: Gör en bättre kontroll här
                Nullable<Rectangle> rectIsBlocking = _mapUnder.GetRectIfPlayerIsBlocked(_myPlayer.GetBoundingArea());
                if (rectIsBlocking != null)
                {
                    _myPlayerWasBlocked = true;
                    //_myPlayerBlockedRect = _myPlayer.GetBoundingingRect();
                    _myPlayerBlockedMapRect = rectIsBlocking.Value;

                    _myPlayer.SetPos(myPosBefore);
                    _myPlayerBlockedRect = _myPlayer.GetBoundingingRect();
                }
                else
                    _myPlayerWasBlocked = false;

                if (_projectileSpawner.RemoveIfHittingBound(_myPlayer.GetBoundingArea()))
                {
                    _myPlayer.SetDestroyed(true);
                    _myPlayer.SetTimeWhenDestroyed(gameTime.TotalGameTime);
                    _explosionSpawner.Spawn(_myPlayer.GetPos(), 1f);
                }
            }


            if (!_theirPlayer.GetDestroyed())
            {
                //theirPlayer
                Vector2 theirPosBefore = _theirPlayer.GetPos();
                if (Keyboard.GetState().IsKeyDown(Keys.A) || (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X < -THUMBSTICK_THRESHOLD) || (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Right.X < -THUMBSTICK_THRESHOLD) || (GamePad.GetState(PlayerIndex.Two).DPad.Left == ButtonState.Pressed))
                {
                    _theirPlayer.MoveLeft(0.7f, (float)gameTime.ElapsedGameTime.TotalSeconds);
                    _theirPlayer.SetRotationLeft();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D) || (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X > THUMBSTICK_THRESHOLD) || (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Right.X > THUMBSTICK_THRESHOLD) || (GamePad.GetState(PlayerIndex.Two).DPad.Right == ButtonState.Pressed))
                {
                    _theirPlayer.MoveRight(0.7f, (float)gameTime.ElapsedGameTime.TotalSeconds);
                    _theirPlayer.SetRotationRight();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.W) || (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y > THUMBSTICK_THRESHOLD) || (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Right.Y > THUMBSTICK_THRESHOLD) || (GamePad.GetState(PlayerIndex.Two).DPad.Up == ButtonState.Pressed))
                {
                    _theirPlayer.MoveUp(0.7f, (float)gameTime.ElapsedGameTime.TotalSeconds);
                    _theirPlayer.SetRotationUp();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S) || (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y < -THUMBSTICK_THRESHOLD) || (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Right.Y < -THUMBSTICK_THRESHOLD) || (GamePad.GetState(PlayerIndex.Two).DPad.Down == ButtonState.Pressed))
                {
                    _theirPlayer.MoveDown(0.7f, (float)gameTime.ElapsedGameTime.TotalSeconds);
                    _theirPlayer.SetRotationDown();
                }

                if (Keyboard.GetState().IsKeyDown(Keys.LeftControl) || (GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed))
                {
                    if (/*(!_theirPlayer.GetFirePushed()) && */((gameTime.TotalGameTime - _theirPlayer.GetTimeFireWasPushed()).TotalSeconds > 0.25))
                    {
                        _projectileSpawner.Spawn(_theirPlayer.GetPos(), _theirPlayer.GetRotation(), 1f);
                        _theirPlayer.SetFirePushed(true);
                        _theirPlayer.SetTimeFireWasPushed(gameTime.TotalGameTime);
                    }
                }
                else
                {
                    _theirPlayer.SetFirePushed(false);
                }

                if (_mapUnder.GetRectIfPlayerIsBlocked(_theirPlayer.GetBoundingArea()) != null)
                    _theirPlayer.SetPos(theirPosBefore);

                if (_projectileSpawner.RemoveIfHittingBound(_theirPlayer.GetBoundingArea()))
                {
                    _theirPlayer.SetDestroyed(true);
                    _theirPlayer.SetTimeWhenDestroyed(gameTime.TotalGameTime);
                    _explosionSpawner.Spawn(_theirPlayer.GetPos(), 1f);
                }
            }


            _projectileSpawner.Move((float)gameTime.ElapsedGameTime.TotalSeconds);
            _projectileSpawner.RemoveIfOutsideBounds(_mapUnder.GetBoundingArea());

            _explosionSpawner.AddElapsedSeconds((float)gameTime.ElapsedGameTime.TotalSeconds);
            _explosionSpawner.RemoveIfLifetimePassed();

            _mapUnder.ProjectileCheck(_projectileSpawner);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Resolution.BeginDraw();

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Resolution.getTransformationMatrix());
            _mapUnder.Draw(OFFSET,_spriteBatch, _alltextures);
            if (!_myPlayer.GetDestroyed())
            {
                _spriteBatch.Draw(_myPlayer.GetTextureFromList(_alltextures), _myPlayer.GetPos() + OFFSET, null, Color.White, _myPlayer.GetRotation(), _myPlayer.GetOrigin(), 1f, SpriteEffects.None, 0f);
            }
            if (!_theirPlayer.GetDestroyed())
            {
                _spriteBatch.Draw(_theirPlayer.GetTextureFromList(_alltextures), _theirPlayer.GetPos() + OFFSET, null, Color.White, _theirPlayer.GetRotation(), _theirPlayer.GetOrigin(), 1f, SpriteEffects.None, 0f);
            }
            _projectileSpawner.Draw(OFFSET, _spriteBatch, _alltextures);
            _mapOver.Draw(OFFSET,_spriteBatch, _alltextures);
            _explosionSpawner.Draw(OFFSET, _spriteBatch, _alltextures);

            if (_myPlayerWasBlocked)
            {

                DrawRectangle(_spriteBatch, OFFSET, _myPlayerBlockedRect, new Color(Color.Red, 0.1f));
                DrawRectangle(_spriteBatch, OFFSET, _myPlayerBlockedMapRect, new Color(Color.Turquoise,0.1f));                
            }

            //DrawLine(_spriteBatch, //draw line
            //    _myPlayer.GetPos() - _myPlayer.GetOrigin(), //start of line
            //    _myPlayer.GetPos() + _myPlayer.GetOrigin(), //end of line
            //    Color.Turquoise
            //);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        void DrawRectangle(SpriteBatch sb, Vector2 offset, Rectangle rect, Color lineColor)
        {
            DrawLine(sb, new Vector2(rect.X, rect.Y) + offset, new Vector2(rect.X + rect.Width, rect.Y) + offset, lineColor);
            DrawLine(sb, new Vector2(rect.X + rect.Width, rect.Y) + offset, new Vector2(rect.X + rect.Width, rect.Y + rect.Height) + offset, lineColor);
            DrawLine(sb, new Vector2(rect.X + rect.Width, rect.Y + rect.Height) + offset, new Vector2(rect.X, rect.Y + rect.Height) + offset, lineColor);
            DrawLine(sb, new Vector2(rect.X, rect.Y + rect.Height) + offset, new Vector2(rect.X, rect.Y) + offset, lineColor);
        }

        void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end, Color lineColor)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(_line,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                lineColor, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }

    }
}
