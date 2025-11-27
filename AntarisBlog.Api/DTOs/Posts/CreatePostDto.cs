using System.ComponentModel.DataAnnotations;

namespace AntarisBlog.Api.DTOs.Posts {
    public class CreatePostDto {

        public string Title { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public bool IsPublished { get; set; } = false;
    }
}
