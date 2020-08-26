using Mondop.VisualStudio.Solution.Models;
using Mondop.VisualStudio.Solution.Processors;
using System.IO;
using System.Linq;

namespace Mondop.VisualStudio.Solution
{
    public class VisualStudioProject
    {
        public string ProjectFileName { get; set; }
        public string ProjectPath => Path.GetDirectoryName(ProjectFileName);
        public string ProjectName => Path.GetFileNameWithoutExtension(ProjectFileName);
        public VisualStudioProjectNode Root { get; set; }
        public ProjectSdk Sdk => SdkConverter.Convert(Root?.Attributes.FirstOrDefault(a => a.Name.Equals(Sdk))?.Value);

        public PackageConfiguration PackageConfiguration { get; set; }
    }
}
