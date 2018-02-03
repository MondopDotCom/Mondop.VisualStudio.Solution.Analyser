namespace Mondop.VisualStudio.Solution.Analyser
{
    public class ConfigurationPlatformAnalysis
    {
        public string Configuration { get; set; }
        public string Platform { get; set; }
        public int WarningLevel { get; set; }
        public bool TreatWarningsAsError { get; set; }
        public string CodeAnalysisRuleSet { get; set; }
        public bool RunCodeAnalysis { get; set; }
        public bool CodeAnalysisIgnoreGeneratedCode { get; set; }
        public string LanguageVersion { get; set; }
    }
}
