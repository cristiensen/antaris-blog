using System.ComponentModel.DataAnnotations;

namespace AntarisBlog.Api.DTOs.Posts {
    public class UpdatePostDto {
        [MaxLength(200)]
        public string? Title { get; set; }

        [MaxLength(72)]
        public string? Slug { get; set; }

        public string? Content { get; set; }

        public bool? IsPublished { get; set; }
    }
}
