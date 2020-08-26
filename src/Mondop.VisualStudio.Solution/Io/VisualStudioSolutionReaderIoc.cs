using Mondop.Guard;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mondop.VisualStudio.Solution.Io
{
    public class VisualStudioSolutionReaderIoc: IVisualStudioSolutionReader
    {
        private readonly IFileReaderWriter _fileReaderWriter;

        private string[] _solutionFileLines;
        private int _lineIndex;

        public VisualStudioSolutionReaderIoc(IFileReaderWriter fileReaderWriter)
        {
            _fileReaderWriter = Ensure.IsNotNull(fileReaderWriter,nameof(fileReaderWriter));
        }

        public VisualStudioSolution Load(string solutionFile)
        {
            OpenSolutionFile(solutionFile);

            var solution = new VisualStudioSolution
            {
                Filename = solutionFile
            };

            string str;
            while ((str = ReadLine()) != null)
            {
                if (str.StartsWith(SolutionConstants.FormatVersionPrefix))
                    solution.FormatVersion= ParseFormatVersion(str);
                else if (str.StartsWith(SolutionConstants.VisualStudioPrefix))
                    solution.VisualStudio = ParseVisualStudio(str);
                else if (str.StartsWith(SolutionConstants.VisualStudioVersionPrefix))
                    solution.VisualStudioVersion = ParseVisualStudioVersion(str);
                else if (str.StartsWith(SolutionConstants.MinimumVisualStudioPrefix))
                    solution.MinimumVisualStudioVersion = ParseMinimumVisualStudioVersion(str);
                else if (str.StartsWith("Project("))
                    solution.Projects.Add(ParseProject(solution, str));
                else if (str.Equals("Global"))
                    ParseGlobal(solution);
            }

            return solution;
        }

        private void OpenSolutionFile(string solutionFile)
        {
            if (!_fileReaderWriter.Exists(solutionFile))
                throw new ArgumentException(solutionFile);

            _solutionFileLines = ReadFileContent(solutionFile);
            _lineIndex = 0;
        }

        private string ParseFormatVersion(string line)
        {
             return line.Replace(SolutionConstants.FormatVersionPrefix, "").Trim();
        }

        private string ParseVisualStudio(string line)
        {
            return line.Replace(SolutionConstants.VisualStudioPrefix, "").Trim();
        }

        private string ParseVisualStudioVersion(string line)
        {
             return line.Replace(SolutionConstants.VisualStudioVersionPrefix, "").Trim();
        }

        private string ParseMinimumVisualStudioVersion(string line)
        {
             return line.Replace(SolutionConstants.MinimumVisualStudioPrefix, "").Trim();
        }

        private string[] ReadFileContent(string solutionFile)
        {
            return _fileReaderWriter.ReadAllLines(solutionFile).Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToArray();
        }

        private string ReadLine()
        {
            if (_lineIndex >= _solutionFileLines.Length)
                return null;

            _lineIndex++;
            return _solutionFileLines[_lineIndex - 1];
        }

        private void ParseGlobal(VisualStudioSolution solution)
        {
            string str;
            while ((str = ReadLine()) != null)
            {
                if (str.StartsWith("GlobalSection("))
                {
                    ParseGlobalSection(solution, str);
                }
                else if (str.Equals("EndGlobal"))
                    return;
                else
                {
                    throw new Exception($"Unexpected line in global section: {str}");
                }
            }
        }

        private void ParseGlobalSection(VisualStudioSolution solution, string sectionline)
        {
            var matches = Regex.Match(sectionline, SolutionConstants.GlobalSectionRegEx);

            var section = new GlobalSection
            {
                Name = matches.Groups["sectionName"].Value,
                SectionType = matches.Groups["sectionValue"].Value
            };
            solution.Global.Sections.Add(section);

            string str;
            while ((str = ReadLine()) != null)
            {
                if (str.Equals("EndGlobalSection"))
                    return;

                section.Items.Add(ParseGlobalSectionItem(str));
            }
        }

        private GlobalSectionItem ParseGlobalSectionItem(string line)
        {
            var matches = Regex.Match(line, SolutionConstants.GlobalSectionItemRegEx);

            var item = new GlobalSectionItem
            {
                Name = matches.Groups["name"].Value,
                Value = matches.Groups["value"].Value
            };

            return item;
        }

        private VisualStudioSolutionProject ParseProject(VisualStudioSolution solution, string projectLine)
        {
            var project = new VisualStudioSolutionProject { Solution = solution };
            var matches = Regex.Match(projectLine, SolutionConstants.ProjectRegex);
            project.TypeGuid = new Guid(matches.Groups["typeguid"].Value);
            project.Name = matches.Groups["name"].Value;
            project.FileName = matches.Groups["filename"].Value;
            project.ProjectGuid = new Guid(matches.Groups["projectguid"].Value);

            string str;
            while ((str = ReadLine()) != null)
            {
                if (str.Equals("EndProject"))
                    return project;
                else if (str.StartsWith("ProjectSection("))
                {
                    project.Sections.Add(ParseProjectSection(str));
                }
                else
                {
                    throw new Exception($"Unexpected line in project: {str}");
                }
            }
            return project;
        }

        private ProjectSection ParseProjectSection(string sectionline)
        {
            var matches = Regex.Match(sectionline, SolutionConstants.ProjectSectionRegEx);

            var projectSection = new ProjectSection
            {
                Name = matches.Groups["sectionName"].Value,
                SectionType = matches.Groups["sectionValue"].Value
            };

            string str;
            while ((str = ReadLine()) != null)
            {
                if (str.Equals("EndProjectSection"))
                    return projectSection;

                projectSection.Items.Add(ParseProjectSectionItem(str));
            }

            return projectSection;
        }

        private ProjectSectionItem ParseProjectSectionItem(string itemLine)
        {
            var matches = Regex.Match(itemLine, SolutionConstants.ProjectSectionItemRegEx);

            var item = new ProjectSectionItem
            {
                Name = matches.Groups["name"].Value,
                Value = matches.Groups["value"].Value
            };

            return item;
        }
    }
}
