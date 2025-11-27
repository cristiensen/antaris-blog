using AntarisBlog.Api.Data;
using AntarisBlog.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AntarisBlog.Api.DTOs.Posts;

namespace AntarisBlog.Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase {
        private readonly AntarisBlogContext _context;

        public PostsController(AntarisBlogContext context)
        {
            _context = context;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var posts = await _context.Posts
                .Include(p => p.Comments)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return Ok(posts);
        }

        // GET: api/posts/:postId
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        // POST: api/posts
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(CreatePostDto dto)
        {
            var post = new Post
            {
                Title = dto.Title,
                Slug = dto.Slug,
                Content = dto.Content,
                IsPublished = dto.IsPublished,
                CreatedAt = DateTime.UtcNow
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, UpdatePostDto dto)
        {
            var existingPost = await _context.Posts.FindAsync(id);
            if (existingPost == null)
                return NotFound();

            if (dto.Title != null)
                existingPost.Title = dto.Title;

            if (dto.Slug != null)
                existingPost.Slug = dto.Slug;

            if (dto.Content != null)
                existingPost.Content = dto.Content;

            if (dto.IsPublished.HasValue)
                existingPost.IsPublished = dto.IsPublished.Value;

            existingPost.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/posts/:postId
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return NotFound();

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
