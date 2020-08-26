using Mondop.Abstractions.Logging;
using Mondop.Guard;

namespace Mondop.VisualStudio.Solution.Analyser.Analysers
{
    public class WarningLevelAnalyser : IAnalyser
    {
        private readonly IProjectNodeSelector _projectNodeSelector;
        private readonly ILogger _logger;
        private readonly IConfigurationPlatformParser _configurationPlatformParser;

        public WarningLevelAnalyser(IProjectNodeSelector projectNodeSelector, ILogger logger,
            IConfigurationPlatformParser configurationPlatformParser)
        {
            _projectNodeSelector = Ensure.IsNotNull(projectNodeSelector, nameof(projectNodeSelector));
            _logger = Ensure.IsNotNull(logger, nameof(logger));
            _configurationPlatformParser = Ensure.IsNotNull(configurationPlatformParser, nameof(configurationPlatformParser));
        }

        public AnalyserType AnalyserType => AnalyserType.WarningLevel;

        private int GetWarningLevel(VisualStudioProjectNode node)
        {
            var warningLevelNode = _projectNodeSelector.First(node, "WarningLevel");
            if (warningLevelNode == null)
                return 4; // Default, node is not always present in .Net Standard project structure.

            return int.Parse(warningLevelNode.Value);
        }

        private bool GetTreatWarningsAsErrors(VisualStudioProjectNode node)
        {
            var treatWarningsAsErrorsNode = _projectNodeSelector.First(node, "TreatWarningsAsErrors");

            if (treatWarningsAsErrorsNode == null)
                return false;

            return bool.Parse(treatWarningsAsErrorsNode.Value);
        }

        public void Analyse(VisualStudioSolutionProject solutionProject, ProjectAnalysis projectAnalysis)
        {
            var configurations = _projectNodeSelector.GetPlatformConfigurations(solutionProject.Project);
            foreach (var configuration in configurations)
            {
                var configurationPlatform = _configurationPlatformParser.GetConfigurationAndPlatform(configuration);

                var configurationPlatformAnalysis = projectAnalysis.GetConfigurationPlatform(
                    configurationPlatform.configuration,configurationPlatform.platform);
                configurationPlatformAnalysis.WarningLevel = GetWarningLevel(configuration);
                configurationPlatformAnalysis.TreatWarningsAsError = GetTreatWarningsAsErrors(configuration);
            }
        }
    }
}
