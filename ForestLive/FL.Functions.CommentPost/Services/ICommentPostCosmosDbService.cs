using FL.Functions.UserPost.Model;
using System.Threading.Tasks;

namespace FL.Functions.UserPost.Services
{
    public interface ICommentPostCosmosDbService
    {
        Task CreatePostInPendingAsync(CommentPostDto comment);

        Task DeletePostInPendingAsync(CommentPostDto comment);
    }
}
