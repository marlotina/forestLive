using FL.Functions.Posts.Dto;
using System.Threading.Tasks;

namespace FL.Functions.Posts.Services
{
    public interface IPostCosmosService
    {
        Task CreatePostAsync(Model.BirdPost post);

        Task DeletePostAsync(Model.BirdPost post);

        Task UpdatePostAsync(Model.BirdPost post);

        Task AddCommentPostAsync(BirdCommentDto comment);

        Task DeleteCommentPostAsync(BirdCommentDto comment);

        Task AddVotePostAsync(VotePostDto vote);

        Task DeleteVotePostAsync(VotePostDto vote);
    }
}
