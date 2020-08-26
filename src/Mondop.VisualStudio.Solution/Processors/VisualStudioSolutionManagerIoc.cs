using Mondop.Guard;
using Mondop.VisualStudio.Solution.Io;
using System;

namespace Mondop.VisualStudio.Solution
{
    public class VisualStudioSolutionManagerIoc : IVisualStudioSolutionManager
    {
        private readonly IVisualStudioSolutionWriter _solutionWriter;
        private readonly IVisualStudioSolutionReader _solutionReader;
        private readonly IVisualStudioProjectReader _projectReader;
        private readonly IVisualStudioProjectWriter _projectWriter;

        public VisualStudioSolutionManagerIoc(IVisualStudioSolutionWriter visualStudioSolutionWriter,
            IVisualStudioSolutionReader visualStudioSolutionReader, IVisualStudioProjectReader projectReader,
            IVisualStudioProjectWriter projectWriter)
        {
            _solutionWriter = Ensure.IsNotNull(visualStudioSolutionWriter, nameof(visualStudioSolutionWriter));
            _solutionReader = Ensure.IsNotNull(visualStudioSolutionReader, nameof(visualStudioSolutionReader));
            _projectReader = Ensure.IsNotNull(projectReader, nameof(projectReader));
            _projectWriter = Ensure.IsNotNull(projectWriter, nameof(projectWriter));
        }

        public VisualStudioSolution CreateNew(string solutionFile)
        {
            var solution = new VisualStudioSolution
            {
                Filename = solutionFile,
                FormatVersion = "12.00",
                VisualStudio = "14",
                VisualStudioVersion = "14.0.25420.1",
                MinimumVisualStudioVersion = "10.0.40219.1",
            };

            var section = new GlobalSection
            {
                Name = "SolutionConfigurationPlatforms",
                SectionType = "preSolution"
            };
            section.Items.Add(new GlobalSectionItem { Name = "Debug|Any CPU", Value = "Debug|Any CPU" });
            section.Items.Add(new GlobalSectionItem { Name = "Release|Any CPU", Value = "Release|Any CPU" });
            solution.Global.Sections.Add(section);

            section = new GlobalSection
            {
                Name = "SolutionProperties",
                SectionType = "preSolution"
            };
            section.Items.Add(new GlobalSectionItem { Name = "HideSolutionNode", Value = "FALSE" });
            solution.Global.Sections.Add(section);

            return solution;
        }

        public VisualStudioSolution Load(string fileName, bool readProjects = true)
        {
            var solution = _solutionReader.Load(fileName);

            if (readProjects)
                foreach (var project in solution.Projects)
                    LoadProject(project);

            return solution;
       }

        public void Save(VisualStudioSolution solution)
        {
            foreach (var project in solution.Projects)
                SaveProject(project);

            _solutionWriter.Save(solution);
        }

        public void LoadProject(VisualStudioSolutionProject solutionProject)
        {
            if (solutionProject.TypeGuid == Guid.Parse(VisualStudioSolutionProjectTypeIds.SolutionFolderGuid))
                return;

            solutionProject.Project = _projectReader.ReadProject(solutionProject.QualifiedFileName);
        }

        public void SaveProject(VisualStudioSolutionProject solutionProject)
        {
            _projectWriter.WriteProject(solutionProject.QualifiedFileName, solutionProject);
        }
    }
}
