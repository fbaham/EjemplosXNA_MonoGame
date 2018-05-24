using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Plataformas2D.GameContent
{
    class Level : IDisposable
    {
        #region Constructor

        // Estructura física del nivel
        private Tile[,] tiles;
        private Texture2D[] layers;

        // Capa sobre la que se dibujan las entidades 
        private const int EntityLayer = 2;

        // Entidades de juego
        Player player;
        //private List<Gem> gems = new List<Gem>();
        //private List<Enemy> enemies = new List<Enemy>();

        // Ubicaciones clave del nivel
        private Vector2 start;
        private Point exit = InvalidPosition;
        private static readonly Point InvalidPosition = new Point(-1, -1);
        
        // Nivel de estado de juego
        private Random random = new Random(354668);
        int score;
        bool reachedExit;
        TimeSpan timeRemaining;
        private const int PointsPerSecond = 5;

        // Contenido del nivel
        ContentManager content;
        //private SoundEffect exitReachedSound;

        public Level(IServiceProvider serviceProvider, Stream fileStream, int levelIndex)
        {
            content = new ContentManager(serviceProvider, "Content");
            timeRemaining = TimeSpan.FromMinutes(2.0);
            LoadTiles(fileStream);
            //loadPlayer();

            layers = new Texture2D[3];
            for (int i = 0; i < layers.Length; ++i)
            {
                // Choose a random segment if each background layer for level variety.
                int segmentIndex = levelIndex;
                layers[i] = Content.Load<Texture2D>("Backgrounds/Layer" + i + "_" + segmentIndex);
            }

            // Load sounds.
            //exitReachedSound = Content.Load<SoundEffect>("Sounds/ExitReached");

        }

        #endregion

        #region Propiedades
        
        public ContentManager Content { get => content; }
        internal Player Player { get => player; }
        public int Width { get => 16; }//tiles.GetLenght(0); }
        public int Height { get => 16; }//tiles.GetLenght(0); }
        public int Score { get => score;}
        public bool ReachedExit { get => reachedExit;}
        public TimeSpan TimeRemaining { get => timeRemaining;}

        #endregion

        #region Cargas

        private void LoadTiles(Stream fileStream)
        {
            // Load the level and ensure all of the lines are the same length.
            int width;
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line = reader.ReadLine();
                width = line.Length;
                while (line != null)
                {
                    lines.Add(line);
                    if (line.Length != width)
                        throw new Exception(String.Format("The length of line {0} is different from all preceeding lines.", lines.Count));
                    line = reader.ReadLine();
                }
            }

            // Allocate the tile grid.
            tiles = new Tile[width, lines.Count];

            // Loop over every tile position,
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    // to load each tile.
                    char tileType = lines[y][x];
                    tiles[x, y] = LoadTile(tileType, x, y);
                }
            }

            // Verify that the level has a beginning and an end.
            if (Player == null)
                throw new NotSupportedException("A level must have a starting point.");
            if (exit == InvalidPosition)
                throw new NotSupportedException("A level must have an exit.");

        }

        private Tile LoadTile(char tileType, int x, int y)
        {
            switch (tileType)
            {
                // Blank space
                case '.':
                    return new Tile(null, TileCollision.Passable);

                // Exit
                case 'X':
                    return LoadExitTile(x, y);

                //// Gem
                //case 'G':
                //    return LoadGemTile(x, y);

                // Floating platform
                case '-':
                    return LoadTile("Platform", TileCollision.Platform);

                // Various enemies
                //case 'A':
                //    return LoadEnemyTile(x, y, "MonsterA");
                //case 'B':
                //    return LoadEnemyTile(x, y, "MonsterB");
                //case 'C':
                //    return LoadEnemyTile(x, y, "MonsterC");
                //case 'D':
                //    return LoadEnemyTile(x, y, "MonsterD");

                // Platform block
                case '~':
                    return LoadVarietyTile("BlockB", 2, TileCollision.Platform);

                // Passable block
                case ':':
                    return LoadVarietyTile("BlockB", 2, TileCollision.Passable);

                // Player 1 start point
                case '1':
                    return LoadStartTile(x, y);

                // Impassable block
                case '#':
                    return LoadVarietyTile("BlockA", 7, TileCollision.Impassable);

                // Unknown tile type character
                default:
                    throw new NotSupportedException(String.Format("Unsupported tile type character '{0}' at position {1}, {2}.", tileType, x, y));
            }
        }

        private Tile LoadTile(string name, TileCollision collision)
        {
            return new Tile(Content.Load<Texture2D>("Tiles/dungeon" + name), collision);
        }

        private Tile LoadVarietyTile(string baseName, int variationCount, TileCollision collision)
        {
            int index = random.Next(variationCount);
            return LoadTile(baseName + index, collision);
        }

        private Tile LoadStartTile(int x, int y)
        {
            if (Player != null)
                throw new NotSupportedException("A level may only have one starting point.");

            start = RectangleExtensions.GetBottomCenter(GetBounds(x, y));
            player = new Player(this, start);

            return new Tile(null, TileCollision.Passable);
        }

        private Tile LoadExitTile(int x, int y)
        {
            if (exit != InvalidPosition)
                throw new NotSupportedException("A level may only have one exit.");

            exit = GetBounds(x, y).Center;

            return LoadTile("Exit", TileCollision.Passable);
        }

        //private Tile LoadEnemyTile(int x, int y, string spriteSet)
        //{
        //    Vector2 position = RectangleExtensions.GetBottomCenter(GetBounds(x, y));
        //    enemies.Add(new Enemy(this, position, spriteSet));

        //    return new Tile(null, TileCollision.Passable);
        //}

        //private Tile LoadGemTile(int x, int y)
        //{
        //    Point position = GetBounds(x, y).Center;
        //    gems.Add(new Gem(this, new Vector2(position.X, position.Y)));

        //    return new Tile(null, TileCollision.Passable);
        //}

        //private void loadPlayer()
        //{
        //    start = new Vector2(80, 80);
        //    player = new Player(this, start);
        //}

        public void Dispose()
        {
            Content.Unload();
        }

        #endregion

        #region Limites y colisiones

        public TileCollision GetCollision(int x, int y)
        {
            // Prevent escaping past the level ends.
            if (x < 0 || x >= Width)
                return TileCollision.Impassable;
            // Allow jumping past the level top and falling through the bottom.
            if (y < 0 || y >= Height)
                return TileCollision.Passable;

            return tiles[x, y].Collision;
        }
       
        public Rectangle GetBounds(int x, int y)
        {
            return new Rectangle(x * Tile.Width, y * Tile.Height, Tile.Width, Tile.Height);
        }

        #endregion

        #region Update
        public void Update(GameTime gameTime, KeyboardState keyboardState, GamePadState gamePadState, AccelerometerState accelState, DisplayOrientation orientation)
        {
            // Pause while the player is dead or time is expired.
            if (!Player.IsAlive || TimeRemaining == TimeSpan.Zero)
            {
                // Still want to perform physics on the player.
                Player.ApplyPhysics(gameTime);
            }
            else if (ReachedExit)
            {
                // Animate the time being converted into points.
                int seconds = (int)Math.Round(gameTime.ElapsedGameTime.TotalSeconds * 100.0f);
                seconds = Math.Min(seconds, (int)Math.Ceiling(TimeRemaining.TotalSeconds));
                timeRemaining -= TimeSpan.FromSeconds(seconds);
                score += seconds * PointsPerSecond;
            }
            else
            {
                timeRemaining -= gameTime.ElapsedGameTime;
                Player.Update(gameTime, keyboardState, gamePadState, accelState, orientation);
                //UpdateGems(gameTime);

                // Falling off the bottom of the level kills the player.
                //if (Player.BoundingRectangle.Top >= Height * Tile.Height)
                //    OnPlayerKilled(null);

                //UpdateEnemies(gameTime);

                // The player has reached the exit if they are standing on the ground and
                // his bounding rectangle contains the center of the exit tile. They can only
                // exit when they have collected all of the gems.
                if (Player.IsAlive &&
                    Player.IsOnGround &&
                    Player.BoundingRectangle.Contains(exit))
                {
                    //OnExitReached();
                }
            }

            // Clamp the time remaining at zero.
            if (timeRemaining < TimeSpan.Zero)
                timeRemaining = TimeSpan.Zero;
        }

        //private void UpdateGems(GameTime gameTime)
        //{
        //    for (int i = 0; i < gems.Count; ++i)
        //    {
        //        Gem gem = gems[i];

        //        gem.Update(gameTime);

        //        if (gem.BoundingCircle.Intersects(Player.BoundingRectangle))
        //        {
        //            gems.RemoveAt(i--);
        //            OnGemCollected(gem, Player);
        //        }
        //    }
        //}

        //private void UpdateEnemies(GameTime gameTime)
        //{
        //    foreach (Enemy enemy in enemies)
        //    {
        //        enemy.Update(gameTime);

        //        // Touching an enemy instantly kills the player
        //        if (enemy.BoundingRectangle.Intersects(Player.BoundingRectangle))
        //        {
        //            OnPlayerKilled(enemy);
        //        }
        //    }
        //}

        //private void OnGemCollected(Gem gem, Player collectedBy)
        //{
        //    score += Gem.PointValue;

        //    gem.OnCollected(collectedBy);
        //}

        //private void OnPlayerKilled(Enemy killedBy)
        //{
        //    Player.OnKilled(killedBy);
        //}

        //private void OnExitReached()
        //{
        //    Player.OnReachedExit();
        //    exitReachedSound.Play();
        //    reachedExit = true;
        //}

        /// <summary>
        /// Restores the player to the starting point to try the level again.
        /// </summary>
        public void StartNewLife()
        {
            Player.Reset(start);
        }

        #endregion

        #region Draw
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i <= EntityLayer; ++i)
                spriteBatch.Draw(layers[i], Vector2.Zero, Color.White);

            DrawTiles(spriteBatch);

            //foreach (Gem gem in gems)
            //    gem.Draw(gameTime, spriteBatch);

            Player.Draw(gameTime, spriteBatch);

            //foreach (Enemy enemy in enemies)
            //    enemy.Draw(gameTime, spriteBatch);

            for (int i = EntityLayer + 1; i < layers.Length; ++i)
                spriteBatch.Draw(layers[i], Vector2.Zero, Color.White);
        }

        private void DrawTiles(SpriteBatch spriteBatch)
        {
            // For each tile position
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    // If there is a visible tile in that position
                    Texture2D texture = tiles[x, y].Texture;
                    if (texture != null)
                    {
                        // Draw it in screen space.
                        Vector2 position = new Vector2(x, y) * Tile.Size;
                        spriteBatch.Draw(texture, position, Color.White);
                    }
                }
            }
        }

        #endregion
    }
}
