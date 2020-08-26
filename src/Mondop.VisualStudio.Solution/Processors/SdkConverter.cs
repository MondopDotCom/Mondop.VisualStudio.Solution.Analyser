using Mondop.VisualStudio.Solution.Models;

namespace Mondop.VisualStudio.Solution.Processors
{
    public static class SdkConverter
    {
        public static ProjectSdk Convert(string sdkAttributeValue)
        {
            if (string.IsNullOrWhiteSpace(sdkAttributeValue))
                return ProjectSdk.Legacy;

            switch(sdkAttributeValue)
            {
                case "Microsoft.NET.Sdk":
                    return ProjectSdk.MicrosoftNetSdk;
                default:
                    return ProjectSdk.Unknown;
            }
        }
    }
}
