using SignatureAnalysis.Core.Interfaces.Hash;
using SignatureAnalysis.Core.Interfaces.Managers;
using System;

namespace SignatureAnalysis.Infrastructure.Hash
{
    public class MD5Hash : IHash
    {
        private readonly IFileManager _fileManager;

        public MD5Hash(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public string hash(string filename)
        {
            if (filename == null)
            {
                return null;
            }

            try
            {
                using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                using var stream = _fileManager.OpenReadStream(filename);
                var computedHash = md5.ComputeHash(stream);
                return BitConverter.ToString(computedHash).Replace("-", "").ToUpperInvariant();
            }
            catch
            {
                return null;
            }
        }
    }
}
