using Mondop.Abstractions.Logging;
using Mondop.Guard;

namespace Mondop.VisualStudio.Solution.Analyser.Analysers
{
    public class CodeAnalysisAnalyser:  IAnalyser
    {
        private readonly IProjectNodeSelector _projectNodeSelector;
        private readonly ILogger _logger;
        private readonly IConfigurationPlatformParser _configurationPlatformParser;

        public CodeAnalysisAnalyser(IProjectNodeSelector projectNodeSelector, ILogger logger,
            IConfigurationPlatformParser configurationPlatformParser)
        {
            _projectNodeSelector = Ensure.IsNotNull(projectNodeSelector, nameof(projectNodeSelector));
            _logger = Ensure.IsNotNull(logger, nameof(logger));
            _configurationPlatformParser = Ensure.IsNotNull(configurationPlatformParser, nameof(configurationPlatformParser));
        }

        public AnalyserType AnalyserType => AnalyserType.CodeAnalysis;

        private string GetCodeAnalysisRuleSet(VisualStudioProjectNode node)
        {
            var codeAnalysisRuleset = _projectNodeSelector.First(node, "CodeAnalysisRuleSet");

            return codeAnalysisRuleset?.Value;
        }

        private bool GetRunCodeAnalysis(VisualStudioProjectNode node)
        {
            var runCodeAnalysis = _projectNodeSelector.First(node, "RunCodeAnalysis");

            if (runCodeAnalysis == null)
                return false;

            return bool.Parse(runCodeAnalysis.Value);
        }

        private bool GetCodeAnalysisIngoreGeneratedCode(VisualStudioProjectNode node)
        {
            var codeAnalysisIngoreGeneratedCode = _projectNodeSelector.First(node, "CodeAnalysisIgnoreGeneratedCode");

            if (codeAnalysisIngoreGeneratedCode == null)
                return true;

            return bool.Parse(codeAnalysisIngoreGeneratedCode.Value);
        }

        public void Analyse(VisualStudioSolutionProject solutionProject, ProjectAnalysis projectAnalysis)
        {
            var configurations = _projectNodeSelector.GetPlatformConfigurations(solutionProject.Project);
            foreach (var configuration in configurations)
            {
                var configurationPlatform = _configurationPlatformParser.GetConfigurationAndPlatform(configuration);

                var configurationPlatformAnalysis = projectAnalysis.GetConfigurationPlatform(
                    configurationPlatform.configuration, configurationPlatform.platform);

                configurationPlatformAnalysis.CodeAnalysisRuleSet = GetCodeAnalysisRuleSet(configuration);
                configurationPlatformAnalysis.RunCodeAnalysis = GetRunCodeAnalysis(configuration);
                configurationPlatformAnalysis.CodeAnalysisIgnoreGeneratedCode = GetCodeAnalysisIngoreGeneratedCode(configuration);
            }
        }
    }
}
