using SignatureAnalysis.ConsoleApp.Models;
using SignatureAnalysis.Core.Enums;
using SignatureAnalysis.Core.Interfaces.Hash;
using SignatureAnalysis.Core.Interfaces.Managers;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace SignatureAnalysis.ConsoleApp
{
    public class SignatureAnalyzer
    {
        private readonly Task[] _workers;
        private const int NUM_OF_WORKERS = 10;

        private readonly IDirectoryManager _directoryManager;
        private readonly IFileManager _fileManager;
        private readonly IHash _hash;
        private readonly ConcurrentQueue<SignatureAnalysisResult> _resultQueue;
        private ConcurrentQueue<string> _fileListingQueue;

        public SignatureAnalyzer(IDirectoryManager directoryManager, IFileManager fileManager, IHash hash)
        {
            _directoryManager = directoryManager;
            _fileManager = fileManager;
            _hash = hash;
            _resultQueue = new ConcurrentQueue<SignatureAnalysisResult>();

            _workers = new Task[NUM_OF_WORKERS];
        }

        private void Work()
        {
            while (true)
            {
                var retrievedItem = _fileListingQueue.TryDequeue(out string file);
                if (retrievedItem == false)
                {
                    return;
                }

                if (file != null)
                {
                    var fileSignature = _fileManager.GetFileSignature(file);
                    if (fileSignature != FileSignature.NOT_SUPPORTED)
                    {
                        var hash = _hash.hash(file);
                        var result = new SignatureAnalysisResult(file, fileSignature, hash);
                        _resultQueue.Enqueue(result);
                    }
                }
            }
        }

        private void LogResults(string path)
        {
            if (path == null)
            {
                return;
            }

            StreamWriter streamWriter;
            try
            {
                streamWriter = _fileManager.OpenWriteStream(path);
            }
            catch
            {
                return;
            }

            while (_resultQueue.TryDequeue(out SignatureAnalysisResult result))
            {
                string content = result.Path + "," + result.FileSignature.ToString() + "," + result.Hash;
                streamWriter.WriteLine(content);
            }

            _fileManager.CloseWriteStream(streamWriter);
        }

        public void Run(SignatureAnalysisRequest request)
        {
            if (request == null)
            {
                return;
            }

            _fileListingQueue = _directoryManager.GetFileListing(request.ProcessFolder, request.ProcessSubFolders);
            for (int i = 0; i < NUM_OF_WORKERS; i++)
            {
                _workers[i] = Task.Run(() => Work());
            }

            Task.WaitAll(_workers);

            LogResults(request.OutputLocation);
        }
    }
}
