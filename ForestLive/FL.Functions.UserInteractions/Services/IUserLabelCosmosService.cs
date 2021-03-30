using FL.Functions.UserInteractions.Dto;
using System.Threading.Tasks;

namespace FL.Functions.UserInteractions.Services
{
    public interface IUserLabelCosmosService
    {
        Task AddCommentPostAsync(BirdCommentDto comment);

        Task DeleteCommentPostAsync(BirdCommentDto comment);

        Task AddVotePostAsync(VotePostDto vote);

        Task DeleteVotePostAsync(VotePostDto vote);
    }
}
