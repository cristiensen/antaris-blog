using System.ComponentModel.DataAnnotations;

namespace AntarisBlog.Api.DTOs.Comments {
    public class CreateCommentDto {

        public int PostId { get; set; }

        public string AuthorName { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;
    }
}