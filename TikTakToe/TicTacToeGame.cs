using System;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Documents;

namespace TikTakToe
{
    public class TicTacToeGame : ViewModel
    {
        bool firstPlayer = true;
        public TicTacToeField[,] GameField { get; set; }

        public TicTacToePlayer PlayerOne { get; set; }
        public TicTacToePlayer PlayerTwo { get; set; }

        public System.Collections.Generic.List<TicTacToePlayer> AllPlayers = new System.Collections.Generic.List<TicTacToePlayer>();

        public TicTacToePlayer CurrentPlayer
        {
            get
            {
                var player = firstPlayer ? PlayerOne : PlayerTwo;
                return player;
            }
        }

        public void ResetGame()
        {
            firstPlayer = true;
            GenGame();

        }

        public void NextRound()
        {
            firstPlayer = !firstPlayer;
            OnPropertyChanged("CurrentPlayer");
        }


        public string CheckGewinner()
        {
            foreach (var item in AllPlayers)
            {
                if (item.CheckIsWinner())
                    return item.PlayerName;
            }
            return null;
        }

        public TicTacToeGame()
        {
            GenGame();
        }

        private void GenGame()
        {
            
            GameField = new TicTacToeField[3, 3];

            for (int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    GameField[x, y] = new TicTacToeField();
                }
            }

            if(PlayerOne==null)
                PlayerOne = new TikTakToe.TicTacToePlayer(this, "Player 1 - x", "x");

            if(PlayerTwo==null)
                PlayerTwo = new TikTakToe.TicTacToePlayer(this, "Player 2 - o", "o");

            PlayerOne.NewTry();
            PlayerTwo.NewTry();
            AllPlayers.Clear();
            AllPlayers.Add(PlayerOne);
            AllPlayers.Add(PlayerTwo);
        }
    }



    public class TicTacToeField : ViewModel
    {
        public TicTacToeField()
        {

        }

        private TicTacToePlayer _FieldSetWithUser;
        public TicTacToePlayer FieldSetWithUser
        {
            get
            {
                return _FieldSetWithUser;
            }

            set
            {
                _FieldSetWithUser = value;
                OnPropertyChanged(nameof(FieldSetWithUser));
            }
        }

    }

    public class TicTacToePlayer : ViewModel
    {
        private string _playerName;
        private TicTacToeGame ticTacToeGame;

        public int AnzahlGewonnen
        {
            get { return anzahlGewonnen; }
            set { anzahlGewonnen = value; OnPropertyChanged(); }
        }

        public void NewTry()
        {
            this.IsGewinner = false;
        }

        public TicTacToePlayer(TicTacToeGame ticTacToeGame, string playername, string playerSymbol)
        {
            this.ticTacToeGame = ticTacToeGame;
            this.PlayerName = playername;
            this.PlayerSymbol = playerSymbol;

            _fieldWinningSetting = new System.Collections.Generic.List<System.Collections.Generic.List<TicTacToeField>>()
            {
                new System.Collections.Generic.List<TicTacToeField>()
                {
                    //xxx
                    //###
                    //###
                    ticTacToeGame.GameField[0, 0],
                    ticTacToeGame.GameField[0, 1],
                    ticTacToeGame.GameField[0, 2],
                },
                new System.Collections.Generic.List<TicTacToeField>()
                {
                    //x##
                    //x##
                    //x##
                    ticTacToeGame.GameField[0, 0],
                    ticTacToeGame.GameField[1, 0],
                    ticTacToeGame.GameField[2, 0],
                },
                new System.Collections.Generic.List<TicTacToeField>()
                {
                    //###
                    //xxx
                    //###
                    ticTacToeGame.GameField[1, 0],
                    ticTacToeGame.GameField[1, 1],
                    ticTacToeGame.GameField[1, 2],
                },
                new System.Collections.Generic.List<TicTacToeField>()
                {
                    //###
                    //###
                    //xxx
                    ticTacToeGame.GameField[2, 0],
                    ticTacToeGame.GameField[2, 1],
                    ticTacToeGame.GameField[2, 2],
                },
                new System.Collections.Generic.List<TicTacToeField>()
                {
                    //##x
                    //##x
                    //##x
                    ticTacToeGame.GameField[0, 2],
                    ticTacToeGame.GameField[1, 2],
                    ticTacToeGame.GameField[2, 2],
                },
                new System.Collections.Generic.List<TicTacToeField>()
                {
                    //x##
                    //#x#
                    //##x
                    ticTacToeGame.GameField[0, 0],
                    ticTacToeGame.GameField[1, 1],
                    ticTacToeGame.GameField[2, 2],
                },
                new System.Collections.Generic.List<TicTacToeField>()
                {
                    //##x
                    //#x#
                    //x##
                    ticTacToeGame.GameField[0, 2],
                    ticTacToeGame.GameField[1, 1],
                    ticTacToeGame.GameField[0, 2],
                }
            };
        }



        public string PlayerName
        {
            get
            {
                return _playerName;
            }

            set
            {
                _playerName = value;
                OnPropertyChanged(nameof(PlayerName));
            }
        }

        public string PlayerSymbol
        {
            get { return playerSymbol; }
            set { playerSymbol = value; OnPropertyChanged(); }
        }

        System.Collections.Generic.List<System.Collections.Generic.List<TicTacToeField>> _fieldWinningSetting;
        private string playerSymbol;
        private int anzahlGewonnen;
        internal bool IsGewinner = false;

        internal bool CheckIsWinner()
        {

            foreach (var ruleSetting in _fieldWinningSetting)
            {
                bool ok = true;
                foreach (var item in ruleSetting)
                {
                    ok = ok && (item.FieldSetWithUser == this);
                }
                if (ok)
                {
                    if (!IsGewinner)
                    {
                        this.IsGewinner = true;
                        AnzahlGewonnen++;
                    }

                    return ok;
                }
            }
            return false;
        }
    }
}