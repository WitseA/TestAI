using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TestGame.AI;
using TestGame.GameObjects;
using TestGame.Managers;

namespace TestGame.Managers
{
    public class GameManager
    {
        private readonly Player _player;
        private readonly Bot _bot;
        public GameManager()
        {
            MovementAI ai = new();
            _player = new Player(Globals.Content.Load<Texture2D>("square"), new Vector2(600, 600));
            var botTexture = Globals.Content.Load<Texture2D>("square");

            ai.GuardCrd = new Vector2(300,300); // Set GuardCrd
            ai.GuardDistance = 200f;
            ai.SWDistance = 150f;
            
            
            List<Vector2> aiList = new List<Vector2>
            {
                new Vector2(100, 100),
                new Vector2(350, 200),
                new Vector2(300, 300),
                new Vector2(100, 30),
                new Vector2(100, 100)
            };

            _bot = new Bot(botTexture, new Vector2(50, 50), aiList, _player, ai);
        }
        public void Update()
        {
            InputManager.Update();
            _player.Update();
            _bot.Update(_player);
            HandleInput(); 
        }
        public void Draw()
        {
            _player.Draw(Color.Blue, 0.05f);
            _bot.Draw(Color.Red, 0.025f);
        }
        private void HandleInput()
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.D1))
            {
                _bot.ai.ChangeMovementPattern("Patrol");
            }
            else if (keyboardState.IsKeyDown(Keys.D2))
            {
                _bot.ai.ChangeMovementPattern("Guard");
            }
            else if (keyboardState.IsKeyDown(Keys.D3))
            {
                _bot.ai.ChangeMovementPattern("Follow");
            }
            else if (keyboardState.IsKeyDown(Keys.D4))
            {
                _bot.ai.ChangeMovementPattern("StayAway");
            }
        }
    }
}
