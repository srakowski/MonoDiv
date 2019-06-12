using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDiv.Example.Divs;

namespace MonoDiv.Example
{
    class ExampleGame : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        View<App> _view;
        SpriteFont _font;

        public ExampleGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 720;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _view = new View<App>();
            _view.RegisterDiv<HelloWorld>();
            _view.Initialize();
        }

        protected override void LoadContent()
        {
            _font = Content.Load<SpriteFont>("Default");
        }

        protected override void Update(GameTime gameTime)
        {
            _view.Update(gameTime, _font);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _view.Draw(_spriteBatch, _font);
        }
    }
}
