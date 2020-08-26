namespace Mondop.VisualStudio.Solution.Io
{
    public interface IVisualStudioProjectReader
    {
        VisualStudioProject ReadProject(string fileName);
    }
}
