using SignatureAnalysis.ConsoleApp.Models;
using SignatureAnalysis.Core.Interfaces.Managers;
using System;

namespace SignatureAnalysis
{
    public class ArgParser
    {
        private readonly IFileManager _fileManager;
        private readonly IDirectoryManager _directoryManager;

        public ArgParser(IFileManager fileManager, IDirectoryManager directoryManager)
        {
            _fileManager = fileManager;
            _directoryManager = directoryManager;
        }

        public SignatureAnalysisRequest Parse(string[] args)
        {
            if (args == null || args.Length != 3)
            {
                return null;
            }

            string dirToAnalyze = args[0];
            if (!_directoryManager.Exists(dirToAnalyze))
            {
                return null;
            }

            try
            {
                string outputFileExtension = _fileManager.GetExtension(args[1]);
                if (outputFileExtension == string.Empty)
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

            bool processSubFolders;
            try
            {
                processSubFolders = bool.Parse(args[2]);
            }
            catch (FormatException)
            {
                return null;
            }

            return new SignatureAnalysisRequest(args[0], args[1], processSubFolders);
        }
    }
}
