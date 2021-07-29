using FL.Functions.UserPost.Dto;
using System.Threading.Tasks;

namespace FL.Functions.UserPost.Services
{
    public interface IUserPostCosmosService
    {
        Task AddVoteCountAsync(VotePostDto vote);

        Task DeleteVoteCountAsync(VotePostDto vote);

        Task AddCommentCountAsync(CommentDto comment);

        Task DeleteCommentCountAsync(CommentDto comment);
    }
}
