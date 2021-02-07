using System.IO;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Domain.Repositories
{
    public interface IUserImageRepository
    {
        Task UploadFileToStorage(Stream fileStream, string fileName);

        Task DeleteFileToStorage(string fileName);
    }
}
