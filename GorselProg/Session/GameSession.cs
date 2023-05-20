using GorselProg.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProg.Session
{
    class GameSession
    {
        private static GameSession _instance;
        private Game _currentGame { get; set; }
        public List<Question> _gameQuestions { get; set; }

        private GameSession()
        {
            _currentGame = new Game();
            _gameQuestions = new List<Question>();
        }

        public static GameSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameSession();
                }
                return _instance;
            }
        }

        // DB den çekilen kategorileri set etmek
        public void SetAllQuestions(List<Question> questions)
        {
            _gameQuestions = questions;
        }
        public List<Question> GetAllQuestions()
        {
            return _gameQuestions;
        }
        public void SetCurrentRoom(Game game)
        {
            _currentGame = game;
        }
        public Game GetCurrentRoom()
        {
            return _currentGame;
        }
    }
}

