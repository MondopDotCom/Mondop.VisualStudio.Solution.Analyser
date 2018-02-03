using Mondop.Abstractions.Logging;
using Mondop.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mondop.VisualStudio.Solution.Analyser.Analysers
{
    public class LanguageSpecificationAnalyser: IAnalyser
    {
        private readonly IProjectNodeSelector _projectNodeSelector;
        private readonly ILogger _logger;
        private readonly IConfigurationPlatformParser _configurationPlatformParser;

        public LanguageSpecificationAnalyser(IProjectNodeSelector projectNodeSelector, ILogger logger,
            IConfigurationPlatformParser configurationPlatformParser)
        {
            _projectNodeSelector = Ensure.IsNotNull(projectNodeSelector, nameof(projectNodeSelector));
            _logger = Ensure.IsNotNull(logger, nameof(logger));
            _configurationPlatformParser = Ensure.IsNotNull(configurationPlatformParser, nameof(configurationPlatformParser));
        }

        public AnalyserType AnalyserType => AnalyserType.LanguageSpecification;

        private string GetLanguageVersion(VisualStudioProjectNode node)
        {
            var languageVersion = _projectNodeSelector.First(node, "");

            if (languageVersion == null)
                return "default";

            return languageVersion.Value;
        }

        public void Analyse(VisualStudioSolutionProject solutionProject, ProjectAnalysis projectAnalysis)
        {
            var configurations = _projectNodeSelector.GetPlatformConfigurations(solutionProject.Project);
            foreach (var configuration in configurations)
            {
                var configurationPlatform = _configurationPlatformParser.GetConfigurationAndPlatform(configuration);

                var configurationPlatformAnalysis = projectAnalysis.GetConfigurationPlatform(
                    configurationPlatform.configuration, configurationPlatform.platform);
                configurationPlatformAnalysis.LanguageVersion = GetLanguageVersion(configuration);
            }
        }
    }
}
