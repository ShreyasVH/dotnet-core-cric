using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Com.Dotnet.Cric.Requests.Players;

namespace Com.Dotnet.Cric.Models
{
    public class Player
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        [ForeignKey("Country")]
        public long CountryId { get; set; }

        public Country Country { get; set; }
        
        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        [MaxLength(255)]
        public string Image { get; set; }

        [JsonConstructor]
        public Player()
        {
            // Parameterless constructor required by Entity Framework Core
        }

        public Player(CreateRequest createRequest)
        {
            Name = createRequest.Name;
            CountryId = createRequest.CountryId;
            DateOfBirth = createRequest.DateOfBirth.Value;
            Image = createRequest.Image;
        }
    }
}