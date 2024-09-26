using Newtonsoft.Json;
using System;

namespace Note.Function.Entities
{
    public class PersonalNote
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } 
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReminderTime { get; set; }
    }
}
