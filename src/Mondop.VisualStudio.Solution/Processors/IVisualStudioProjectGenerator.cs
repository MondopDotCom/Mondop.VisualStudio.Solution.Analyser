namespace Mondop.VisualStudio.Solution
{
    public interface IVisualStudioProjectGenerator
    {
        VisualStudioProject CreateNewProject(VisualStudioSolutionProject solutionProject,
            string outputType, string rootNamespace, string targetFramework);
        void AddBuildConfigurations(VisualStudioProject project);
        void AddBuildTargets(VisualStudioSolutionProject solutionProject);

        VisualStudioProjectNode AddCompileItem(VisualStudioProject project, string fileName);
        VisualStudioProjectNode GetCompileItem(VisualStudioProject project, string fileName);
    }
}
