using AntarisBlog.Api.Data;
using AntarisBlog.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AntarisBlog.Api.DTOs.Comments;
using System.Diagnostics;

namespace AntarisBlog.Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase {

        private readonly AntarisBlogContext _context;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(AntarisBlogContext context, ILogger<CommentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/comments/byPost/:Id
        [HttpGet("byPost/{postId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsForPost(int postId)
        {
            var comments = await _context.Comments
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return Ok(comments);
        }

        // POST: api/comments
        [HttpPost]
        public async Task<ActionResult<Comment>> CreateComment(CreateCommentDto dto)
        {
            var post = await _context.Posts.FindAsync(dto.PostId);
            if (post == null)
            {
                return NotFound($"Post with id {dto.PostId} not found.");
            }

            var comment = new Comment
            {
                AuthorName = dto.AuthorName,
                Text = dto.Text,
                PostId = dto.PostId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCommentsForPost),
                new { postId = comment.PostId },
                comment
            );
        }

        // PUT: api/comments/:Id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, UpdateCommentDto dto)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
                return NotFound($"Comment with ID {id} not found");

            comment.AuthorName = comment.AuthorName;
            comment.Text = dto.Text;
            comment.CreatedAt = comment.CreatedAt;
            comment.PostId = comment.PostId;
            comment.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(comment);
        }

        // GET: api/comments/:Id
        [HttpGet("{commentId}")]
        public async Task<ActionResult<Comment>> GetCommentById(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);

            if (comment == null)
                return NotFound($"Comment with ID {commentId} not found.");

            var dto = new GetCommentByIdDto
            {
                Id = comment.Id,
                AuthorName = comment.AuthorName,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt,
                PostId = comment.PostId
            };

            return Ok(dto);
        }

        // DELETE: api/comments/:Id
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            _logger.LogInformation($"Comment id to be deleted: {commentId}");

            var comment = await _context.Comments.FindAsync(commentId);

            _logger.LogInformation($"Comment found: {comment}");

            if (comment == null)
                return NotFound();

            _context.Comments.Remove(comment);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
