namespace Mondop.VisualStudio.Solution
{
    public interface IVisualStudioSolutionManager
    {
        VisualStudioSolution Load(string fileName,bool readProjects = true);
        void Save(VisualStudioSolution solution);
        VisualStudioSolution CreateNew(string solutionFile);
        void LoadProject(VisualStudioSolutionProject solutionProject);
        void SaveProject(VisualStudioSolutionProject solutionProject);
    }
}
