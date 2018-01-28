using System;

namespace Mondop.VisualStudio.Solution.Analyser
{
    public class StringToFrameworkConverter : IStringToFrameworkConverter
    {
        private bool IsClientProfile(string targetFrameworkProfile)
        {
            return !string.IsNullOrWhiteSpace(targetFrameworkProfile) &&
                targetFrameworkProfile.Equals("Client");
        }

        public Framework Convert(string frameworkVersion, string targetFrameworkProfile)
        {
            switch (frameworkVersion)
            {
                case "v1.0":
                    return Framework.Framework1_0;
                case "v1.1":
                    return Framework.Framework1_1;
                case "v2.0":
                    return Framework.Framework2_0;
                case "v3.0":
                    return Framework.Framework3_0;
                case "v3.5":
                    return IsClientProfile(targetFrameworkProfile) ? Framework.Framework3_5_ClientProfile : Framework.Framework3_5;
                case "v4.0":
                    return IsClientProfile(targetFrameworkProfile) ? Framework.Framework4_0_ClientProfile : Framework.Framework4_0;
                case "v4.5":
                    return Framework.Framework4_5;
                case "v4.5.1":
                    return Framework.Framework4_5_1;
                case "v4.5.2":
                    return Framework.Framework4_5_2;
                case "v4.6":
                    return Framework.Framework4_6;
                case "v4.6.1":
                    return Framework.Framework4_6_1;
                case "v4.6.2":
                    return Framework.Framework4_6_2;
                case "v4.7":
                    return Framework.Framework4_7;
                case "v4.7.1":
                    return Framework.Framework4_7_1;
                default:
                    throw new UnimplementedConversionException(frameworkVersion, typeof(Framework).Name);
            }
        }
    }
}
