namespace Mondop.VisualStudio.Solution.Analyser
{
    public interface IAnalyser
    {
        AnalyserType AnalyserType { get; }

        void Analyse(VisualStudioSolutionProject solutionProject, ProjectAnalysis projectAnalysis);
    }
}
