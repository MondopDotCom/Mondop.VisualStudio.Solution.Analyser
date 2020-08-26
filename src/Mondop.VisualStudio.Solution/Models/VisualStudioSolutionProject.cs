using System;
using System.Collections.Generic;
using System.IO;

namespace Mondop.VisualStudio.Solution
{
    public class VisualStudioSolutionProject
    {
        public Guid TypeGuid { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public Guid ProjectGuid { get; set; }
        public List<ProjectSection> Sections { get; } = new List<ProjectSection>();

        public VisualStudioSolution Solution { get; set; }

        public VisualStudioProject Project { get; set; }

        public string QualifiedFileName => string.IsNullOrWhiteSpace(Solution?.SolutionFolder)?FileName:Path.Combine(Solution.SolutionFolder, FileName);

        public string ProjectPath => Path.GetDirectoryName(QualifiedFileName);
    }
}
