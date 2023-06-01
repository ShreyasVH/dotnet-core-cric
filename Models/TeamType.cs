using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Com.Dotnet.Cric.Models
{
    public class TeamType
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
