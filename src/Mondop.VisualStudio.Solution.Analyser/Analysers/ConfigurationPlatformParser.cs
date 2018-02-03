using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mondop.VisualStudio.Solution.Analyser.Analysers
{
    public interface IConfigurationPlatformParser
    {
        (string configuration, string platform) GetConfigurationAndPlatform(VisualStudioProjectNode node);
    }
    public class ConfigurationPlatformParser: IConfigurationPlatformParser
    {
        public (string configuration, string platform) GetConfigurationAndPlatform(VisualStudioProjectNode node)
        {
            var attribute = node.Attributes.First(x => x.Name.Equals("Condition", StringComparison.OrdinalIgnoreCase));

            string pattern = @"==\s*'(?<configuration>.*)\|(?<platform>.*)'";

            var match = Regex.Match(attribute.Value, pattern);

            return (match.Groups["configuration"].Value, match.Groups["platform"].Value);
        }
    }
}
