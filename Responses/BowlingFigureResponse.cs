using System.Collections.Generic;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class BowlingFigureResponse
    {
        public int Id { get; set; }
        public PlayerResponse Player { get; set; }
        public int Balls { get; set; }
        public byte Maidens { get; set; }
        public int Runs { get; set; }
        public int Wickets { get; set; }
        public int Innings { get; set; }

        public BowlingFigureResponse(BowlingFigure bowlingFigure, PlayerResponse player)
        {
            Id = bowlingFigure.Id;
            Player = player;
            Balls = bowlingFigure.Balls;
            Maidens = bowlingFigure.Maidens;
            Runs = bowlingFigure.Runs;
            Wickets = bowlingFigure.Wickets;
            Innings = bowlingFigure.Innings;
        }
    }
}