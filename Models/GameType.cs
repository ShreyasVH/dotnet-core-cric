using System.ComponentModel.DataAnnotations;


namespace Com.Dotnet.Cric.Models
{
    public class GameType
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}