using FL.Functions.Posts.Dto;
using System.Threading.Tasks;

namespace FL.Functions.Posts.Services
{
    public interface IPostCosmosService
    {
        Task CreatePostAsync(Model.BirdPost post);

        Task DeletePostAsync(Model.BirdPost post);

        Task UpdatePostAsync(Model.BirdPost post);

        Task AddCommentPostAsync(CommentBaseDto comment);

        Task DeleteCommentPostAsync(CommentBaseDto comment);

        Task AddVotePostAsync(VotePostBaseDto vote);

        Task DeleteVotePostAsync(VotePostBaseDto vote);
    }
}
