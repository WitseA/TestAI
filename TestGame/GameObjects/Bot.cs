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
        private float _lastDistance;
        private float _timer = 0f;
        private float _distance;
        private bool _distanceCheck;
        private bool _setTimer = false;
        private bool _alerted = false;

        public Color Color = Color.Red;
        public MovementAI ai { get; set; }
        public Bot(Texture2D tex, Vector2 pos, List<Vector2> crd, Player target, MovementAI _ai = null) : base(tex, pos)
        {
            Speed = 150;
            _checkPosFollow.X = 200;
            _checkPosFollow.Y = 200;
            ai = _ai;
            if (ai != null) {
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
        public void Update()
        {
            UpdateAI();
        }
        private void UpdateAI()
        {
            _distance = Vector2.Distance(this.ai.Target.Position, this.Position);
            checkAI();
            _distanceCheck = _distance > 100f;
            if (_distanceCheck && !_alerted)
            {
                ai.ChangeMovementPattern("Patrol");
                Color = Color.Red;
            }
            else
            {
                _alerted = true;
                ai.ChangeMovementPattern("StayAway");
                Color = Color.Green;
            }

            if (_distance <= _lastDistance && ai.CurrentPattern == "stayaway")
            {
                ai.ChangeMovementPattern("follow");
            }
            _lastDistance = _distance;
            if (_alerted && !_setTimer)
            {
                _timer = Globals.TotalSeconds;
                _setTimer = true;
            }
            if (Globals.TotalSeconds-_timer <= 300f && _distance > 400f)
            {
                _alerted = false;
            }
        }
        public void checkAI()
        {
            if (ai != null && ai.CurrentPattern != null)
            {
                switch (ai.CurrentPattern)
                {
                    case "patrol":
                        ai.Patrol(this);
                        break;
                    case "guard":
                        ai.Guard(this);
                        break;
                    case "follow":
                        ai.Follow(this);
                        break;
                    case "stayaway":
                        ai.StayAway(this);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
