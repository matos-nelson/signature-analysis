using System.Collections.Concurrent;

namespace SignatureAnalysis.Core.Interfaces.Managers
{
    public interface IDirectoryManager
    {
        bool Exists(string path);
        ConcurrentQueue<string> GetFileListing(string path, bool includeSubFolders);
    }
}
