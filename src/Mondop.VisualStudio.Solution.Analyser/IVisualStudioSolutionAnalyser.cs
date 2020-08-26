namespace Mondop.VisualStudio.Solution.Analyser
{
    public interface IVisualStudioSolutionAnalyser
    {
        SolutionAnalysis Analyse(VisualStudioSolution solution);
    }
}
