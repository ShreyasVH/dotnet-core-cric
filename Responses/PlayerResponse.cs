using System;
using System.Collections.Generic;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class PlayerResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CountryResponse Country { get; set; }

        public DateOnly DateOfBirth { get; set; }
        public string Image { get; set; }

        public Dictionary<string, Dictionary<string, int>> DismissalStats { get; set; } = new();
        public Dictionary<string, BattingStats> BattingStats { get; set; } = new();
        public Dictionary<string, BowlingStats> BowlingStats { get; set; } = new();
        public Dictionary<string, FieldingStats> FieldingStats { get; set; } = new();

        public PlayerResponse()
        {

        }

        public PlayerResponse(Player player)
        {
            this.Id = player.Id;
            this.Name = player.Name;
            this.DateOfBirth = player.DateOfBirth;
            this.Image = player.Image;
        }
    }
}