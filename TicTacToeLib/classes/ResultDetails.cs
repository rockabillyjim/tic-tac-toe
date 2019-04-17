using System;

namespace TicTacToeLib
{
    public struct ResultDetails
    {
        public GameStatus Result { get; set; }
        public Square[] MoveLog { get; set; }
    }
}