using FL.Functions.Posts.Model;
using System.Threading.Tasks;

namespace FL.Functions.Post.Services
{
    public interface IBirdsCosmosDbService
    {
        Task AddCommentPostAsync(BirdComment comment);

        Task DeleteCommentPostAsync(BirdComment comment);
    }
}
