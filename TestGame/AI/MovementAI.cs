using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.GameObjects;

namespace TestGame.AI
{
    public class MovementAI
    {
        #region Properties
        public Player Target { get; set; }
        public Vector2 GuardCrd { get; set; }
        public float GuardDistance { get; set; }
        public float SWDistance { get; set; }
        public string CurrentPattern { get; private set; }

        private readonly List<Vector2> _path = new();
        private int _current;
        #endregion
        #region Methods
        public void AddWaypoint(Vector2 wp)
        {
            _path.Add(wp);
        }
        public void ChangeMovementPattern(string newPattern)
        {
            CurrentPattern = newPattern.ToLower();
        }

        #region AI_Movements
        public void Patrol(Sprite bot)
        {
            if (_path.Count < 1) return;

            var dir = _path[_current] - bot.Position;

            if (dir.Length() > 4)
            {
                dir.Normalize();
                bot.Position += dir * bot.Speed * Globals.TotalSeconds;
            }
            else
            {
                _current = (_current + 1) % _path.Count;
            }
        }
        public void Guard(Sprite bot)
        {
            if (Target is null) return;

            var toTarget = (GuardCrd - Target.Position).Length();
            Vector2 dir;

            if (toTarget < GuardDistance)
            {
                dir = Target.Position - bot.Position;
            }
            else
            {
                dir = GuardCrd - bot.Position;
            }

            if (dir.Length() > 4f)
            {
                dir.Normalize();
                bot.Position += dir * bot.Speed * Globals.TotalSeconds;
            }
        }
        public void Follow(Sprite bot)
        {
            if (Target is null) return;

            var dir = Target.Position - bot.Position;

            if (dir.Length() > 4)
            {
                dir.Normalize();
                bot.Position += dir * bot.Speed * Globals.TotalSeconds;
            }
        }
        public void StayAway(Sprite bot)
        {
            if (Target is null) return;

            var dir = Target.Position - bot.Position;
            var length = dir.Length();

            if (length > SWDistance + 2)
            {
                dir.Normalize();
                bot.Position += dir * bot.Speed * Globals.TotalSeconds;
            }
            else if (length < SWDistance - 2)
            {
                dir.Normalize();
                bot.Position -= dir * bot.Speed * Globals.TotalSeconds;
            }
        }
        #endregion
        #endregion
    }
}
