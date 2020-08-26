using System.Collections.Generic;

namespace Mondop.VisualStudio.Solution
{
    public class ProjectSection
    {
        public string Name { get; set; }
        public string SectionType { get; set; }
        public List<ProjectSectionItem> Items { get; } = new List<ProjectSectionItem>();
    }
}
