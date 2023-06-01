using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Com.Dotnet.Cric.Requests.Teams;

namespace Com.Dotnet.Cric.Models
{
    public class Team
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [Required]
        [ForeignKey("Country")]
        public long CountryId { get; set; }

        public Country Country { get; set; }

        [Required]
        [ForeignKey("TeamType")]
        public int TypeId { get; set; }

        public TeamType Type { get; set; }

        [JsonConstructor]
        public Team()
        {
            // Parameterless constructor required by Entity Framework Core
        }

        public Team(CreateRequest createRequest)
        {
            Name = createRequest.Name;
            CountryId = createRequest.CountryId;
            TypeId = createRequest.TypeId;
        }
    }
}
