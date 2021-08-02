using Fl.Functions.UserInteractions.Model;
using FL.Functions.UserInteractions.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Functions.UserInteractions.Services
{
    public interface IUserInterationCosmosService
    {
        Task AddVotePostAsync(VotePostDto vote);

        Task DeleteVotePostAsync(VotePostBaseDto vote);

        Task AddFollowerAsync(UserFollowDto follower);

        Task DeleteFollowerAsync(UserFollowDto follower);

        Task AddLabelAsync(IEnumerable<UserLabel> labels);

        Task RemovePostLabelAsync(IEnumerable<UserLabel> removeLabels);
    }
}
