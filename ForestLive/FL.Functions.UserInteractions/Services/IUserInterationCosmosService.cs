using FL.Functions.UserInteractions.Dto;
using System.Threading.Tasks;

namespace FL.Functions.UserInteractions.Services
{
    public interface IUserInterationCosmosService
    {
        Task AddCommentPostAsync(CommentDto comment);

        Task DeleteCommentPostAsync(CommentBaseDto comment);

        Task AddVotePostAsync(VotePostDto vote);

        Task DeleteVotePostAsync(VotePostBaseDto vote);

        Task AddCommentVotePostAsync(VoteCommentPostDto vote);

        Task DeleteCommentVotePostAsync(VoteCommentPostDto vote);
    }
}
