using Mondop.Abstractions.Logging;
using Mondop.Guard;

namespace Mondop.VisualStudio.Solution.Analyser.Analysers
{
    public class FrameworkAnalyser : IAnalyser
    {
        private readonly IProjectNodeSelector _projectNodeSelector;
        private readonly ILogger _logger;
        private readonly IStringToFrameworkConverter _stringToFrameworkConverter;

        public FrameworkAnalyser(IProjectNodeSelector projectNodeSelector, ILogger logger,
            IStringToFrameworkConverter stringToFrameworkConverter)
        {
            _projectNodeSelector = Ensure.IsNotNull(projectNodeSelector, nameof(projectNodeSelector));
            _logger = Ensure.IsNotNull(logger, nameof(logger));
            _stringToFrameworkConverter = Ensure.IsNotNull(stringToFrameworkConverter, nameof(stringToFrameworkConverter));
        }

        private Framework DetectFramework(VisualStudioSolutionProject project)
        {
            var frameworkVersionNode = _projectNodeSelector.First(project.Project, "TargetFrameworkVersion");
            var targetProfileNode = _projectNodeSelector.First(project.Project, "TargetFrameworkProfile");

            if (frameworkVersionNode != null)
            {
                return _stringToFrameworkConverter.Convert(frameworkVersionNode.Value, targetProfileNode?.Value);
            }

            _logger.Error($"TargetFrameworkVersion not found in project {project.Name}");
            return Framework.Unknown;
        }


        public void Analyse(VisualStudioSolutionProject solutionProject, ProjectAnalysis projectAnalysis)
        {
            projectAnalysis.Framework = DetectFramework(solutionProject);
        }

        public AnalyserType AnalyserType => AnalyserType.FrameworkVersion;
    }
}
