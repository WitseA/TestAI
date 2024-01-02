using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Managers;

namespace TestGame.GameObjects
{
    public class Player : Sprite
    {
        public Vector2 Direction { get; private set; }
        public Rectangle Rectangle;
        public Player(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            Rectangle = new()
            {
                X = Convert.ToInt32(Position.X),
                Y = Convert.ToInt32(Position.Y),
                Width = Convert.ToInt32(texture.Width / .05f),
                Height = Convert.ToInt32(texture.Width / .05f)
            };
        }

        public void Update()
        {
            Direction = InputManager.Direction;

            if (Direction != Vector2.Zero)
            {
                Direction = Vector2.Normalize(Direction);
                Position += Direction * Speed * Globals.TotalSeconds;
            }
        }
    }
}
