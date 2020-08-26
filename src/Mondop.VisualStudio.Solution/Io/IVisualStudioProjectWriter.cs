namespace Mondop.VisualStudio.Solution.Io
{
    public interface IVisualStudioProjectWriter
    {
        void WriteProject(string fileName, VisualStudioSolutionProject project);
    }
}
