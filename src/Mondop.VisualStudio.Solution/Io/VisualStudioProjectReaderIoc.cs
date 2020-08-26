using Mondop.Core;
using Mondop.Guard;
using Mondop.VisualStudio.Solution.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Mondop.VisualStudio.Solution.Io
{
    public class VisualStudioProjectReaderIoc : IVisualStudioProjectReader
    {
        private readonly IPackageConfigReader _packageConfigReader;

        public VisualStudioProjectReaderIoc(IPackageConfigReader packageConfigReader)
        {
            _packageConfigReader = Ensure.IsNotNull(packageConfigReader, nameof(packageConfigReader));
        }

        private VisualStudioProjectNodeAttribute ReadAttribute(XmlReader textReader)
        {
            var attribute = new VisualStudioProjectNodeAttribute();
            attribute.Name = textReader.Name;
            attribute.Value = textReader.Value;

            return attribute;
        }

        private List<VisualStudioProjectNodeAttribute> ReadAttributes(XmlReader textReader)
        {
            var result = new List<VisualStudioProjectNodeAttribute>();

            if (textReader.MoveToFirstAttribute())
            {
                result.Add(ReadAttribute(textReader));

                while (textReader.MoveToNextAttribute())
                {
                    result.Add(ReadAttribute(textReader));
                }
            }

            return result;
        }

        private PackageConfiguration ReadPackages(string fileName)
        {
            var packagesConfig = Path.Combine(Path.GetDirectoryName(fileName), "packages.config");

            if (!File.Exists(packagesConfig))
                return null;

            return _packageConfigReader.Read(packagesConfig);
        }

        public VisualStudioProject ReadProject(string fileName)
        {
            VisualStudioProjectNode activeNode = null;

            var project = new VisualStudioProject();
            project.ProjectFileName = fileName;

            using (var xmlReader = XmlReader.Create(fileName))
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.IsStartElement())
                    {
                        bool isEmptyElement = xmlReader.IsEmptyElement;
                        var newNode = new VisualStudioProjectNode
                        {
                            Name = xmlReader.Name,
                            Value = xmlReader.Value,
                            NodeType = VisualStudioProjectNodeType.Element
                        };

                        if (xmlReader.HasAttributes)
                        {
                            newNode.Attributes = ReadAttributes(xmlReader);
                        }

                        if (activeNode == null)
                        {
                            project.Root = newNode;
                        }
                        else
                        {
                            newNode.Parent = activeNode;
                            activeNode.Children.Add(newNode);
                        }

                        if (!isEmptyElement)
                            activeNode = newNode;
                    }
                    else if (xmlReader.NodeType == XmlNodeType.EndElement)
                    {
                        activeNode = activeNode?.Parent;
                    }
                    else if (xmlReader.NodeType == XmlNodeType.Text)
                    {
                        activeNode.Value = xmlReader.Value;
                    }
                    else if (xmlReader.NodeType == XmlNodeType.Comment)
                    {
                        var newNode = new VisualStudioProjectNode
                        {
                            Name = xmlReader.Name,
                            Value = xmlReader.Value,
                            NodeType = VisualStudioProjectNodeType.Comment
                        };
                        newNode.Parent = activeNode;
                        activeNode.Children.Add(newNode);
                    }
                }
            }

            project.PackageConfiguration = ReadPackages(fileName);

            return project;
        }
    }
}
