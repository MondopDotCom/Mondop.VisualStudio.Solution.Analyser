using System;
using System.Collections.Generic;
using System.Linq;

namespace Mondop.VisualStudio.Solution
{
    public class VisualStudioProjectGeneratorIoc : IVisualStudioProjectGenerator
    {
        private VisualStudioProjectNode CreateImportCommonProps(VisualStudioProjectNode parent)
        {
            var node = new VisualStudioProjectNode
            {
                Name = "Import",
                Parent = parent,
                Attributes = new List<VisualStudioProjectNodeAttribute>
                {
                    new VisualStudioProjectNodeAttribute { Name= "Project",Value = "$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props" },
                    new VisualStudioProjectNodeAttribute { Name="Condition",Value ="Exists('$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props')"}
                }
            };

            return node;
        }

        public void AddBuildTargets(VisualStudioSolutionProject solutionProject)
        {
            var root = solutionProject.Project.Root;

            if (solutionProject.TypeGuid == new Guid(VisualStudioSolutionProjectTypeIds.CsProjectGuid))
            {
                var node = root.Children.FirstOrDefault(n => n.Name.Equals("Import") && n.Attributes.Any(a => a.Name.Equals("Project") && a.Value.Equals("$(MSBuildToolsPath)\\Microsoft.CSharp.targets")));
                if (node != null)
                    return;

                int lastNodeIndex = -1;
                var lastNode = root.Children.Last(n => n.Name.Equals("ItemGroup"));
                if (lastNode != null)
                    root.Children.IndexOf(lastNode);

                var targetsNode = new VisualStudioProjectNode
                {
                    Name = "Import",
                    Parent = root,
                    Attributes = new List<VisualStudioProjectNodeAttribute>
                    {
                        new VisualStudioProjectNodeAttribute { Name="Project", Value="$(MSBuildToolsPath)\\Microsoft.CSharp.targets"}
                    }
                };
                if (lastNodeIndex == -1)
                    root.Children.Add(targetsNode);
                else
                    root.Children.Insert(lastNodeIndex + 1, targetsNode);
            }
        }

        private VisualStudioProjectNode CreatePropertyGroup(VisualStudioSolutionProject solutionProject,
            string outputType, string rootNamespace, string targetFramework)
        {
            var node = new VisualStudioProjectNode
            {
                Name = "PropertyGroup"
            };

            node.Children.Add(new VisualStudioProjectNode
            {
                Name = "Configuration",
                Value = "Debug",
                Parent = node,
                Attributes = new List<VisualStudioProjectNodeAttribute>
                {
                    new VisualStudioProjectNodeAttribute{ Name = "Condition", Value=" '$(Configuration)' == '' "}
                }
            });
            node.Children.Add(new VisualStudioProjectNode
            {
                Name = "Platform",
                Value = "AnyCPU",
                Parent = node,
                Attributes = new List<VisualStudioProjectNodeAttribute>
                {
                    new VisualStudioProjectNodeAttribute{ Name ="Condition", Value=" '$(Platform)' == '' "}
                }
            });
            node.Children.Add(new VisualStudioProjectNode { Name = "ProjectGuid", Parent = node, Value = solutionProject.ProjectGuid.ToString("B").ToUpperInvariant() });
            node.Children.Add(new VisualStudioProjectNode { Name = "OutputType", Parent = node, Value = outputType });
            node.Children.Add(new VisualStudioProjectNode { Name = "AppDesignerFolder", Parent = node, Value = "Properties" });
            node.Children.Add(new VisualStudioProjectNode { Name = "RootNamespace", Parent = node, Value = rootNamespace });
            node.Children.Add(new VisualStudioProjectNode { Name = "AssemblyName", Parent = node, Value = rootNamespace });
            node.Children.Add(new VisualStudioProjectNode { Name = "TargetFrameworkVersion", Parent = node, Value = targetFramework });

            return node;
        }

        private VisualStudioProjectNode CreateRoot(VisualStudioSolutionProject solutionProject,
            string outputType, string rootNamespace, string targetFramework)
        {
            var node = new VisualStudioProjectNode
            {
                Name = "Project",
                Attributes = new List<VisualStudioProjectNodeAttribute>
                {
                    new VisualStudioProjectNodeAttribute{ Name="xmlns", Value ="http://schemas.microsoft.com/developer/msbuild/2003"},
                    new VisualStudioProjectNodeAttribute{ Name="DefaultTargets", Value ="Build"},
                    new VisualStudioProjectNodeAttribute{ Name="ToolsVersion", Value="12.0"}
                }
            };
            node.Children.Add(CreateImportCommonProps(node));
            node.Children.Add(CreatePropertyGroup(solutionProject, outputType, rootNamespace, targetFramework));

            return node;
        }

        public VisualStudioProject CreateNewProject(VisualStudioSolutionProject solutionProject,
            string outputType, string rootNamespace, string targetFramework)
        {
            var project = new VisualStudioProject
            {
                ProjectFileName = solutionProject.FileName,
                Root = CreateRoot(solutionProject, outputType, rootNamespace, targetFramework)
            };

            solutionProject.Project = project;

            return project;
        }

        public void AddNode(VisualStudioProjectNode parent, string name, string value)
        {
            parent.Children.Add(new VisualStudioProjectNode { Name = name, Value = value, Parent = parent });
        }

        public void AddBuildConfigurations(VisualStudioProject project)
        {
            var propertyGroup = new VisualStudioProjectNode
            {
                Parent = project.Root,
                Name = "PropertyGroup",
                Attributes = new List<VisualStudioProjectNodeAttribute> { new VisualStudioProjectNodeAttribute { Name = "Condition", Value = " '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' " } }
            };
            project.Root.Children.Add(propertyGroup);

            AddNode(propertyGroup, "DebugSymbols", "true");
            AddNode(propertyGroup, "DebugType", "full");
            AddNode(propertyGroup, "Optimize", "false");
            AddNode(propertyGroup, "OutputPath", "bin\\Debug\\");
            AddNode(propertyGroup, "DefineConstants", "DEBUG;TRACE");
            AddNode(propertyGroup, "ErrorReport", "prompt");
            AddNode(propertyGroup, "WarningLevel", "4");
            AddNode(propertyGroup, "Prefer32Bit", "false");

            propertyGroup = new VisualStudioProjectNode
            {
                Parent = project.Root,
                Name = "PropertyGroup",
                Attributes = new List<VisualStudioProjectNodeAttribute> { new VisualStudioProjectNodeAttribute { Name = "Condition", Value = " '$(Configuration)|$(Platform)' == 'Release|AnyCPU' " } }
            };
            project.Root.Children.Add(propertyGroup);

            AddNode(propertyGroup, "DebugType", "pdbonly");
            AddNode(propertyGroup, "Optimize", "true");
            AddNode(propertyGroup, "OutputPath", "bin\\Release\\");
            AddNode(propertyGroup, "DefineConstants", "TRACE");
            AddNode(propertyGroup, "ErrorReport", "prompt");
            AddNode(propertyGroup, "WarningLevel", "4");
            AddNode(propertyGroup, "Prefer32Bit", "false");
        }

        private VisualStudioProjectNode GetCompileItemGroup(VisualStudioProjectNode root)
        {
            return root.Children.FirstOrDefault(x => x.Children.Any(y => y.Name.Equals("Compile")));
        }

        public VisualStudioProjectNode GetCompileItem(VisualStudioProject project, string fileName)
        {
            var compileItemGroups = project.Root.Children.Where(x => x.Children.Any(y => y.Name.Equals("Compile")));

            foreach (var compileItemGroup in compileItemGroups)
            {
                var node = compileItemGroup.Children.FirstOrDefault(x => x.Name.Equals("Compile") && x.Attributes.First(y => y.Name == "Include").Value.Equals(fileName));
                if (node != null)
                    return node;
            }

            return null;
        }

        public VisualStudioProjectNode AddCompileItem(VisualStudioProject project, string fileName)
        {
            var itemGroup = GetCompileItemGroup(project.Root);

            if (itemGroup == null)
            {
                itemGroup = new VisualStudioProjectNode
                {
                    Name = "ItemGroup",
                    Parent = project.Root
                };

                project.Root.Children.Add(itemGroup);
            }

            var compileItem = new VisualStudioProjectNode
            {
                Parent = itemGroup,
                Name = "Compile",
                Attributes = new List<VisualStudioProjectNodeAttribute> { new VisualStudioProjectNodeAttribute { Name = "Include", Value = fileName } }
            };

            itemGroup.Children.Add(compileItem);

            return compileItem;
        }
    }
}
