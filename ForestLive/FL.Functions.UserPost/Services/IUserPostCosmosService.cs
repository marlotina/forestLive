using FL.Functions.UserPost.Dto;
using System.Threading.Tasks;

namespace FL.Functions.UserPost.Services
{
    public interface IUserPostCosmosService
    {
        Task CreatePostAsync(Model.Post post);

        Task UpdatePostAsync(Model.Post post);

        Task DeletePostAsync(Model.Post post);

        Task AddVoteAsync(VotePostDto vote);

        Task DeleteVoteAsync(VotePostDto vote);

        Task AddCommentAsync(CommentDto comment);

        Task DeleteCommentAsync(CommentDto comment);
    }
}
