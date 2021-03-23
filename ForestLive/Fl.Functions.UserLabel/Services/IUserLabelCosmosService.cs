using Fl.Functions.UserLabel.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Functions.UserLabel.Services
{
    public interface IUserLabelCosmosService
    {
        Task AddLabelAsync(IEnumerable<Fl.Functions.UserLabel.Model.UserLabel> labels);

        Task RemovePostLabelAsync(IEnumerable<RemoveLabelDto> removeLabels);
    }
}
