using SignatureAnalysis.Core.Interfaces.Managers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace SignatureAnalysis.Infrastructure.Managers
{
    public class DirectoryManager : IDirectoryManager
    {
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public ConcurrentQueue<string> GetFileListing(string path, bool includeSubFolders)
        {
            var searchOption = includeSubFolders == true ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            IEnumerable<string> files;
            try
            {
                files = Directory.EnumerateFiles(path, "*", searchOption);
            }
            catch
            {
                return null;
            }

            ConcurrentQueue<string> cQueue = new ConcurrentQueue<string>();
            foreach (string file in files)
            {
                cQueue.Enqueue(file);
            }

            return cQueue;
        }
    }
}
