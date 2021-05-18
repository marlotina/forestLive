using FL.Functions.BirdPost.Dto;
using System.Threading.Tasks;

namespace FL.Functions.BirdPost.Services
{
    public interface ISpeciePostCosmosService
    {
        Task CreatePostAsync(Dto.BirdPost post);

        Task AddCommentCountAsync(CommentBaseDto comment);

        Task DeleteCommentCountAsync(CommentBaseDto comment);

        Task AddVotePostCountAsync(VotePostBaseDto vote);

        Task DeleteVotePostCountAsync(VotePostBaseDto vote);
    }
}
