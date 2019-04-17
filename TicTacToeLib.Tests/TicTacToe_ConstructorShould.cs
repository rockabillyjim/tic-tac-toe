using System.Linq;
using NUnit.Framework;
using TicTacToeLib;

namespace TicTacToeLib.Tests
{
    [TestFixture]
    public class TicTacToe_ConstructorShould
    {
        private readonly TicTacToe _ticTacToe;
        
        public TicTacToe_ConstructorShould()
        {
            _ticTacToe = new TicTacToe();
        }
        
        [Test]
        public void InitialiseNextPlayerToX()
        {
            var result = _ticTacToe.NextPlayer;
            Assert.AreEqual(result, Player.X);
        }

        [Test]
        public void SetStatusToInProgress()
        {
            var result = _ticTacToe.Status;
            Assert.AreEqual(result, GameStatus.InProgress);
        }

        [Test]
        public void SetTurnTo0()
        {
            var result = _ticTacToe.Turn;
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void InitialiseBoardWith9Squares()
        {
            var result = _ticTacToe.Board.Count();
            Assert.AreEqual(result, 9);
        }

        [Test]
        public void InitialiseBoardWith9DifferentSquares()
        {
            var result = _ticTacToe.Board.GroupBy(g => new { g.Row, g.Column }).Where(w => w.Count() > 1).Count();
            Assert.AreEqual(result, 0);
        }
    }
}