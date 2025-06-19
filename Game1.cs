using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mono_Game___Final_Project
{
    enum Screen
    {
        Intro,
        maskedKillerlogo
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window;

        Texture2D maskedKillerlogo;

        Texture2D campGroundsTexture;
        Rectangle campGroundsRect;


        Texture2D survivorTexture;
        Texture2D survivor1Texture;
        Texture2D survivor2Texture;
        Texture2D survivor3Texture;

        Texture2D killerTexture;
        Texture2D killer1Texture;
        Texture2D killer2Texture;
        Texture2D killer3Texture;

        Texture2D playerTexture;

        Texture2D botTexture;

        Rectangle survivorRect;

        Rectangle killerRect;

        int numOfGasCanisters = 0;

        SpriteFont textFont;

        Vector2 survivorSpeed;
        Vector2 killerSpeed;

        Texture2D generatorTexture;
        Rectangle generatorRect;

        Screen screen;


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

        int playerX = 30;
        int playerY = 30;

        int killerX = 667;
        int killerY = 156;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            screen = Screen.Intro;

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



            survivorRect = new Rectangle(playerX, playerY, 50, 50);
            survivorSpeed = new Vector2();
            killerRect = new Rectangle(killerX, killerY, 50, 50);
            killerSpeed = new Vector2();
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
            maskedKillerlogo = Content.Load<Texture2D>("maskedKiller2logo");
            campGroundsTexture = Content.Load<Texture2D>("campGrounds");
            survivorTexture = Content.Load<Texture2D>("survivor");
            survivor1Texture = Content.Load<Texture2D>("survivor1");
            survivor2Texture = Content.Load<Texture2D>("survivor2");
            survivor3Texture = Content.Load<Texture2D>("survivor3");
            playerTexture = survivorTexture;

            killerTexture = Content.Load<Texture2D>("killer");
            killer1Texture = Content.Load<Texture2D>("killer1");
            killer2Texture = Content.Load<Texture2D>("killer2");
            killer3Texture = Content.Load<Texture2D>("killer3");
            generatorTexture = Content.Load<Texture2D>("generator");
            gasCanisterTexture = Content.Load<Texture2D>("gasCanister");
            botTexture = killerTexture;
            textFont = Content.Load<SpriteFont>("ScoreFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            this.Window.Title = "x = " + mouseState.X + " y = " + mouseState.Y;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.maskedKillerlogo;
            }
            else if (screen == Screen.maskedKillerlogo)
            {
                killerRect.X += (int)killerSpeed.X;
                killerRect.Y += (int)killerSpeed.Y;
                survivorSpeed = Vector2.Zero;
                killerSpeed = Vector2.Zero;
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    survivorSpeed.Y -= 1;
                    //killerSpeed.Y -= 2;
                    playerTexture = survivorTexture;
                    botTexture = killerTexture;
                }
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    survivorSpeed.Y += 1;
                    //killerSpeed.Y += 2;
                    playerTexture = survivor1Texture;
                    botTexture = killer3Texture;
                }

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    survivorSpeed.X -= 1;
                    //killerSpeed.X -= 2;
                    playerTexture = survivor2Texture;
                    botTexture = killer2Texture;
                }
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    survivorSpeed.X += 1;
                    //killerSpeed.X += 2;
                    playerTexture = survivor3Texture;
                    botTexture = killer1Texture;
                }
                if(survivorRect.X < killerRect.X)
                {
                    killerSpeed.X -= 2;
                }
                if (survivorRect.X > killerRect.X)
                { 
                    killerSpeed.X += 2;
                }

                if(survivorRect.Y < killerRect.Y)
                {
                    killerSpeed.Y -= 2;
                }

                if (survivorRect.Y > killerRect.Y)
                {
                    killerSpeed.Y += 2;
                }
                killerRect.X += (int)killerSpeed.X;
                survivorRect.X += (int)survivorSpeed.X;
                if (survivorRect.Right >= window.Width || survivorRect.Left <= 0)
                {
                    survivorRect.X -= (int)survivorSpeed.X;
                    killerRect.X -= (int)killerSpeed.X;
                }
                killerRect.Y += (int)killerSpeed.Y;
                survivorRect.Y += (int)survivorSpeed.Y;
                if (survivorRect.Bottom >= window.Height || survivorRect.Top <= 0)
                {
                    survivorRect.Y -= (int)survivorSpeed.Y;
                    killerRect.Y -= (int)killerSpeed.Y;
                }

                for (int i = 0; i < gasCanisters.Count; i++)
                {
                    if (survivorRect.Intersects(gasCanisters[i]))
                    {
                        gasCanisters.RemoveAt(i);
                        i--;
                        numOfGasCanisters++;
                    }
                }

                if (survivorRect.Intersects(generatorRect))
                {
                    survivorRect.Offset(-survivorSpeed);
                }

                if (killerRect.Intersects(generatorRect))
                {
                    killerRect.Offset(-killerSpeed);
                }

                if (survivorRect.Intersects(barrierRect))
                {
                    survivorRect.Offset(-survivorSpeed);
                }

                if (killerRect.Intersects(barrierRect))
                {
                    killerRect.Offset(-killerSpeed);
                }

                if (survivorRect.Intersects(barrier1Rect))

                {
                    survivorRect.Offset(-survivorSpeed);
                }
                if (killerRect.Intersects(barrier1Rect))

                {
                    killerRect.Offset(-killerSpeed);
                }

                if (survivorRect.Intersects(barrier2Rect))
                {
                    survivorRect.Offset(-survivorSpeed);
                }
                if (killerRect.Intersects(barrier2Rect))
                {
                    killerRect.Offset(-killerSpeed);
                }

                if (survivorRect.Intersects(barrier3Rect))
                {
                    survivorRect.Offset(-survivorSpeed);
                }

                if (killerRect.Intersects(barrier3Rect))
                {
                    killerRect.Offset(-killerSpeed);
                }

                if (survivorRect.Intersects(barrier4Rect))
                {
                    survivorRect.Offset(-survivorSpeed);
                }

                if (killerRect.Intersects(barrier4Rect))
                {
                    killerRect.Offset(-killerSpeed);
                }

                if (survivorRect.Intersects(barrier5Rect))
                {
                    survivorRect.Offset(-survivorSpeed);
                }

                if (killerRect.Intersects(barrier5Rect))
                {
                    killerRect.Offset(-killerSpeed);
                }


            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(maskedKillerlogo, new Vector2(0, 0), Color.White);
            }
            else if (screen == Screen.maskedKillerlogo)
            {
                _spriteBatch.Draw(campGroundsTexture, campGroundsRect, Color.White);

                _spriteBatch.Draw(playerTexture, survivorRect, Color.White);

                _spriteBatch.Draw(botTexture, killerRect, Color.White);

                _spriteBatch.Draw(generatorTexture, generatorRect, Color.White);

                foreach (Rectangle gascanister in gasCanisters)
                    _spriteBatch.Draw(gasCanisterTexture, gascanister, Color.White);

                _spriteBatch.DrawString(textFont, (numOfGasCanisters).ToString("0"), new Vector2(763, 30), Color.Red);

                _spriteBatch.DrawString(textFont, "/3", new Vector2(780, 30), Color.Red);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
