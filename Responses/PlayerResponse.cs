using System;
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

        public PlayerResponse()
        {

        }

        public PlayerResponse(Player player, CountryResponse countryResponse)
        {
            this.Id = player.Id;
            this.Name = player.Name;
            this.Country = countryResponse;
            this.DateOfBirth = player.DateOfBirth;
            this.Image = player.Image;
        }
    }
}