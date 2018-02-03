using System.Collections.Generic;
using System.Linq;

namespace Mondop.VisualStudio.Solution.Analyser
{
    public class ProjectAnalysis
    {
        public List<ConfigurationPlatformAnalysis> ConfigurationPlatforms { get; } = new List<ConfigurationPlatformAnalysis>();

        public Framework Framework { get; set; }
        public VisualStudioSolutionProject Project { get; set; }

        public ConfigurationPlatformAnalysis GetConfigurationPlatform(string configuration, string platform)
        {
            var result = ConfigurationPlatforms.FirstOrDefault(x => x.Configuration.Equals(configuration) &&
                x.Platform.Equals(platform));

            if (result != null)
                return result;

            result = new ConfigurationPlatformAnalysis
            {
                Configuration = configuration,
                Platform = platform
            };
            ConfigurationPlatforms.Add(result);
            return result;
        }

    }
}