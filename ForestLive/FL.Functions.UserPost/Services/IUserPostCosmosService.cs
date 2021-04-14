using FL.Functions.UserPost.Dto;
using System.Threading.Tasks;

namespace FL.Functions.UserPost.Services
{
    public interface IUserPostCosmosService
    {
        Task CreatePostAsync(Model.BirdPost post);

        Task UpdatePostAsync(Model.BirdPost post);

        Task DeletePostAsync(Model.BirdPost post);

        Task AddVoteAsync(VotePostBaseDto vote);

        Task DeleteVoteAsync(VotePostBaseDto vote);

        Task AddCommentAsync(CommentBaseDto comment);

        Task DeleteCommentAsync(CommentBaseDto comment);
    }
}
