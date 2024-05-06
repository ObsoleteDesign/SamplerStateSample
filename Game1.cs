using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SamplerStateSample
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D mandelbrot;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            mandelbrot = Content.Load<Texture2D>("mandelbrot");
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //Default effect of SpriteBach will use LinearClamp so any UV coords beyond 0-1 will clamp to last pixel on that 'edge'
            _spriteBatch.Begin();
            _spriteBatch.Draw(mandelbrot, new Rectangle(64, 64, mandelbrot.Width * 2, mandelbrot.Height * 2),
                new Rectangle(0, 0, mandelbrot.Width * 2, mandelbrot.Height * 2), Color.White);
            _spriteBatch.End();

            //Some of the predefined static SamplerStates have different options you can imply when drawing sprites
            _spriteBatch.Begin(samplerState: SamplerState.PointWrap);
            _spriteBatch.Draw(mandelbrot, new Rectangle(256, 64, mandelbrot.Width * 2, mandelbrot.Height * 2),
                new Rectangle(0, 0, mandelbrot.Width * 2, mandelbrot.Height * 2), Color.White);
            _spriteBatch.End();

            //To get more complex results we must configure our own SamplerState
            //Mirrors texture on UV coords.  SamplerState has various other options as well
            _spriteBatch.Begin(samplerState: new SamplerState() {  AddressU = TextureAddressMode.Mirror, AddressV = TextureAddressMode.Mirror });
            _spriteBatch.Draw(mandelbrot, new Rectangle(64, 256, mandelbrot.Width * 2, mandelbrot.Height * 2),
                new Rectangle(0, 0, mandelbrot.Width * 2, mandelbrot.Height * 2), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
