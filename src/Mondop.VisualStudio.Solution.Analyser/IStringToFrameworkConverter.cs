namespace Mondop.VisualStudio.Solution.Analyser
{
    public interface IStringToFrameworkConverter
    {
        Framework Convert(string frameworkVersion, string targetFrameworkProfile);
    }
}
