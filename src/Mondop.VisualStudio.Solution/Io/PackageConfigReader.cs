using System.IO;
using System.Xml;

namespace Mondop.VisualStudio.Solution.Io
{
    public class PackageConfigReader: IPackageConfigReader
    {
        private PackageConfigurationItem ReadPackage(XmlReader reader)
        {
            var package = new PackageConfigurationItem();
            package.Id = reader.GetAttribute("id");
            package.Version = reader.GetAttribute("version");
            package.TargetFramework = reader.GetAttribute("targetFramework");

            return package;
        }

        public PackageConfiguration Read(Stream stream)
        {
            using (var xmlReader = XmlReader.Create(stream))
            {
                PackageConfiguration config = null;
                while (xmlReader.Read())
                {
                    if (xmlReader.IsStartElement())
                    {
                        switch (xmlReader.Name)
                        {
                            case "packages":
                                config = new PackageConfiguration();
                                break;
                            case "package":
                                var package = ReadPackage(xmlReader);
                                config.Packages.Add(package);
                                break;
                        }
                    }
                }

                return config;
            }
        }

        public PackageConfiguration Read(string packageConfigFile)
        {
            var stream = new FileStream(packageConfigFile,FileMode.Open,FileAccess.Read);

            return Read(stream);
        }
    }
}
