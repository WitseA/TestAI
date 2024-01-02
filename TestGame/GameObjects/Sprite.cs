using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame.GameObjects
{
    public class Sprite
    {
        protected readonly Texture2D texture;
        protected readonly Vector2 origin;
        public Vector2 Position { get; set; }
        public int Speed { get; set; }

        public Sprite(Texture2D tex, Vector2 pos)
        {
            texture = tex;
            Position = pos;
            Speed = 300;
            origin = new(tex.Width / 2, tex.Height / 2);
        }

        public virtual void Draw(Color color, float scale)
        {
            Globals.SpriteBatch.Draw(texture, Position, null, color, 0, origin, scale, SpriteEffects.None, 1);
        }
    }
}
