using Mondop.Core;
using Mondop.Guard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Mondop.VisualStudio.Solution.Io
{
    public class VisualStudioProjectWriterIoc : IVisualStudioProjectWriter
    {
        private readonly IFileReaderWriter _fileReaderWriter;

        public VisualStudioProjectWriterIoc(IFileReaderWriter fileReaderWriter)
        {
            _fileReaderWriter = Ensure.IsNotNull(fileReaderWriter, nameof(fileReaderWriter));
        }

        private void WriteAttributes(XmlWriter xmlWriter, IEnumerable<VisualStudioProjectNodeAttribute> attributes)
        {
            if (attributes == null)
                return;

            foreach (var attribute in attributes)
                xmlWriter.WriteAttributeString(attribute.Name, attribute.Value);
        }

        private void WriteNode(XmlWriter xmlWriter, VisualStudioProjectNode node,string projectPath)
        {
            var ns = node.Attributes?.SingleOrDefault(x => x.Name.Equals("xmlns"));
            var attr = node.Attributes?.Where(x => !x.Name.Equals("xmlns"));

            if (ns!=null)
                xmlWriter.WriteStartElement(node.Name,ns.Value);
            else
                xmlWriter.WriteStartElement(node.Name);
            WriteAttributes(xmlWriter, attr);
            foreach (var child in node.Children)
                WriteNode(xmlWriter, child,projectPath);
            if (!string.IsNullOrWhiteSpace(node.Value))
                xmlWriter.WriteString(node.Value);
            xmlWriter.WriteEndElement();

            if (!string.IsNullOrWhiteSpace(node.GeneratedData))
                WriteGeneratedData(node,projectPath);
        }

        public void WriteProject(string fileName, VisualStudioSolutionProject project)
        {
            if (project.TypeGuid == new Guid(VisualStudioSolutionProjectTypeIds.SolutionFolderGuid))
                return;

            var ms = new MemoryStream();

            var settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            using (var xmlWriter = XmlWriter.Create(ms ,settings))
            {
                xmlWriter.WriteStartDocument();
                WriteNode(xmlWriter, project.Project.Root, project.ProjectPath);
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
            }

            _fileReaderWriter.WriteFile(project.QualifiedFileName,ms);
        }

        private string GetNodeFileName(VisualStudioProjectNode node)
        {
            var nodeFilename = node.Attributes.FirstOrDefault(a => a.Name == "Include")?.Value;

            return nodeFilename;
        }

        private void WriteGeneratedData(VisualStudioProjectNode node,string projectPath)
        {
            var fileName = Path.Combine(projectPath, GetNodeFileName(node));

            _fileReaderWriter.WriteFile(fileName, node.GeneratedData,Encoding.UTF8);
        }
    }
}
