using Lamar;
using SignatureAnalysis.Core.Interfaces.Hash;
using SignatureAnalysis.Core.Interfaces.Managers;
using SignatureAnalysis.Infrastructure.Hash;
using SignatureAnalysis.Infrastructure.Managers;

namespace SignatureAnalysis.Infrastructure
{
    public class InfrastructureRegistry : ServiceRegistry
    {
        public InfrastructureRegistry()
        {
            For(typeof(IDirectoryManager)).Use(typeof(DirectoryManager)).Transient();
            For(typeof(IFileManager)).Use(typeof(FileManager)).Scoped();
            For(typeof(IHash)).Use(typeof(MD5Hash)).Transient();
        }
    }
}
