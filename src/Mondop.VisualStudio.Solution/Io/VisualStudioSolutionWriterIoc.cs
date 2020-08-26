using Mondop.Guard;
using System.Text;

namespace Mondop.VisualStudio.Solution.Io
{
    public class VisualStudioSolutionWriterIoc : IVisualStudioSolutionWriter
    {
        private readonly IFileReaderWriter _fileReaderWriter;

        public VisualStudioSolutionWriterIoc(IFileReaderWriter fileReader)
        {
            _fileReaderWriter = Ensure.IsNotNull(fileReader, nameof(fileReader));
        }

        private void WriteHeader(VisualStudioSolution solution, StringBuilder sb)
        {
            sb.AppendLine($"{SolutionConstants.FormatVersionPrefix} {solution.FormatVersion}");
            sb.AppendLine($"{SolutionConstants.VisualStudioPrefix} {solution.VisualStudio}");
            sb.AppendLine($"{SolutionConstants.VisualStudioVersionPrefix} {solution.VisualStudioVersion}");
            sb.AppendLine($"{SolutionConstants.MinimumVisualStudioPrefix} {solution.MinimumVisualStudioVersion}");
        }

        private void WriteProjects(VisualStudioSolution solution, StringBuilder sb)
        {
            foreach (var solutionProject in solution.Projects)
            {
                sb.AppendLine($"Project(\"{solutionProject.TypeGuid.ToString("B").ToUpperInvariant()}\") = \"{solutionProject.Name}\"," +
                    $" \"{solutionProject.FileName}\", \"{solutionProject.ProjectGuid.ToString("B").ToUpperInvariant()}\"");

                foreach (var section in solutionProject.Sections)
                {
                    sb.AppendLine($"\tProjectSection({section.Name}) = {section.SectionType}");
                    foreach (var sectionItem in section.Items)
                        sb.AppendLine($"\t\t{sectionItem.Name} = {sectionItem.Value}");
                    sb.AppendLine($"\tEndProjectSection");
                }

                sb.AppendLine("EndProject");
            }
        }

        private void WriteGlobalSection(StringBuilder sb, GlobalSection section)
        {
            sb.AppendLine($"\tGlobalSection({section.Name}) = {section.SectionType}");
            foreach (var item in section.Items)
                sb.AppendLine($"\t\t{item.Name} = {item.Value}");
            sb.AppendLine("\tEndGlobalSection");
        }

        private void WriteGlobal(VisualStudioSolution solution, StringBuilder sb)
        {
            sb.AppendLine("Global");

            foreach (var globalSection in solution.Global.Sections)
                WriteGlobalSection(sb, globalSection);

            sb.AppendLine("EndGlobal");
        }

        public void Save(VisualStudioSolution solution)
        {
            var sb = new StringBuilder();

            sb.AppendLine();
            WriteHeader(solution, sb);
            WriteProjects(solution, sb);
            WriteGlobal(solution, sb);

            _fileReaderWriter.WriteFile(solution.Filename, sb.ToString(), Encoding.UTF8);
        }
    }
}
