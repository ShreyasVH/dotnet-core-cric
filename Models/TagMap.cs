using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Com.Dotnet.Cric.Models
{
    public class TagMap
    {
        public long Id { get; set; }
        
        [Required]
        public int EntityId { get; set; }
        
        [Required]
        [ForeignKey("Tag")]
        public int TagId { get; set; }
        
        public Tag Tag { get; set; }

        [JsonConstructor]
        public TagMap()
        {
            // Parameterless constructor required by Entity Framework Core
        }

        public TagMap(int entityId, int tagId)
        {
            EntityId = entityId;
            TagId = tagId;
        }
    }
}