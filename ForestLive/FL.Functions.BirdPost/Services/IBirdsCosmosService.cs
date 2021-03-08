using FL.Functions.BirdPost.Dto;
using FL.Functions.BirdPost.Model;
using System.Threading.Tasks;

namespace FL.Functions.BirdPost.Services
{
    public interface IBirdsCosmosService
    {
        Task CreatePostAsync(Model.Post post);

        Task DeletePostAsync(Model.Post post);

        Task AddVoteAsync(VotePostDto vote);

        Task DeleteVoteAsync(VotePostDto vote);

        Task AddCommentAsync(BirdCommentDto comment);

        Task DeleteCommentAsync(BirdCommentDto comment);
    }
}
