using FL.Functions.UserPost.Dto;
using FL.Functions.UserPost.Model;
using System.Threading.Tasks;

namespace FL.Functions.UserPost.Services
{
    public interface IUserPostCosmosService
    {
        Task CreatePostInPendingAsync(Model.BirdPost post);

        Task DeletePostInPendingAsync(Model.BirdPost post);

        Task AddVoteAsync(VotePost vote);

        Task DeleteVoteAsync(VotePost vote);

        Task AddCommentAsync(BirdCommentDto comment);

        Task DeleteCommentAsync(BirdCommentDto comment);
    }
}
