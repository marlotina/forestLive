using FL.Functions.BirdPost.Dto;
using System.Threading.Tasks;

namespace FL.Functions.BirdPost.Services
{
    public interface IBirdsCosmosService
    {
        Task CreatePostAsync(Dto.BirdPost post);

        Task UpdateSpecieAsync(Dto.BirdPost post);

        Task AddCommentAsync(CommentPostDto comment);

        Task DeleteCommentAsync(CommentPostDto comment);

        Task AddVotePostAsync(VotePostDto vote);

        Task DeleteVotePostAsync(VotePostDto vote);
    }
}
