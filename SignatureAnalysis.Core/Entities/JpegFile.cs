namespace SignatureAnalysis.Core.Entities
{
    public sealed class JpegFile : BaseFile
    {
        public JpegFile()
        {
            Type = Enums.FileSignature.JPG;
            AddSignatures(
                new byte[] { 0xFF, 0xD8 }
            );
        }
    }
}
