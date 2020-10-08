using System.IO;
using System.Threading.Tasks;

namespace FL.Infrastructure.Implementations.Domain.Repository
{
    public interface IBlobContainerRepository
    {
        Task<bool> UploadFileToStorage(Stream fileStream, string fileName, string containerName);

        Task<bool> DeleteFileToStorage(string fileName, string containerName);
    }
}
