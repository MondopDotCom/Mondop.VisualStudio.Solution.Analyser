namespace Mondop.VisualStudio.Solution.Io
{
    public interface IVisualStudioSolutionReader
    {
        VisualStudioSolution Load(string solutionFile);
    }
}
