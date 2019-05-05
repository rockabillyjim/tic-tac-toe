using System.Collections.Generic;

namespace TicTacToe.API.Domain.Models
{
    public class Square
    {
        public int Id { get; set; }
        /// <summary>
        /// The Row on which the Square appears.
        /// </summary>
        public Row Row { get; protected internal set; }
        /// <summary>
        /// The Column on which the Square appears.
        /// </summary>
        public Column Column { get; protected internal set; }
        /// <summary>
        /// The Player that played the Square in this game.
        /// </summary>
        public Player? Value { get; protected internal set; }
        /// <summary>
        /// The turn on which the Square was played in this game.
        /// </summary>
        public int? PlayedOnTurn { get; protected internal set; }
        /// <summary>
        /// Indicates whether the Square is in the top-left-to-bottom-right diagonal.
        /// </summary>
        protected internal bool Diagonal1
        {
            get
            {
                if ((Row == Row.Top && Column == Column.Left) || (Row == Row.Middle && Column == Column.Middle) || (Row == Row.Bottom && Column == Column.Right))
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// Indicates whether the Square is in the top-right-to-bottom-left diagonal.
        /// </summary>
        protected internal bool Diagonal2
        {
            get
            {
                if ((Row == Row.Top && Column == Column.Right) || (Row == Row.Middle && Column == Column.Middle) || (Row == Row.Bottom && Column == Column.Left))
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// The co-ordinates of the Square in the Board.
        /// </summary>
        public string Location
        {
            get => $"[{Row},{Column}]";
        }

        /// <summary>
        /// Represents a Square on the board. Can only be created within the project.
        /// </summary>
        protected internal Square()
        {
        }
    }
}