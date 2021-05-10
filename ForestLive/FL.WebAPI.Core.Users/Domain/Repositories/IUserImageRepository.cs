using System.IO;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Domain.Repositories
{
    public interface IUserImageRepository
    {
        Task<bool> UploadFileToStorage(Stream fileStream, string fileName);

        Task<bool> DeleteFileToStorage(string fileName);
    }
}
