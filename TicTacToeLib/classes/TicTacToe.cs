using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToeLib
{
    public class TicTacToe
    {
        private Random _rand = new Random();
        
        public GameStatus Status { get; private set; }
        public Player NextPlayer { get; private set; }
        public int Turn { get; private set; }
        public List<Square> Board { get; private set; }

        public List<Square> AvailableSquares
        {
            get => Board.Where(x => x.Value == null).ToList();
        }
        
        public Square[] MoveLog
        {
            get => Board.Where(r => r.Value != null).OrderBy(o => o.PlayedOnTurn).ToArray();
        }
            
        public ResultDetails ResultDetails
        {
            get => new ResultDetails { Result = Status, MoveLog = MoveLog };
        }
        
        public TicTacToe()
        {
            NewGame();
        }
        
        public void Move()
        {
            Move(NextPlayer);
        }
        
        public void Move(Player Player)
        {
            var index = _rand.Next(AvailableSquares.Count);
            var square = AvailableSquares[index];
            Move(Player, square);
        }
        
        public void Move(Player Player, Square Square)
        {
            if (!AvailableSquares.Contains(Square))
                throw new ArgumentOutOfRangeException("Square already played.");
                
            if (NextPlayer != Player)
                throw new Exception($"It is not Player {Player}'s turn.");
            
            Square.PlayedOnTurn = ++Turn;
            Square.Value = Player;
            NextPlayer = Player == Player.X ? Player.O : Player.X;
            
            UpdateStatus();
        }
        
        public void Move(Player Player, int Row, int Column)
        {
            var square = Board.Where(x => x.Row == (Row)Row && x.Column == (Column)Column).SingleOrDefault();
            Move(Player, square);
        }
        
        public void Move(Player Player, Row Row, Column Column)
        {
            var square = Board.Where(x => x.Row == Row && x.Column == Column).SingleOrDefault();
            Move(Player, square);
        }
        
        public void NewGame()
        {
            Status = GameStatus.InProgress;
            NextPlayer = Player.X;
            Turn = 0;
            
            Board = new List<Square>();
            
            for (int row = 1; row < 4; row++)
            {
                for (int column = 1; column < 4; column++)
                {
                    Board.Add(new Square { Row = (Row)row, Column = (Column)column });
                }
            }
        }
        
        public void UpdateStatus()
        {
            var rowWinner = RowWinner();
            var columnWinner = ColumnWinner();
            var diagonalWinner = DiagonalWinner();
            
            if (rowWinner == Player.O || columnWinner == Player.O || diagonalWinner == Player.O)
            {
                Status = GameStatus.WinO;
                return;
            }
            
            if (rowWinner == Player.X || columnWinner == Player.X || diagonalWinner == Player.X)
            {
                Status = GameStatus.WinX;
                return;
            }
            
            if (Turn == 9)
            {
                Status = GameStatus.Draw;
                return;
            }
        }
        
        private Player? RowWinner()
        {
            var result = Board
                .Where(s => s.Value != null)
                .GroupBy(s => new { s.Row, s.Value })
                .Where(grp => grp.Count() == 3)
                .Select(p => new { Player = p.Key.Value })
                .SingleOrDefault();
            return result == null ? null : result.Player;
        }
        
        private Player? ColumnWinner()
        {
            var result = Board
                .Where(s => s.Value != null)
                .GroupBy(s => new { s.Column, s.Value })
                .Where(grp => grp.Count() == 3)
                .Select(p => new { Player = p.Key.Value })
                .SingleOrDefault();
            return result == null ? null : result.Player;
        }
        
        private Player? DiagonalWinner()
        {
            var result1 = Board
                .Where(s => s.Value != null && s.Diagonal1)
                .GroupBy(s => s.Value)
                .Where(grp => grp.Count() == 3)
                .Select(p => new { Player = p.Key.Value })
                .SingleOrDefault();
            
            var result2 = Board
                .Where(s => s.Value != null && s.Diagonal2)
                .GroupBy(s => s.Value)
                .Where(grp => grp.Count() == 3)
                .Select(p => new { Player = p.Key.Value })
                .SingleOrDefault();
                
            if (result1 != null)
                return result1.Player;
            else if (result2 != null)
                return result2.Player;
            else
                return null;
        }
    }
}