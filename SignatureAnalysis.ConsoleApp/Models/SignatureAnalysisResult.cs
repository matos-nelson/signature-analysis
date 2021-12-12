using SignatureAnalysis.Core.Enums;

namespace SignatureAnalysis.ConsoleApp.Models
{
    public class SignatureAnalysisResult
    {
        public string Path { get; }
        public string Hash { get; }
        public FileSignature FileSignature { get; }

        public SignatureAnalysisResult(string Path, FileSignature FileSignature, string Hash)
        {
            this.Path = Path;
            this.FileSignature = FileSignature;
            this.Hash = Hash;
        }
    }
}
