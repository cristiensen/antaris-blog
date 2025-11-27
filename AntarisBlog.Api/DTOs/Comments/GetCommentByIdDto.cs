using System.ComponentModel.DataAnnotations;

namespace AntarisBlog.Api.DTOs.Comments {
    public class GetCommentByIdDto {

        public int Id { get; set; }

        public string AuthorName { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public int PostId { get; set; }
    }
}