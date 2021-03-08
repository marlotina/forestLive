using FL.Functions.UserPost.Dto;
using System.Threading.Tasks;

namespace FL.Functions.UserPost.Services
{
    public interface IUserPostCosmosService
    {
        Task CreatePostInPendingAsync(Model.BirdPost post);

        Task DeletePostInPendingAsync(Model.BirdPost post);

        Task AddVoteAsync(VotePostDto vote);

        Task DeleteVoteAsync(VotePostDto vote);

        Task AddCommentAsync(BirdCommentDto comment);

        Task DeleteCommentAsync(BirdCommentDto comment);
    }
}
