using FL.Functions.BirdPost.Model;
using System.Threading.Tasks;

namespace FL.Functions.BirdPost.Services
{
    public interface IBirdsCosmosService
    {
        Task CreatePostAsync(BirdPostDto post);

        Task DeletePostAsync(BirdPostDto post);

        Task AddVoteAsync(VotePostDto post);

        Task DeleteVoteAsync(VotePostDto deletePostRequest);

        Task AddCommentAsync(BirdCommentDto comment);

        Task DeleteCommentAsync(BirdCommentDto comment);
    }
}
