using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

using Com.Dotnet.Cric.Requests.Countries;

namespace Com.Dotnet.Cric.Models
{
    public class Country
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [JsonConstructor]
        public Country()
        {
            // Parameterless constructor required by Entity Framework Core
        }

        public Country(CreateRequest createRequest)
        {
            Name = createRequest.Name;
        }
    }
}
