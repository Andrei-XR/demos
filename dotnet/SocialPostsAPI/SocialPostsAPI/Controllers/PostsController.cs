using Microsoft.AspNetCore.Mvc;
using SocialPostsAPI.Models;
using SocialPostsAPI.Services;

namespace SocialPostsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly PostService _postService;

        public PostsController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Post>>> Get()
        {
            return await _postService.GetPostsAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Post>> Get(string id)
        {
            var post = await _postService.GetByIdAsync(id);

            if(post == null)
                return NotFound();

            return post;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Post post)
        {
            await _postService.CreateAsync(post);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Update(string id, Post postIn)
        {
            var existing = await _postService.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            postIn.Id = existing.Id;
            await _postService.UpdateAsync(id, postIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existing = await _postService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _postService.DeleteAsync(id);
            return NoContent();
        }
    }
}
