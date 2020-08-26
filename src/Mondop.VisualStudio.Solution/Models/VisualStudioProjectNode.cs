using Mondop.VisualStudio.Solution.Models;
using System.Collections.Generic;

namespace Mondop.VisualStudio.Solution
{
    public class VisualStudioProjectNode
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public VisualStudioProjectNodeType NodeType { get; set; }

        public string GeneratedData { get; set; }

        public VisualStudioProjectNode Parent { get; set; }
        public List<VisualStudioProjectNode> Children { get; } = new List<VisualStudioProjectNode>();
        public List<VisualStudioProjectNodeAttribute> Attributes { get; set; } 
    }
}
