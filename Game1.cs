using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mono_Game___Final_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window;

        Texture2D campGroundsTexture;
        Rectangle campGroundsRect;

        Texture2D survivorTexture;
        Texture2D survivor1Texture;
        Texture2D survivor2Texture;
        Texture2D survivor3Texture;


        Texture2D playerTexture;

        Rectangle survivorRect;



        Vector2 survivorSpeed;

        Texture2D generatorTexture;
        Rectangle generatorRect;


        Texture2D gasCanisterTexture;
        List<Rectangle> gasCanisters;

        Rectangle barrierRect;
        Rectangle barrier1Rect;
        Rectangle barrier2Rect;
        Rectangle barrier3Rect;
        Rectangle barrier4Rect;
        Rectangle barrier5Rect;

        KeyboardState keyboardState;

        MouseState mouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferHeight = 550;
            _graphics.PreferredBackBufferWidth = 850;
            _graphics.ApplyChanges();

            campGroundsRect = new Rectangle(0, 0, 850, 550);
            barrierRect = new Rectangle(373, 0, 20, 280);
            barrier1Rect = new Rectangle(379, 344, 20, 280);
            barrier2Rect = new Rectangle(405, 0, 200, 250);
            barrier3Rect = new Rectangle(607, 0, 300, 150);
            barrier4Rect = new Rectangle(629, 399, 300, 150);
            barrier5Rect = new Rectangle(528, 500, 100, 20);
            generatorRect = new Rectangle(425, 221, 45, 40);



            survivorRect = new Rectangle(30, 40, 50, 50);
            survivorSpeed = new Vector2();
            window = new Rectangle(0, 0, 850, 550);

            gasCanisters = new List<Rectangle>();

            base.Initialize();


            gasCanisters.Add(new Rectangle(450, 460, 50, 60));
            gasCanisters.Add(new Rectangle(160, 166, 50, 60));
            gasCanisters.Add(new Rectangle(85, 447, 50, 60));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            campGroundsTexture = Content.Load<Texture2D>("campGrounds");
            survivorTexture = Content.Load<Texture2D>("survivor");
            survivor1Texture = Content.Load<Texture2D>("survivor1");
            survivor2Texture = Content.Load<Texture2D>("survivor2");
            survivor3Texture = Content.Load<Texture2D>("survivor3");
            playerTexture = survivorTexture;
            generatorTexture = Content.Load<Texture2D>("generator");
            gasCanisterTexture = Content.Load<Texture2D>("gasCanister");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            this.Window.Title = "x = " + mouseState.X + " y = " + mouseState.Y;
            survivorSpeed = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                survivorSpeed.Y -= 1;
                playerTexture = survivorTexture;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                survivorSpeed.Y += 1;
                playerTexture = survivor1Texture;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                survivorSpeed.X -= 1;
                playerTexture = survivor2Texture;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                survivorSpeed.X += 1;
                playerTexture = survivor3Texture;
            }

            survivorRect.X += (int)survivorSpeed.X;
            if (survivorRect.Right >= window.Width || survivorRect.Left <= 0)
            {
                survivorRect.X -= (int)survivorSpeed.X;
            }

            survivorRect.Y += (int)survivorSpeed.Y;
            if (survivorRect.Bottom >= window.Height || survivorRect.Top <= 0)
            {
                survivorRect.Y -= (int)survivorSpeed.Y;
            }

            for (int i = 0; i < gasCanisters.Count; i++)
            {
                if (survivorRect.Intersects(gasCanisters[i]))
                {
                    gasCanisters.RemoveAt(i);
                    i--;
                }
            }

            if (survivorRect.Intersects(generatorRect))
            {
                survivorRect.Offset(-survivorSpeed);
            }

            if (survivorRect.Intersects(barrierRect))
            {
                survivorRect.Offset(-survivorSpeed);
            }

            if (survivorRect.Intersects(barrier1Rect))

            {
                survivorRect.Offset(-survivorSpeed);
            }
            if(survivorRect.Intersects(barrier2Rect))
            {
                survivorRect.Offset(-survivorSpeed);
            }

            if (survivorRect.Intersects(barrier3Rect))
            {
                survivorRect.Offset(-survivorSpeed);
            }

            if (survivorRect.Intersects(barrier4Rect))
            {
                survivorRect.Offset(-survivorSpeed);
            }

            if (survivorRect.Intersects(barrier5Rect))
            {
                survivorRect.Offset(-survivorSpeed);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(campGroundsTexture, campGroundsRect, Color.White);

            _spriteBatch.Draw(playerTexture, survivorRect, Color.White);

            _spriteBatch.Draw(generatorTexture, generatorRect, Color.White);

            foreach (Rectangle gascanister in gasCanisters)
                _spriteBatch.Draw(gasCanisterTexture, gascanister, Color.White);


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
