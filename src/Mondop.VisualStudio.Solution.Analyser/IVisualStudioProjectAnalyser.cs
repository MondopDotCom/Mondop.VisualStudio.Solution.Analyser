namespace Mondop.VisualStudio.Solution.Analyser
{
    public interface IVisualStudioProjectAnalyser
    {
        ProjectAnalysis Analyse(VisualStudioSolutionProject solutionProject);
    }
}
