using System.ComponentModel.DataAnnotations;

namespace AntarisBlog.Api.DTOs.Comments {
    public class UpdateCommentDto {

        public string Text { get; set; } = string.Empty;
    }
}