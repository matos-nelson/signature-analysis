namespace SignatureAnalysis.ConsoleApp.Models
{
    public class SignatureAnalysisRequest
    {
        public string ProcessFolder { get; }
        public string OutputLocation { get; }
        public bool ProcessSubFolders { get; }

        public SignatureAnalysisRequest(string ProcessFolder, string OutputLocation, bool ProcessSubFolders)
        {
            this.ProcessFolder = ProcessFolder;
            this.OutputLocation = OutputLocation;
            this.ProcessSubFolders = ProcessSubFolders;
        }
    }
}
