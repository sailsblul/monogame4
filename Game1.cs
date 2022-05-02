using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogame4
{
    public class Game1 : Game
    {
        MouseState mouseState;
        Texture2D bombTexture;
        Rectangle bombRect;
        SpriteFont timerFont;
        SoundEffect explode;
        Texture2D explosion;
        SoundEffectInstance explodeInstance;
        float seconds;
        float startTime;
        bool exploded = false;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            Window.Title = "oh no!!! a bomb!!!";
            bombRect = new Rectangle(50, 50, 700, 400);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bombTexture = Content.Load<Texture2D>("bomb");
            timerFont = Content.Load<SpriteFont>("timer");
            explode = Content.Load<SoundEffect>("explosion");
            explodeInstance = explode.CreateInstance();
            explodeInstance.IsLooped = false;
            explosion = Content.Load<Texture2D>("explosionimg");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            // TODO: Add your update logic here
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            if (!exploded)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                if (seconds >= 15)
                {
                    exploded = true;
                    explodeInstance.Play();
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                }
            }
            else
            {
                //the explode sound effect provided has a bit of silence at the end. kinda annoying but I dont feel like editing it
                if (explodeInstance.State == SoundState.Stopped)
                    Exit();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.PapayaWhip);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (exploded)
                _spriteBatch.Draw(explosion, bombRect, Color.White);
            else
            {
                _spriteBatch.Draw(bombTexture, bombRect, Color.White);
                _spriteBatch.DrawString(timerFont, (15 - seconds).ToString("00.0"), new Vector2(300, 230), Color.Black);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
