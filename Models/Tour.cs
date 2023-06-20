using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

using Com.Dotnet.Cric.Requests.Tours;

namespace Com.Dotnet.Cric.Models
{
    public class Tour
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public DateTime StartTime { get; set; }

        [JsonConstructor]
        public Tour()
        {
            // Parameterless constructor required by Entity Framework Core
        }

        public Tour(CreateRequest createRequest)
        {
            Name = createRequest.Name;
            StartTime = createRequest.StartTime.Value;
        }
    }
}

