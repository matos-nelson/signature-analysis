namespace SignatureAnalysis.Core.Entities
{
    public sealed class PdfFile : BaseFile
    {
        public PdfFile()
        {
            Type = Enums.FileSignature.PDF;
            AddSignatures(
                new byte[] { 0x25, 0x50, 0x44, 0x46 }
            );
        }
    }
}
