using System.Collections.Generic;

namespace Mondop.VisualStudio.Solution
{
    public class GlobalSection
    {
        public string Name { get; set; }
        public string SectionType { get; set; }

        public List<GlobalSectionItem> Items { get; } = new List<GlobalSectionItem>();
    }
}
