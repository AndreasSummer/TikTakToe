using System;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void User1TopRow()
        {
            var game = new TikTakToe.TicTacToeGame();
            game.PlayerOne.PlayerName = "Player 1";
            game.PlayerOne.PlayerName = "Player 2";
            game.GameField[0, 0].FieldSetWithUser = game.PlayerOne;
            game.GameField[0, 1].FieldSetWithUser = game.PlayerOne;
            game.GameField[0, 2].FieldSetWithUser = game.PlayerOne;


            Assert.Equal(game.PlayerOne.PlayerName, game.CheckGewinner());
        }

        [Fact]
        public void User2TopRow()
        {
            var game = new TikTakToe.TicTacToeGame();
            game.PlayerOne.PlayerName = "Player 1";
            game.PlayerOne.PlayerName = "Player 2";
            game.GameField[0, 0].FieldSetWithUser = game.PlayerTwo;
            game.GameField[0, 1].FieldSetWithUser = game.PlayerTwo;
            game.GameField[0, 2].FieldSetWithUser = game.PlayerTwo;


            Assert.Equal(game.PlayerTwo.PlayerName, game.CheckGewinner());
        }

        [Fact]
        public void User2TopUpToDownRight()
        {
            var game = new TikTakToe.TicTacToeGame();
            game.PlayerOne.PlayerName = "Player 1";
            game.PlayerOne.PlayerName = "Player 2";
            game.GameField[0, 0].FieldSetWithUser = game.PlayerTwo;
            game.GameField[1, 1].FieldSetWithUser = game.PlayerTwo;
            game.GameField[2, 2].FieldSetWithUser = game.PlayerTwo;


            Assert.Equal(game.PlayerTwo.PlayerName, game.CheckGewinner());
        }

        [Fact]
        public void TestRoundSystem()
        {
            var game = new TikTakToe.TicTacToeGame();
            game.NextRound();
            Assert.Equal(game.PlayerTwo, game.CurrentPlayer);
        }
    }
}
