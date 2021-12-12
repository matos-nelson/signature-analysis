using SignatureAnalysis.Core.Enums;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SignatureAnalysis.Core.Entities
{
    public abstract class BaseFile
    {
        protected FileSignature Type { get; set; }

        private List<byte[]> Signatures { get; } = new List<byte[]>();

        public int SignatureLength => Signatures.Max(m => m.Length);

        protected BaseFile AddSignatures(params byte[][] bytes)
        {
            Signatures.AddRange(bytes);
            return this;
        }

        public FileSignature Verify(Stream stream)
        {
            stream.Position = 0;
            try
            {
                var reader = new BinaryReader(stream);

                var headerBytes = reader.ReadBytes(SignatureLength);

                bool isVerified = Signatures.Any(signature =>
                    headerBytes.Take(signature.Length)
                        .SequenceEqual(signature));

                return isVerified == true ? Type : FileSignature.NOT_SUPPORTED;
            }
            catch
            {
                return FileSignature.NOT_SUPPORTED;
            }
        }
    }
}
