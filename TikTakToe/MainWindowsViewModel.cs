using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace TikTakToe
{
    public class MainWindowsViewModel : ViewModel
    {
        public string R1C1 { get { return this.Game.GameField[0, 0].FieldSetWithUser?.PlayerSymbol; } }
        public string R1C2 { get { return this.Game.GameField[0, 1].FieldSetWithUser?.PlayerSymbol; } }
        public string R1C3 { get { return this.Game.GameField[0, 2].FieldSetWithUser?.PlayerSymbol; } }

        public string R2C1 { get { return this.Game.GameField[1, 0].FieldSetWithUser?.PlayerSymbol; } }
        public string R2C2 { get { return this.Game.GameField[1, 1].FieldSetWithUser?.PlayerSymbol; } }
        public string R2C3 { get { return this.Game.GameField[1, 2].FieldSetWithUser?.PlayerSymbol; } }

        public string R3C1 { get { return this.Game.GameField[2, 0].FieldSetWithUser?.PlayerSymbol; } }
        public string R3C2 { get { return this.Game.GameField[2, 1].FieldSetWithUser?.PlayerSymbol; } }
        public string R3C3 { get { return this.Game.GameField[2, 2].FieldSetWithUser?.PlayerSymbol; } }

        public void InvalidateContent()
        {
            base.OnPropertyChanged("R1C1");
            base.OnPropertyChanged("R1C2");
            base.OnPropertyChanged("R1C3");

            base.OnPropertyChanged("R2C1");
            base.OnPropertyChanged("R2C2");
            base.OnPropertyChanged("R2C3");

            base.OnPropertyChanged("R3C1");
            base.OnPropertyChanged("R3C2");
            base.OnPropertyChanged("R3C3");
        }


        private TicTacToeGame _game;
        public TicTacToeGame Game
        {
            get
            {
                return _game;
            }

            set
            {
                _game = value;
                OnPropertyChanged(nameof(Game));
            }
        }

        public RelayCommand clickCommand { get; set; }

        public RelayCommand NewGameCommand { get; set; }

        public MainWindowsViewModel()
        {

            clickCommand = new RelayCommand(t => ClickOnButton((Button)t));
            NewGameCommand = new RelayCommand(t => { Game.ResetGame(); InvalidateContent(); });
            Game = new TicTacToeGame();
        }

        private void ClickOnButton(Button t)
        {
            var grid = (Grid)t.Parent;
            var x = Grid.GetRow(t);
            var y = Grid.GetColumn(t);

            TicTacToeField fieldToSet = Game.GameField[x - 1, y];
            if (fieldToSet.FieldSetWithUser != null)
            {
                MessageBox.Show("Bereits belegt von " + fieldToSet.FieldSetWithUser.PlayerName);
                return;
            }

            fieldToSet.FieldSetWithUser = Game.CurrentPlayer;
            if (Game.CurrentPlayer.CheckIsWinner())
            {
                MessageBox.Show("Gewonnen hat " + Game.CurrentPlayer.PlayerName);
            }
            Game.NextRound();
            InvalidateContent();
        }
    }
}
