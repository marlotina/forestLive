using FL.Functions.BirdPost.Dto;
using System.Threading.Tasks;

namespace FL.Functions.BirdPost.Services
{
    public interface IBirdsCosmosService
    {
        Task CreatePostAsync(Dto.BirdPost post);

        Task AddCommentAsync(CommentBaseDto comment);

        Task DeleteCommentAsync(CommentBaseDto comment);

        Task AddVotePostAsync(VotePostDto vote);

        Task DeleteVotePostAsync(VotePostDto vote);
    }
}
