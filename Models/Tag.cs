using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

using Com.Dotnet.Cric.Requests.Countries;

namespace Com.Dotnet.Cric.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [JsonConstructor]
        public Tag()
        {
            // Parameterless constructor required by Entity Framework Core
        }
    }
}