namespace Mondop.VisualStudio.Solution.Analyser.Analysers
{
    public interface IAnalyser
    {
        AnalyserType AnalyserType { get; }

        void Analyse(VisualStudioSolutionProject solutionProject, ProjectAnalysis projectAnalysis);
    }
}
