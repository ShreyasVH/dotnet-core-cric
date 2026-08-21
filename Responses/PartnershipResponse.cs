using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class PartnershipResponse
    {
        public int Id { get; set; }
        public int Innings { get; set; }
        public int Wicket { get; set; }
        public int Runs { get; set; }
        public int Balls { get; set; }
        public bool Ended { get; set; }
        public PlayerContribution Player1 { get; set; }
        public PlayerContribution Player2 { get; set; }

        public class PlayerContribution(PlayerMiniResponse player, int runs, int balls)
        {
            public PlayerMiniResponse Player { get; set; } = player;
            public int Runs { get; set; } = runs;
            public int Balls { get; set; } = balls;
        }

        public PartnershipResponse(Partnership partnership, PlayerMiniResponse player1, PlayerMiniResponse player2)
        {
            Id = partnership.Id;
            Innings = partnership.Innings;
            Wicket = partnership.Wicket;
            Runs = partnership.Runs;
            Balls = partnership.Balls;
            Ended = partnership.Ended;
            Player1 = new PlayerContribution(player1, partnership.Runs1, partnership.Balls1);
            Player2 = new PlayerContribution(player2, partnership.Runs2, partnership.Balls2);
        }
    }
}

