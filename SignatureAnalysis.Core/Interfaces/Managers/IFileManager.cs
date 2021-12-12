using SignatureAnalysis.Core.Enums;
using System.IO;

namespace SignatureAnalysis.Core.Interfaces.Managers
{
    public interface IFileManager
    {
        string GetExtension(string path);
        FileStream OpenReadStream(string filename);
        StreamWriter OpenWriteStream(string filename);
        void WriteLine(string content, StreamWriter streamWriter);
        void CloseWriteStream(StreamWriter streamWriter);
        void CloseReadStream(FileStream fileStream);
        FileSignature GetFileSignature(string path);
    }
}
