using FL.Functions.Posts.Model;
using System.Threading.Tasks;

namespace FL.Functions.Posts.Services
{
    public interface IPostCosmosService
    {
        Task AddCommentPostAsync(BirdComment comment);

        Task DeleteCommentPostAsync(BirdComment comment);

        Task AddVotePostAsync(VotePostDto vote);

        Task DeleteVotePostAsync(VotePostDto vote);
    }
}
