using System.Collections.Generic;
using System.Linq;

namespace Mondop.VisualStudio.Solution
{
    public class ProjectNodeSelector : IProjectNodeSelector
    {
        private VisualStudioProjectNode Select(VisualStudioProjectNode node,string name)
        {
            if (node.Name.Equals(name))
                return node;

            foreach(var child in node.Children)
            {
                var foundNode = Select(child, name);
                if (foundNode != null)
                    return foundNode;
            }

            return null;
        }

        private void SelectAll(VisualStudioProjectNode node,string name,List<VisualStudioProjectNode> target)
        {
            if (node.Name.Equals(name))
                target.Add(node);

            foreach (var child in node.Children)
                SelectAll(child, name, target);
        }

        private List<VisualStudioProjectNode> SelectAll(VisualStudioProjectNode node,string name)
        {
            var result = new List<VisualStudioProjectNode>();
            SelectAll(node, name, result);

            return result;
        }

        public VisualStudioProjectNode First(VisualStudioProject project, string name)
        {
            return Select(project.Root, name);
        }

        public VisualStudioProjectNode First(VisualStudioProjectNode node, string name)
        {
            return Select(node, name);
        }

        public List<VisualStudioProjectNode> GetPlatformConfigurations(VisualStudioProject project)
        {
            var result = SelectAll(project.Root, "PropertyGroup");

            return result.Where(x => x.Attributes?.Any(a => a.Name.Equals("Condition"))??false).ToList();
        }
    }
}
