using SignatureAnalysis.Core;
using SignatureAnalysis.Core.Entities;
using SignatureAnalysis.Core.Enums;
using SignatureAnalysis.Core.Interfaces.Managers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SignatureAnalysis.Infrastructure.Managers
{
    public class FileManager : IFileManager
    {
        private readonly IEnumerable<BaseFile> _types;

        public FileManager()
        {
            _types = ReflectiveEnumerator.GetEnumerableOfType<BaseFile>()
                .OrderByDescending(x => x.SignatureLength)
                .ToList();
        }

        public string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }

        public FileStream OpenReadStream(string filename)
        {
            return File.OpenRead(filename);
        }

        public StreamWriter OpenWriteStream(string path)
        {
            if (path == null)
            {
                return null;
            }

            string dirPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            return new StreamWriter(path, false);
        }

        public void WriteLine(string content, StreamWriter streamWriter)
        {
            if (content == null || streamWriter == null)
            {
                return;
            }

            streamWriter.WriteLine(content);
        }

        public void CloseWriteStream(StreamWriter streamWriter)
        {
            if (streamWriter == null)
            {
                return;
            }

            streamWriter.Close();
        }

        public void CloseReadStream(FileStream fileStream)
        {
            if (fileStream == null)
            {
                return;
            }

            fileStream.Close();
        }

        public FileSignature GetFileSignature(string path)
        {
            if (path == null)
            {
                return FileSignature.NOT_SUPPORTED;
            }

            try
            {
                using var file = OpenReadStream(path);

                FileSignature fileType = FileSignature.NOT_SUPPORTED;
                foreach (var type in _types)
                {
                    fileType = type.Verify(file);
                    if (fileType != FileSignature.NOT_SUPPORTED)
                        break;
                }

                CloseReadStream(file);
                return fileType;
            }
            catch
            {
                return FileSignature.NOT_SUPPORTED;
            }
        }
    }
}
