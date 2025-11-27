using System;
using System.Text.Json.Serialization;

namespace AntarisBlog.Api.Models {
    public class Comment {
        public int Id { get; set; }

        public string AuthorName { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public int PostId { get; set; }

        [JsonIgnore]
        public Post? Post { get; set; }
    }
}
