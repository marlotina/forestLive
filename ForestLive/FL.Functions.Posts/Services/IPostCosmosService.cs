using FL.Functions.Posts.Dto;
using System.Threading.Tasks;

namespace FL.Functions.Posts.Services
{
    public interface IPostCosmosService
    {
        Task UpdatePostAsync(Model.BirdPost post);

        Task AddCommentPostCountAsync(CommentBaseDto comment);

        Task DeleteCommentPostCountAsync(CommentBaseDto comment);

        Task AddVotePostCountAsync(VotePostBaseDto vote);

        Task DeleteVotePostCountAsync(VotePostBaseDto vote);
    }
}
