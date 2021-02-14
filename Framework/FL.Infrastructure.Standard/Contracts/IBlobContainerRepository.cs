using System.IO;
using System.Threading.Tasks;

namespace FL.Infrastructure.Standard.Contracts
{
    public interface IBlobContainerRepository
    {
        Task<bool> UploadFileToStorage(Stream fileStream, string fileName, string containerName, string folder = null);

        Task<bool> DeleteFileToStorage(string fileName, string containerName);
    }
}
