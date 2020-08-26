namespace Mondop.VisualStudio.Solution.Io
{
    public interface IPackageConfigReader
    {
        PackageConfiguration Read(string packageConfigFile);
    }
}
