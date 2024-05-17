using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameProject;

public class GameEngine : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D _texture;
    Texture2D texture;
    Texture2D door;
    Texture2D intersect;
    Texture2D house;
    Vector2 position;
    float speed = 2f;
    Song song;
    bool showHouse = false;
    public GameEngine()
    {
        _graphics = new GraphicsDeviceManager(this);
        position = new Vector2(Window.ClientBounds.Width / 16, (float)3.14 * Window.ClientBounds.Height / 5);
        Content.RootDirectory = "C:\\Users\\Yanturin Aygiz\\Downloads\\game-project-Yanay215-main\\game-project-Yanay215-main\\GameProject\\Content\\bin\\DesktopGL";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _texture = Content.Load<Texture2D>("background111");
        texture = Content.Load<Texture2D>("char1");
        door = Content.Load<Texture2D>("door1");
        intersect = Content.Load<Texture2D>("intersect111");
        house = Content.Load<Texture2D>("house11");
        song = Content.Load<Song>("Garmony");
        MediaPlayer.Play(song);
        MediaPlayer.IsRepeating = true;
        MediaPlayer.MediaStateChanged += ChangeVolume;
        // TODO: use this.Content to load your game content here
    }
    void ChangeVolume(object sender,System.EventArgs e)
    {
        MediaPlayer.Volume -= 0.1f;
    }
    protected override void UnloadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
        if(IsActive)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Collide()&&keyboardState.IsKeyDown(Keys.E))
            {
                showHouse = true;
            }
            if (showHouse)
            {
                if (keyboardState.IsKeyDown(Keys.S)) position.Y += speed;
                if (keyboardState.IsKeyDown(Keys.W)) position.Y -= speed;
            }
            // TODO: Add your update logic here
            if (keyboardState.IsKeyDown(Keys.D)) position.X += speed;
            if (keyboardState.IsKeyDown(Keys.A)) position.X -= speed;
            if (position.X > Window.ClientBounds.Width - texture.Width) position.X = Window.ClientBounds.Width - texture.Width;
            if (position.X < 0) position.X = 0;
            base.Update(gameTime);
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        _spriteBatch.Draw(door, new Vector2(Window.ClientBounds.Width / (float)2.2, (float)3.14 * Window.ClientBounds.Height / 5), Color.White);
        _spriteBatch.Draw(_texture, Vector2.Zero,Color.White);
        _spriteBatch.Draw(texture,position,
                null,
                Color.White,
                0,
                Vector2.Zero,
                1.1f,
                SpriteEffects.None,
                0);
        if (Collide()) _spriteBatch.Draw(intersect,Vector2.Zero,Color.White);
        if (showHouse)
        {
            _spriteBatch.Draw(house, Vector2.Zero, Color.White);
            _spriteBatch.Draw(texture, new Vector2(position.X - 30f, position.Y + 80f),
                null,
                Color.White,
                0,
                Vector2.Zero,
                1.1f,
                SpriteEffects.None,
                0) ;
        }
        _spriteBatch.End();
            // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
    protected bool Collide()
    {
        Rectangle charRect = new Rectangle((int)position.X,(int)position.Y,texture.Width,texture.Height);
        Rectangle doorRect = new Rectangle((int)(Window.ClientBounds.Width / (float)2.2), (int)((float)3.14 * Window.ClientBounds.Height / 5), door.Width, door.Height);
        return charRect.Intersects(doorRect);
    }
}