using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SocialPostsAPI.Models;

namespace SocialPostsAPI.Services
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string PostsCollectionName { get; set;} = string.Empty;
    }

    public class PostService
    {
        private readonly IMongoCollection<Post> _postsCollection;

        public PostService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _postsCollection = database.GetCollection<Post>(settings.Value.PostsCollectionName);
        }

        public async Task<List<Post>> GetPostsAsync() =>
            await _postsCollection.Find(_ => true).ToListAsync();

        public async Task<Post?> GetByIdAsync(string id) =>
            await _postsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Post post) =>
            await _postsCollection.InsertOneAsync(post);

        public async Task UpdateAsync(string id, Post postIn) =>
            await _postsCollection.ReplaceOneAsync(p => p.Id == id, postIn);

        public async Task DeleteAsync(string id) =>
            await _postsCollection.DeleteOneAsync(p => p.Id == id);
    }
}
