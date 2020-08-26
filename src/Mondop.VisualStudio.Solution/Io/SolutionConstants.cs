namespace Mondop.VisualStudio.Solution.Io
{
    public static class SolutionConstants
    {
        public const string FormatVersionPrefix = "Microsoft Visual Studio Solution File, Format Version";
        public const string VisualStudioPrefix = "# Visual Studio";
        public const string VisualStudioVersionPrefix = "VisualStudioVersion =";
        public const string MinimumVisualStudioPrefix = "MinimumVisualStudioVersion =";

        public const string ProjectRegex = "Project\\(\\\"(?<typeguid>.*?)\\\"\\)\\s*=\\s*\\\"(?<name>.*?)\\\"\\s*,\\s*\\\"(?<filename>.*?)\\\"\\s*,\\s*\\\"(?<projectguid>.*?)\\\"";
        public const string GlobalSectionRegEx = "GlobalSection\\((?<sectionName>.*?)\\)\\s*=\\s*(?<sectionValue>.*)";
        public const string GlobalSectionItemRegEx = "\\s*(?<name>.*?)\\s*=\\s*(?<value>.*)\\s*";

        public const string ProjectSectionRegEx = "ProjectSection\\((?<sectionName>.*?)\\)\\s*=\\s*(?<sectionValue>.*)";
        public const string ProjectSectionItemRegEx = "\\s*(?<name>.*?)\\s*=\\s*(?<value>.*)\\s*";
    }
}
