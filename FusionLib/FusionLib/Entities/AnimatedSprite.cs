using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FusionLib.Entities
{
    public class AnimatedSprite
    {
        private Texture2D spritesheet;
        private int rows, columns, width, height, totalFrames, frame, frameRow, frameCol;
        private Rectangle source, destination;
        int msPerFrame = 100;
        int lastUpdate = 0;

        public AnimatedSprite(Texture2D spritesheet, int totalFrames, int spriteWidth, int spriteHeight)
        {
            this.spritesheet = spritesheet;
            this.totalFrames = totalFrames;
            this.frame = 0;
            this.frameCol = 0;
            this.frameRow = 0;

            this.width = spriteWidth;
            this.height = spriteHeight;

            this.columns = spritesheet.Width / spriteWidth;
            this.rows = spritesheet.Height / spriteHeight;
        }

        public void Update(GameTime gameTime)
        {
            lastUpdate += gameTime.ElapsedGameTime.Milliseconds;

            if (lastUpdate > msPerFrame)
            {
                lastUpdate = 0;
                frame++;
                if (frame == totalFrames)
                    frame = 0;
                frameRow = (int)((float)frame / (float)columns);
                frameCol = frame % columns;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            source = new Rectangle(width * frameCol, height * frameRow, width, height);
            destination = new Rectangle((int)position.X, (int)position.Y, width, height);

            spriteBatch.Draw(spritesheet, destination, source, Color.White);
        }
    }
}