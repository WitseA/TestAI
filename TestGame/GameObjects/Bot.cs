using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.AI;

namespace TestGame.GameObjects
{
    public class Bot : Sprite
    {
        private Vector2 _checkPosFollow;
        private Rectangle Rectangle;

        public MovementAI ai { get; set; }

        public Bot(Texture2D tex, Vector2 pos, List<Vector2> crd, Player target, MovementAI _ai = null) : base(tex, pos)
        {
            Speed = 150;
            _checkPosFollow.X = 200;
            _checkPosFollow.Y = 200;
            ai = _ai;
            if(ai != null) { 
                foreach (var item in crd)
                {
                    ai.AddWaypoint(item);
                }
            }
            ai.Target = target;
            Rectangle = new()
            {
                X = Convert.ToInt32(Position.X),
                Y = Convert.ToInt32(Position.Y),
                Width = texture.Width,
                Height = texture.Height
            };
        }

        public void Update(Player player)
        {
            if (ai != null && ai.CurrentPattern != null)
            {
                switch (ai.CurrentPattern)
                {
                    case "Patrol":
                        ai.Patrol(this);
                        break;
                    case "Guard":
                        ai.Guard(this);
                        break;
                    case "Follow":
                        ai.Follow(this);
                        break;
                    case "StayAway":
                        ai.StayAway(this);
                        break;
                }
            }
        }
    }
}
