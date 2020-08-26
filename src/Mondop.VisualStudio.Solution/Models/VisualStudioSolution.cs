using System.Collections.Generic;
using System.IO;

namespace Mondop.VisualStudio.Solution
{
    public class VisualStudioSolution
    {
        public string Filename { get; set; }

        public string FormatVersion { get; set; }
        public string VisualStudio { get; set; }
        public string VisualStudioVersion { get; set; }
        public string MinimumVisualStudioVersion { get; set; }

        public List<VisualStudioSolutionProject> Projects { get; } = new List<VisualStudioSolutionProject>();

        public VisualStudioSolutionGlobal Global { get; } = new VisualStudioSolutionGlobal();

        public string SolutionFolder => Path.GetDirectoryName(Filename);


    }
}
