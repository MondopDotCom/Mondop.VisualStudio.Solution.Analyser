using System.Collections.Generic;

namespace Mondop.VisualStudio.Solution
{ 
    public class PackageConfiguration
    {
        public List<PackageConfigurationItem> Packages { get; } = new List<PackageConfigurationItem>();
    }
}
