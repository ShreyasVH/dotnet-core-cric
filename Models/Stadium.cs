using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Com.Dotnet.Cric.Requests.Stadiums;

namespace Com.Dotnet.Cric.Models
{
    public class Stadium
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string City{ get; set; }
        [MaxLength(100)]
        public string State { get; set; }
        [Required]
        [ForeignKey("Country")]
        public long CountryId { get; set; }

        public Country Country { get; set; }

        [JsonConstructor]
        public Stadium()
        {
            // Parameterless constructor required by Entity Framework Core
        }

        public Stadium(CreateRequest createRequest)
        {
            Name = createRequest.Name;
            City = createRequest.City;
            State = createRequest.State;
            CountryId = createRequest.CountryId;
        }
    }
}
