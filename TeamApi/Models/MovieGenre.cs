using System;
namespace TeamApi.Models
{
    public class MovieGenre
    {
        public int Id { get; set; } //Primary key
        public string Name { get; set; } = string.Empty; // e.g. "Horror"
        public string Description { get; set; } = string.Empty; // short description
        public string TypicalMood { get; set; } = string.Empty; // e.g. "Scary", "Exciting"
        public bool IsFamilyFriendly { get; set; } //true/false
        public int AverageRuntimeMinutes { get; set; } // e.g. 120
    }
}
