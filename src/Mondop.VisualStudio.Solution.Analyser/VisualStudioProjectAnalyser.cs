using Mondop.Core;
using System;

namespace Mondop.VisualStudio.Solution.Analyser
{
    public class VisualStudioProjectAnalyser : IVisualStudioProjectAnalyser
    {
        private readonly IAnalyser[] _analysers;
        private readonly IVisualStudioSolutionManager _visualStudioSolutionManager;

        public VisualStudioProjectAnalyser(IAnalyser[] analysers,IVisualStudioSolutionManager visualStudioSolutionManager)
        {
            _analysers = Ensure.IsNotNull(analysers, nameof(analysers));
            _visualStudioSolutionManager = Ensure.IsNotNull(visualStudioSolutionManager, nameof(visualStudioSolutionManager));
        }

        private bool CanAnalyse(Guid projectType)
        {
            switch (projectType.ToString("B").ToUpper())
            {
                case VisualStudioSolutionProjectTypeIds.CsProjectGuid:
                    return true;
                default:
                    return false;
            }
        }

        public ProjectAnalysis Analyse(VisualStudioSolutionProject solutionProject)
        {
            if (solutionProject.Project == null)
                _visualStudioSolutionManager.LoadProject(solutionProject);

            if (CanAnalyse(solutionProject.TypeGuid))
            {
                var result = new ProjectAnalysis { Project = solutionProject };

                foreach (var analyser in _analysers)
                    analyser.Analyse(solutionProject, result);
            }

            return null;
        }
    }
}
